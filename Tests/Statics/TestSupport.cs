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
            Enum measureUnit = item;
            decimal? exchangeRate = null;

            yield return MeasureUnitExchangeRatePair_ToObjectArray(measureUnit, exchangeRate);
        }

        foreach (KeyValuePair<Enum, decimal> item in ExchangeMeasures.DefaultRates)
        {
            Enum measureUnit = item.Key;
            decimal? exchangeRate = item.Value;

            yield return MeasureUnitExchangeRatePair_ToObjectArray(measureUnit, exchangeRate);
        }
    }

    private static object[] MeasureUnitExchangeRatePair_ToObjectArray(Enum measureUnit, decimal? exchangeRate)
    {
        return new MeasureUnitExchangeRatePair
        {
            MeasureUnit = measureUnit,
            ExchangeRate = exchangeRate,
        }
        .ToObjectArray();
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

    internal static IEnumerable<object[]> GetInvalidTypeQuantityArgs()
    {
        ValueType quantity = default(bool); // bool
        yield return ValueTypeQuantity_ToObjectArray(quantity);

        quantity = default(TypeCode); // Enum
        yield return ValueTypeQuantity_ToObjectArray(quantity);

        quantity = default(char); // char
        yield return ValueTypeQuantity_ToObjectArray(quantity);

        quantity = default(IntPtr); // IntPtr
        yield return ValueTypeQuantity_ToObjectArray(quantity);

        quantity = default(UIntPtr); // UIntPtr
        yield return ValueTypeQuantity_ToObjectArray(quantity);

        quantity = default(DateTime); // DateTime
        yield return ValueTypeQuantity_ToObjectArray(quantity);

        quantity = default(byte); // byte
        yield return ValueTypeQuantity_ToObjectArray(quantity);

        quantity = default(sbyte); // sbyte
        yield return ValueTypeQuantity_ToObjectArray(quantity);

        quantity = default(short); // short
        yield return ValueTypeQuantity_ToObjectArray(quantity);

        quantity = default(ushort); // ushort
        yield return ValueTypeQuantity_ToObjectArray(quantity);

        quantity = default(float); // float
        yield return ValueTypeQuantity_ToObjectArray(quantity);

        quantity = Convert.ToDouble(decimal.MaxValue) + double.Epsilon; // exceeding max value
        yield return ValueTypeQuantity_ToObjectArray(quantity);

        quantity = Convert.ToDouble(decimal.MinValue) - double.Epsilon; // exceeding min value
        yield return ValueTypeQuantity_ToObjectArray(quantity);
    }

    private static object[] ValueTypeQuantity_ToObjectArray(ValueType quantity)
    {
        return new ValueTypeQuantity { Quantity = quantity }.ToObjectArray();
    }

    private struct ValueTypeQuantity
    {
        internal ValueType Quantity { get; init; }

        internal object[] ToObjectArray()
        {
            return new object[]
            {
                Quantity,
            };
        }
    }

}
#nullable enable
