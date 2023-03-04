﻿namespace CsabaDu.FooVar.Tests.Statics;

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
            quantityTypeCode = Type.GetTypeCode(quantity.GetType());
            maxValue = ConvertMeasures.GetMaxValue(quantityTypeCode);
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
        decimal exchangeRate = measureUnit.GetExchangeRate();

        try
        {
            decimalQuantity *= exchangeRate;
            decimalQuantity /= targetExchangeRate;
            return true;
        }
        catch (OverflowException)
        {
            try
            {
                decimalQuantity /= targetExchangeRate;
                decimalQuantity *= exchangeRate;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }
}
#nullable enable
