using CsabaDu.FooVar.Measures.Factories;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes;

namespace CsabaDu.FooVar.Measures.Statics;

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

    private static IEnumerable<Type> ValidMeasureUnitTypes => ValidMeasureUnits.Select(x => x.GetType());
    #endregion

    #region Quantity
    internal static bool IsValidQuantityType(this Type type)
    {
        return ValidQuantityTypes.Contains(type);
    }

    internal static bool IsValidQuantityParamType(Type? type)
    {
        if (type == null) return false;

        return ValidQuantityParamTypes.Contains(type);
    }

    internal static bool TryGetValidQuantity(ValueType? quantityParam, [NotNullWhen(true)] out ValueType? quantity, BaseMeasureType baseMeasureType = default)
    {
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
                case BaseMeasureType.Measure:
                    break;
                case BaseMeasureType.Denominator:
                    quantity = GetDenominatorQuantity(decimalQuantity);
                    break;
                case BaseMeasureType.Limit:
                    quantity = GetLimitQuantity(decimalQuantity);
                    break;

                default:
                    return false;
            }
        }

        return quantity != null;
    }

    private static bool TryGetNotNullQuantityParam(ValueType? quantityParam, [NotNullWhen(true)] out ValueType? quantity, BaseMeasureType baseMeasureType)
    {
        quantity = default;

        switch (baseMeasureType)
        {
            case BaseMeasureType.Measure:
                quantity = quantityParam;
                break;
            case BaseMeasureType.Denominator:
                quantity = quantityParam ?? decimal.One;
                break;
            case BaseMeasureType.Limit:
                quantity = quantityParam ?? uint.MinValue;
                break;

            default:
                return false;
        }

        return IsValidQuantityParamType(quantity?.GetType());
    }

    private static void ValidateMeasureQuantity(ValueType? quantity, BaseMeasureType baseMeasureType)
    {
        if (quantity != null) return;
        else if (baseMeasureType != default) return;

        throw new ArgumentNullException(nameof(quantity));
    }

    internal static ValueType GetValidQuantity(ValueType? quantity, BaseMeasureType baseMeasureType = default)
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
        return quantity.IsValidQuantity(BaseMeasureType.Denominator) ?
            quantity
            : null;
    }

    private static ulong? GetLimitQuantity(decimal quantity)
    {
        return quantity.IsValidQuantity(BaseMeasureType.Limit) ?
            (ulong)quantity.ToQuantity(typeof(ulong))!
            : null;
    }

    private static bool IsValidQuantity(this decimal quantity, BaseMeasureType baseMeasureType)
    {
        return baseMeasureType switch
        {
            BaseMeasureType.Measure => true,
            BaseMeasureType.Denominator => quantity > decimal.Zero,
            BaseMeasureType.Limit => quantity >= ulong.MinValue && quantity <= ulong.MaxValue,

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

        return measureUnitType.IsValidMeasureUnitType()
            && measureUnitType == thisMeasureUnitType
            && Enum.IsDefined(measureUnitType, measureUnit);
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
