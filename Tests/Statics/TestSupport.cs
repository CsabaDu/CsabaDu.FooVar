namespace CsabaDu.FooVar.Tests.Statics;

#nullable disable
internal static class TestSupport
{

    internal static void RemoveIfNotDefaultMeasureUnit(Enum measureUnit)
    {
        if (measureUnit.ShouldHaveAdHocExchangeRate())
        {
            ExchangeMeasures.Rates.Remove(measureUnit);
        }
    }

    internal static void RestoreDefaultMeasureUnits()
    {
        foreach (Enum measureUnit in ValidateMeasures.ValidMeasureUnits)
        {
            RemoveIfNotDefaultMeasureUnit(measureUnit);
        }
    }

    internal static bool IsIntegerType(Type type)
    {
        return type == typeof(int) || type == typeof(uint) || type == typeof(long) || type == typeof(ulong);
    }

    internal static decimal GetQuantityDecimalValue(decimal decimalQuantity, Type quantityType)
    {
        if (quantityType == typeof(decimal) || quantityType == typeof(double)) return decimal.Round(decimalQuantity, 8);

        if (quantityType == typeof(float)) return decimal.Round(decimalQuantity, 4);

        return decimal.Round(decimalQuantity);
    }

    internal static decimal GetQuantityDecimalValue(decimal decimalQuantity, TypeCode quantityTypeCode)
    {
        return quantityTypeCode switch
        {
            TypeCode.Single => decimal.Round(decimalQuantity, 4),
            TypeCode.Double => decimal.Round(decimalQuantity, 8),
            TypeCode.Decimal => decimal.Round(decimalQuantity, 8),

            _ => decimal.Round(decimalQuantity),
        };
    }

    internal static (ValueType, ValueType) GetAndExchangeRandomQuantity(Enum measureUnit, decimal targetExchangeRate)
    {
        decimal decimalQuantity = GetExchangedRandomQuantity(measureUnit, targetExchangeRate, out ValueType quantity, out TypeCode quantityTypeCode);

        ValueType exchangedQuantity = decimalQuantity.ToQuantity(quantityTypeCode);

        return (quantity, exchangedQuantity);
    }

    private static decimal GetExchangedRandomQuantity(Enum measureUnit, decimal targetExchangeRate, out ValueType quantity, out TypeCode quantityTypeCode)
    {
        decimal decimalQuantity, maxValue;

        do
        {
            quantity = RandomParams.GetRandomValueTypeQuantity();
            decimalQuantity = GetExchangedDecimalQuantity(measureUnit, quantity, targetExchangeRate);

            Type quantityType = quantity.GetType();
            quantityTypeCode = Type.GetTypeCode(quantityType);

            maxValue = GetMaxValue(quantityTypeCode);
        }
        while (Math.Abs(decimalQuantity) > maxValue);

        return decimalQuantity;
    }

    private static decimal GetExchangedDecimalQuantity(Enum measureUnit, ValueType quantity, decimal targetExchangeRate)
    {
        decimal decimalQuantity;
        bool canExchangeDecimal;

        do
        {
            canExchangeDecimal = TryExchange(measureUnit, quantity, targetExchangeRate, out decimalQuantity);
        }
        while (!canExchangeDecimal);

        return decimalQuantity;
    }

    private static bool TryExchange(Enum measureUnit, ValueType quantity, decimal targetExchangeRate, out decimal decimalQuantity)
    {
        decimalQuantity = (decimal)quantity.ToQuantity(TypeCode.Decimal);

        try
        {
            decimalQuantity *= measureUnit.GetExchangeRate();
            decimalQuantity /= targetExchangeRate;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static decimal GetMaxValue(TypeCode quantityTypeCode)
    {
        return quantityTypeCode switch
        {
            TypeCode.Int32 => int.MaxValue,
            TypeCode.UInt32 => uint.MaxValue,
            TypeCode.Int64 => long.MaxValue,
            TypeCode.UInt64 => ulong.MaxValue,

            _ => decimal.MaxValue,
        };
    }
}
#nullable enable
