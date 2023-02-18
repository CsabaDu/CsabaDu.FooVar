namespace CsabaDu.FooVar.Tests.Statics;

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

    internal static decimal GetQuantityDecimalValue(decimal quantityValue, Type type)
    {
        if (type == typeof(decimal) || type == typeof(double)) return decimal.Round(quantityValue, 8);

        if (type == typeof(float)) return decimal.Round(quantityValue, 4);

        return decimal.Round(quantityValue);
    }
}
