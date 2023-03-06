﻿using CsabaDu.FooVar.Measures.Factories;
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

    internal static bool IsValidQuantityParamType(Type? type)
    {
        if (type == null) return false;

        return ValidQuantityTypes.Contains(type);
    }

    internal static bool TryGetValidQuantity(ValueType? quantityParam, [NotNullWhen(true)] out ValueType? quantity, BaseMeasureType baseMeasureType = BaseMeasureType.Measure)
    {
        if (!TryGetNotNullQuantityParam(quantityParam, out quantity, baseMeasureType)) return false;

        //if (baseMeasureType == BaseMeasureType.Measure)
        //{
        //    quantity = GetMeasureQuantity(quantity);
        //}
        //else
        //{
        if (quantity.ToQuantity(TypeCode.Decimal) is not decimal decimalQuantity) return false;

        if (!decimalQuantity.IsValidQuantity(baseMeasureType)) return false;

        switch (baseMeasureType)
        {
            case BaseMeasureType.Measure:
                return true;
            case BaseMeasureType.Denominator:
                quantity = GetDenominatorQuantity(decimalQuantity);
                return quantity != null;
            case BaseMeasureType.Limit:
                quantity = GetLimitQuantity(decimalQuantity);
                return quantity != null;

            default:
                return false;
        }
        //}

        //return quantity != null;
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

    private static ValueType? GetMeasureQuantity(ValueType? quantity)
    {
        if (quantity is not ValueType notNullQuantity) return null;

        Type quantityType = notNullQuantity.GetType();

        return notNullQuantity.ToQuantity(quantityType);
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
            (ulong?)quantity.ToQuantity(TypeCode.UInt64)
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

    internal static (decimal minValue, decimal maxValue) GetQuantityValueLimits(TypeCode typeCode)
    {
        switch (typeCode)
        {
            case TypeCode.Int32:
                return new(int.MinValue, int.MaxValue);
            case TypeCode.UInt32:
                return new(uint.MinValue, uint.MaxValue);
            case TypeCode.Int64:
                return new(long.MinValue, long.MaxValue);
            case TypeCode.UInt64:
                return new(ulong.MinValue, ulong.MaxValue);
            default:
                return new(decimal.MinValue, decimal.MaxValue);
        }
    }

}
