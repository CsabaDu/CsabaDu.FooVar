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

    private static IEnumerable<TypeCode> ValidQuantityTypeCodes
    {
        get
        {
            foreach (Type item in ValidQuantityTypes)
            {
                yield return Type.GetTypeCode(item);
            }
        }
    }

    internal static ICollection<Enum> ValidMeasureUnits => ExchangeMeasures.Rates.Keys;

    private static IEnumerable<Type> ValidMeasureUnitTypes => ValidMeasureUnits.Select(x => x.GetType());
    #endregion

    #region Quantity
    internal static bool IsValidQuantityType(this Type type)
    {
        return ValidQuantityTypes.Contains(type);
    }

    internal static bool IsValidTypeQuantityParam(ValueType? quantity)
    {
        if (quantity == null) return false;

        return IsValidQuantityType(quantity.GetType());
    }

    internal static bool TryGetValidQuantity(ValueType? quantityParam, [NotNullWhen(true)] out ValueType? quantity, BaseMeasureType baseMeasureType = BaseMeasureType.Measure)
    {
        if (!TryGetNotNullQuantityParam(quantityParam, baseMeasureType, out quantity)) return false;

        if (quantity.ToQuantity(TypeCode.Decimal) is not decimal decimalQuantity) return false;

        switch (baseMeasureType)
        {
            case BaseMeasureType.Measure:
                return decimalQuantity.IsValidDecimalQuantity(baseMeasureType);
            case BaseMeasureType.Denominator:
                quantity = GetDenominatorQuantity(decimalQuantity);
                return quantity != null;
            case BaseMeasureType.Limit:
                quantity = GetLimitQuantity(decimalQuantity);
                return quantity != null;

            default:
                return false;
        }
    }

    private static bool TryGetNotNullQuantityParam(ValueType? quantityParam, BaseMeasureType baseMeasureType, [NotNullWhen(true)] out ValueType? quantity)
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

        return IsValidTypeQuantityParam(quantity);
    }

    private static void ValidateMeasureQuantity(ValueType? quantity, BaseMeasureType baseMeasureType)
    {
        if (quantity != null)
        {
            TypeCode typeCode = Type.GetTypeCode(quantity.GetType());
            var (minValue, maxValue) = GetQuantityValueLimits(typeCode);
            decimal? nullableDecimalQuantity = (decimal?)quantity.ToQuantity(TypeCode.Decimal);

            if (nullableDecimalQuantity is decimal decimalQuantity
                && decimalQuantity >= minValue
                && decimalQuantity <= maxValue) return;

            throw new ArgumentOutOfRangeException(nameof(quantity), quantity, null);
        }
        else if (baseMeasureType != BaseMeasureType.Measure) return;

        throw new ArgumentNullException(nameof(quantity));
    }

    internal static ValueType GetValidQuantity(ValueType? quantity, BaseMeasureType baseMeasureType = default)
    {
        ValidateMeasureQuantity(quantity, baseMeasureType);

        if (TryGetValidQuantity(quantity, out quantity, baseMeasureType) && quantity != null) return quantity;

        throw new ArgumentOutOfRangeException(nameof(quantity));
    }

    private static decimal? GetDenominatorQuantity(decimal quantity)
    {
        return quantity.IsValidDecimalQuantity(BaseMeasureType.Denominator) ?
            quantity
            : null;
    }

    private static ulong? GetLimitQuantity(decimal quantity)
    {
        return quantity.IsValidDecimalQuantity(BaseMeasureType.Limit) ?
            (ulong?)quantity.ToQuantity(TypeCode.UInt64)
            : null;
    }

    private static bool IsValidDecimalQuantity(this decimal quantity, BaseMeasureType baseMeasureType)
    {
        return baseMeasureType switch
        {
            BaseMeasureType.Measure => true,
            BaseMeasureType.Denominator => quantity > decimal.Zero,
            BaseMeasureType.Limit => quantity >= ulong.MinValue && quantity <= ulong.MaxValue,

            _ => throw new ArgumentOutOfRangeException(nameof(baseMeasureType), baseMeasureType, null),
        };
    }

    internal static HashSet<TypeCode> GetValidQuantityTypeCodes() => ValidQuantityTypeCodes.ToHashSet();

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

    internal static void ValidateConstantMeasureUnitType(Type measureUnitType)
    {
        _ = measureUnitType ?? throw new ArgumentNullException(nameof(measureUnitType));

        if (ExchangeMeasures.ConstantMeasureUnitTypes.Contains(measureUnitType)) return;

        throw new ArgumentOutOfRangeException(nameof(measureUnitType), measureUnitType, null);
    }

    internal static void ValidateExchangeRate(this Enum measureUnit, decimal? exchangeRate, bool constantMeasureUnitsOnly)
    {
        if (exchangeRate is not decimal notNullExchangeRate) return;

        if (notNullExchangeRate <= 0) throw new ArgumentOutOfRangeException(nameof(exchangeRate), exchangeRate, null);

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

        return GetOrCreateLimit((IBaseMeasure)other, limit);
    }

    internal static (decimal minValue, decimal maxValue) GetQuantityValueLimits(TypeCode? typeCode = null)
    {
        return typeCode switch
        {
            TypeCode.Int32 => (int.MinValue, int.MaxValue),
            TypeCode.UInt32 => (uint.MinValue, uint.MaxValue),
            TypeCode.Int64 => (long.MinValue, long.MaxValue),
            TypeCode.UInt64 => (ulong.MinValue, ulong.MaxValue),

            _ => (decimal.MinValue, decimal.MaxValue),
        };
    }
}
