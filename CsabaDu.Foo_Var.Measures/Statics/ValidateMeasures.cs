using CsabaDu.Foo_Var.Measures.Factories;
using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;

namespace CsabaDu.Foo_Var.Measures.Statics;

public static class ValidateMeasures
{
    #region Properties
    private static HashSet<Type> ValidQuantityTypes => new()
    {
        typeof(int),
        typeof(uint),
        typeof(long),
        typeof(ulong),
        typeof(double),
        typeof(decimal),
    };

    private static HashSet<Type> ValidQuantityParamTypes => new(ValidQuantityTypes)
    {
        typeof(byte),
        typeof(sbyte),
        typeof(short),
        typeof(ushort),
        typeof(float),
    };

    internal static ICollection<Enum> ValidMeasureUnits => ExchangeMeasures.Rates.Keys;

    private static HashSet<Type> ValidMeasureUnitTypes => ValidMeasureUnits.Select(x => x.GetType()).ToHashSet();

    //private static HashSet<Type> BaseMeasureTypes => new()
    //{
    //    typeof(IMeasure),
    //    typeof(IDenominator),
    //    typeof(ILimit),
    //};

    //private static bool IsValidBaseMeasureType(Type? baseMeasureType)
    //{
    //    return baseMeasureType == null || BaseMeasureTypes.Contains(baseMeasureType);
    //}

    #endregion

    #region Quantity
    internal static bool IsValidQuantityType(this Type type)
    {
        return ValidQuantityTypes.Contains(type);
    }

    //internal static bool IsValidQuantityParam(this ValueType quantity, Type? baseMeasureType = null) // TODO
    //{
    //    if (!IsValidBaseMeasureType(baseMeasureType)) return false;

    //    if (!IsValidQuantityParamType(quantity.GetType())) return false;

    //    if (baseMeasureType == null || baseMeasureType == typeof(IMeasure)) return true;

    //    if (quantity is not decimal decimalQuantity)
    //    {
    //        ValueType? nullableQuantity = quantity.ToQuantity(typeof(decimal));

    //        if (nullableQuantity == null) return false;

    //        decimalQuantity = (decimal)nullableQuantity;
    //    }

    //    return IsValidQuantity(decimalQuantity, baseMeasureType);

    //    //throw new ArgumentOutOfRangeException(nameof(baseMeasureType));
    //}

    internal static bool IsValidQuantityParamType(Type? type)
    {
        if (type == null) return false;

        return ValidQuantityParamTypes.Contains(type);
    }

    internal static bool TryGetValidQuantity(ValueType? quantityParam, [NotNullWhen(true)] out ValueType? quantity, BaseMeasure baseMeasureType = default)
    {
        //baseMeasureType ??= typeof(IMeasure);

        if (!TryGetNotNullQuantityParam(quantityParam, out quantity, baseMeasureType)) return false;

        if (baseMeasureType == default)
        {
            quantity = GetNumeratorQuantity(quantity);
        }
        else
        {
            if (quantity.ToQuantity(typeof(decimal)) is not decimal decimalQuantity) return false;

            if (!decimalQuantity.IsValidQuantity(baseMeasureType)) return false;

            switch (baseMeasureType)
            {
                case BaseMeasure.Measure:
                    break;
                case BaseMeasure.Denominator:
                    quantity = GetDenominatorQuantity(decimalQuantity);
                    break;
                case BaseMeasure.Limit:
                    quantity = GetLimitQuantity(decimalQuantity);
                    break;

                default:
                    return false;
            }
            //if (baseMeasureType == BaseMeasureType.Denominator)
            //{
            //    quantity = GetDenominatorQuantity(decimalQuantity);
            //}
            //else if (baseMeasureType == BaseMeasureType.Limit)
            //{
            //    quantity = GetLimitQuantity(decimalQuantity);
            //}
            //else
            //{
            //    return false;
            //}
        }

        return quantity != null;
    }

