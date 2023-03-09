namespace CsabaDu.FooVar.Tests.Statics;

#nullable disable
internal static class TestSupport
{
    #region DynamicDataSource
    #region Structs
    private readonly struct EnumMeasureUnit
    {
        internal Enum MeasureUnit { get; init;}

        internal object[] ToObjectArray()
        {
            return new object[]
            {
                MeasureUnit,
            };
        }

    }

    private readonly struct MeasureUnitExchangeRatePair
    {
        internal Enum MeasureUnit { get; init; }
        internal decimal? ExchangeRate { get; init; }

        internal object[] ToObjectArray()
        {
            return new object[]
            {
                MeasureUnit,
                ExchangeRate,
            };
        }
    }

    private readonly struct QuantityTypeCode
    {
        internal TypeCode TypeCode { get; init; }

        internal object[] ToObjectArray()
        {
            return new object[]
            {
                TypeCode,
            };
        }
    }

    private readonly struct ValueTypeQuantity
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
    #endregion

    #region DynamicData methods
    internal static IEnumerable<object[]> GetUnsignedIntegerTypeCodeArg()
    {
        TypeCode typeCode = TypeCode.UInt32; // uint
        yield return ValueTypeQuantity_ToObjectArray(typeCode);

        typeCode = TypeCode.UInt64; // ulong
        yield return ValueTypeQuantity_ToObjectArray(typeCode);
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

    internal static IEnumerable<object[]> GetInvalidMeasureUnitArg()
    {
        Enum measureUnit = GetRandomLimitType(); // Not MeasureUnit-type Enum
        yield return EnumMeasureUnit_ToObjectArray(measureUnit);

        measureUnit = GetRandomNotDefinedMeasureUnit(); // Not defined MeasureUnit
        yield return EnumMeasureUnit_ToObjectArray(measureUnit);

        measureUnit = GetRandomNonDefaultMeasureUnit(); // MeasureUnit does not have ExchangeRate
        yield return EnumMeasureUnit_ToObjectArray(measureUnit);
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
    #endregion

    #region ToBojectArray
    private static object[] MeasureUnitExchangeRatePair_ToObjectArray(Enum measureUnit, decimal? exchangeRate)
    {
        return new MeasureUnitExchangeRatePair
        {
            MeasureUnit = measureUnit,
            ExchangeRate = exchangeRate,
        }
        .ToObjectArray();
    }

    private static object[] QuantityTypeCode_ToObjectArray(TypeCode typeCode)
    {
        return new QuantityTypeCode { TypeCode = typeCode, }.ToObjectArray();
    }

    private static object[] ValueTypeQuantity_ToObjectArray(ValueType quantity)
    {
        return new ValueTypeQuantity { Quantity = quantity }.ToObjectArray();
    }

    private static object[] EnumMeasureUnit_ToObjectArray(Enum measureUnit)
    {
        return new EnumMeasureUnit { MeasureUnit = measureUnit }.ToObjectArray();
    }
    #endregion
    #endregion

    #region Restore non-constant params
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
    #endregion

}
#nullable enable
