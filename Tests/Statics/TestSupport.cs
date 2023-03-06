using CsabaDu.FooVar.Measures.Factories;
using CsabaDu.FooVar.Measures.Interfaces.Behaviors;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes;
using CsabaDu.FooVar.Measures.Interfaces.Factories;
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

    internal static bool IsIntegerType(Type type)
    {
        return type == typeof(int) || type == typeof(uint) || type == typeof(long) || type == typeof(ulong);
    }

    internal static decimal GetQuantityDecimalValue(decimal decimalQuantity, Type quantityType)
    {
        if (quantityType == typeof(decimal) || quantityType == typeof(double)) return decimal.Round(decimalQuantity, 8);

        //if (quantityType == typeof(float)) return decimal.Round(decimalQuantity, 4);

        return decimal.Round(decimalQuantity);
    }

    internal static decimal GetQuantityDecimalValue(decimal decimalQuantity, TypeCode quantityTypeCode)
    {
        return quantityTypeCode switch
        {
            //TypeCode.Single => decimal.Round(decimalQuantity, 4),
            TypeCode.Double => decimal.Round(decimalQuantity, 8),
            TypeCode.Decimal => decimal.Round(decimalQuantity, 8),

            _ => decimal.Round(decimalQuantity),
        };
    }

    internal static (ValueType quantity, ValueType exchangedQuantity) GetAndExchangeRandomQuantity(Enum measureUnit, decimal targetExchangeRate)
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
            quantity = GetRandomValueTypeQuantity();
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

    internal static IEnumerable<object[]> GetThreeBaseMeasureArgsWithEachDefaultMeasureUnit()
    {
        foreach (Enum item in ExchangeMeasures.DefaultMeasureUnits)
        {
            yield return new ThreeBaseMeasureArgsToType
            {
                Quantity = GetRandomValueTypeQuantity(),
                MeasureUnit = item,
                ExchangeRate = null,
            }.ToObjectArray();
        }

        foreach (KeyValuePair<Enum, decimal> item in ExchangeMeasures.DefaultRates)
        {
            yield return new ThreeBaseMeasureArgsToType
            {
                Quantity = GetRandomValueTypeQuantity(),
                MeasureUnit = item.Key,
                ExchangeRate = item.Value,
            }.ToObjectArray();
        }
    }

    private struct ThreeBaseMeasureArgsToType
    {
        public ValueType Quantity { get; set; }
        public Enum MeasureUnit { get; set; }
        public decimal? ExchangeRate { get; set; }

        public object[] ToObjectArray()
        {
            return new object[]
            {
                Quantity,
                MeasureUnit,
                ExchangeRate,
            };
        }
    }

    internal static IEnumerable<object[]> GetTwoBaseMeasureArgsWithEachDefaultMeasurement()
    {
        IMeasurementFactory factory = new MeasurementFactory();

        foreach (Enum item in ExchangeMeasures.DefaultMeasureUnits)
        {
            yield return new TwoBaseMeasureArgsToType
            {
                Quantity = GetRandomValueTypeQuantity(),
                Measurement = factory.GetMeasurement(item),
            }.ToObjectArray();
        }

        foreach (KeyValuePair<Enum, decimal> item in ExchangeMeasures.DefaultRates)
        {
            yield return new TwoBaseMeasureArgsToType
            {
                Quantity = GetRandomValueTypeQuantity(),
                Measurement = factory.GetMeasurement(item.Key, item.Value),
            }.ToObjectArray();
        }
    }

    private struct TwoBaseMeasureArgsToType
    {
        public ValueType Quantity { get; set; }
        public IMeasurement Measurement { get; set; }

        public object[] ToObjectArray()
        {
            return new object[]
            {
                Quantity,
                Measurement,
            };
        }
    }
}
#nullable enable