    private static bool TryGetNotNullQuantityParam(ValueType? quantityParam, [NotNullWhen(true)] out ValueType? quantity, BaseMeasure baseMeasureType)
    {
        quantity = default;

        //if (baseMeasureType == BaseMeasureType.Measure)
        //{
        //    quantity = quantityParam;
        //}
        //else if (baseMeasureType == BaseMeasureType.Denominator)
        //{
        //    quantity = quantityParam ?? decimal.One;
        //}
        //else if (baseMeasureType == BaseMeasureType.Limit)
        //{
        //    quantity = quantityParam ?? uint.MinValue;
        //}
        //else
        //{
        //    return false;
        //}

        switch (baseMeasureType)
        {
            case BaseMeasure.Measure:
                quantity = quantityParam;
                break;
            case BaseMeasure.Denominator:
                quantity = quantityParam ?? decimal.One;
                break;
            case BaseMeasure.Limit:
                quantity = quantityParam ?? uint.MinValue;
                break;

            default:
                return false;
        }

        return IsValidQuantityParamType(quantity?.GetType());
    }

    private static void ValidateMeasureQuantity(ValueType? quantity, BaseMeasure baseMeasureType)
    {
        if (quantity != null) return;
        else if (baseMeasureType != default) return;

        throw new ArgumentNullException(nameof(quantity));
    }

    internal static ValueType GetValidQuantity(ValueType? quantity, BaseMeasure baseMeasureType = default)
    {
        ValidateMeasureQuantity(quantity, baseMeasureType);

        if (TryGetValidQuantity(quantity, out quantity, baseMeasureType) && quantity != null) return quantity;

        throw new ArgumentOutOfRangeException(nameof(quantity));
    }

    private static ValueType? GetNumeratorQuantity(ValueType? quantity)
    {
        if (quantity is sbyte || quantity is short) return quantity.ToQuantity(typeof(int));

        if (quantity is byte || quantity is ushort) return quantity.ToQuantity(typeof(uint));

        return quantity;
    }

    private static decimal? GetDenominatorQuantity(decimal quantity)
    {
        return quantity.IsValidQuantity(BaseMeasure.Denominator) ?
            quantity
            : null;
    }

    private static ulong? GetLimitQuantity(decimal quantity)
    {
        return quantity.IsValidQuantity(BaseMeasure.Limit) ?
            (ulong)quantity.ToQuantity(typeof(ulong))!
            : null;
    }

    private static bool IsValidQuantity(this decimal quantity, BaseMeasure baseMeasureType)
    {
        return baseMeasureType switch
        {
            BaseMeasure.Measure => true,
            BaseMeasure.Denominator => quantity > decimal.Zero,
            BaseMeasure.Limit => quantity >= ulong.MinValue && quantity <= ulong.MaxValue,

            _ => false,
        };
    }

    internal static HashSet<Type> GetValidQuantityTypes() => ValidQuantityTypes;
    #endregion

    #region MeasureUnit
    public static bool IsDefinedMeasureUnit(this Enum measureUnit, Type? measureUnitType = null)
    {
        Type thisMeasureUnitType = measureUnit.GetType();
        measureUnitType ??= thisMeasureUnitType;

        return measureUnitType.IsValidMeasureUnitType() && measureUnitType == thisMeasureUnitType && Enum.IsDefined(measureUnitType, measureUnit);
    }

    public static bool IsValidMeasureUnit(this Enum measureUnit)
    {
        return ValidMeasureUnits.Contains(measureUnit);
    }

    private static bool IsValidMeasureUnitType(this Type type)
    {
        return ValidMeasureUnitTypes.Contains(type);
    }

    internal static void ValidateExchangeRate(this Enum measureUnit, decimal? exchangeRate, bool constantMeasureUnitsOnly)
    {
        if (exchangeRate is not decimal notNullExchangeRate) return;

        if (constantMeasureUnitsOnly && !ExchangeMeasures.ConstantMeasureUnits.Contains(measureUnit)) return;

        if (measureUnit.TryAddExchangeRate(notNullExchangeRate)) return;

        if (notNullExchangeRate == measureUnit.GetExchangeRate()) return;

        throw new ArgumentOutOfRangeException(nameof(exchangeRate), exchangeRate, null);
    }
    #endregion

    internal static ILimit GetOrCreateLimit(this IBaseMeasure baseMeasure, ILimit? limit = null)
    {
        return limit ?? new LimitFactory().GetLimit(baseMeasure);
    }

    internal static ILimit GetOrCreateLimit(this IRate other, ILimit? limit = null)
    {
        if (other is ILimitedRate limitedRate) return GetOrCreateLimit(limitedRate.Limit, limit);

        return GetOrCreateLimit(other.Denominator, limit);
    }
}
