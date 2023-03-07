using static CsabaDu.FooVar.Tests.Statics.RandomParams;

namespace CsabaDu.FooVar.Tests.Statics;

#nullable disable
internal static class TestSupport
{

    internal static void RemoveIfNonDefaultMeasureUnit(Enum measureUnit)
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
            RemoveIfNonDefaultMeasureUnit(measureUnit);
        }
    }

    internal static (ValueType quantity, ValueType exchangedQuantity) GetRandomExchangedQuantityPair(Enum measureUnit, decimal targetExchangeRate)
    {
        ValueType quantity;
        TypeCode typeCode;
        decimal exchangedDecimalQuantity, minValue, maxValue;

        do
        {
            quantity = GetRandomValueTypeQuantity();
            exchangedDecimalQuantity = GetExchangedDecimalQuantity(measureUnit, quantity, targetExchangeRate);
            typeCode = Type.GetTypeCode(quantity.GetType());
            (minValue, maxValue) = ValidateMeasures.GetQuantityValueLimits(typeCode);
        }
        while (exchangedDecimalQuantity < minValue || exchangedDecimalQuantity > maxValue);

        ValueType exchangedQuantity = exchangedDecimalQuantity.ToQuantity(typeCode);

        return (quantity, exchangedQuantity);
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
            decimalQuantity /= targetExchangeRate;
            decimalQuantity *= exchangeRate;
            return true;
        }
        catch (OverflowException)
        {
            try
            {
                decimalQuantity *= exchangeRate;
                decimalQuantity /= targetExchangeRate;
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

    internal static IEnumerable<object[]> GetAllDefaultMeasureUnitExchangeRatePairs()
    {
        foreach (Enum item in ExchangeMeasures.DefaultMeasureUnits)
        {
            yield return new MeasureUnitExchangeRatePair
            {
                MeasureUnit = item,
                ExchangeRate = null,
            }
            .ToObjectArray();
        }

        foreach (KeyValuePair<Enum, decimal> item in ExchangeMeasures.DefaultRates)
        {
            yield return new MeasureUnitExchangeRatePair
            {
                MeasureUnit = item.Key,
                ExchangeRate = item.Value,
            }
            .ToObjectArray();
        }
    }

    private struct MeasureUnitExchangeRatePair
    {
        internal Enum MeasureUnit { get; set; }
        internal decimal? ExchangeRate { get; set; }

        internal object[] ToObjectArray()
        {
            return new object[]
            {
                MeasureUnit,
                ExchangeRate,
            };
        }
    }
}
#nullable enable
