using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;

namespace CsabaDu.FooVar.Tests.Statics;

internal static class RandomParams
{
    internal enum RandomMeasureUnitType { Default, Constant, }
    private static Type[] NonConstantMeasureUnitTypes => new[] { typeof(Currency), typeof(Pieces), };

    private const int MaxCountOfRandom = 100;

    private static readonly int DefaultMeasureUnitsCount = ExchangeMeasures.DefaultMeasureUnits.Count;
    private static readonly int ConstantMeasureUnitsCount = ExchangeMeasures.ConstantMeasureUnits.Count;

    private static readonly ICollection<Type> ValidQuantityTypes = ValidateMeasures.GetValidQuantityTypes();
    private static readonly ICollection<TypeCode> ValidQuantityTypeCodes = ValidateMeasures.GetValidQuantityTypeCodes();

    private static readonly int ValidQuantityTypesCount = ValidQuantityTypes.Count;
    private static readonly int ValidQuantityTypeCodesCount = ValidQuantityTypeCodes.Count;

    private static readonly int LimitTypeNamesCount = Enum.GetNames(typeof(LimitType)).Length;

    private static readonly Random R = Random.Shared;

    //private static int UlongQuantitiesCount;
    //private static ulong[] RandomUlongQuantities => GetRandomLimitQuantities();
    private static int RandomValueTypeQuantitiesCount;
    private static int RandomPositiveDecimalQuantitiesCount;
    private static int RandomLimitTypesCount;
    private static int RandomDefaultMeasureUnitsCount;
    private static IEnumerable<decimal> RandomPositiveDecimalQuantities
    {
        get
        {
            for (int i = 0; i < MaxCountOfRandom; i++)
            {
                yield return CreateRandomPositiveDecimalQuantity();
            };
        }
    }
    private static IEnumerable<LimitType> RandomLimitTypes
    {
        get
        {
            for (int i = 0; i < MaxCountOfRandom; i++)
            {
                yield return CreateRandomLimitType();
            };
        }
    }

    private static ValueType[] RandomValueTypeQuantities
    {
        get
        {
            ValueType[] randomValueTypeQuantities = new ValueType[MaxCountOfRandom];

            for (int i = 0; i < MaxCountOfRandom; i++)
            {
                randomValueTypeQuantities[i] = CreateRandomValueTypeQuantity();
            };

            return randomValueTypeQuantities;
        }
    }

    private static IEnumerable<Enum> RandomDefaultMeasureUnits
    {
        get
        {
            for (int i = 0; i < MaxCountOfRandom; i++)
            {
                yield return CreateRandomDefaultMeasureUnit();
            };
        }
    }

    internal static Enum GetRandomDefaultMeasureUnit()
    {
        if (RandomDefaultMeasureUnitsCount == 0)
        {
            RandomDefaultMeasureUnitsCount = MaxCountOfRandom;
        }

        return RandomDefaultMeasureUnits.ElementAt(--RandomDefaultMeasureUnitsCount);
    }

    internal static ValueType GetRandomValueTypeQuantity()
    {
        if (RandomValueTypeQuantitiesCount == 0)
        {
            RandomValueTypeQuantitiesCount = MaxCountOfRandom;
        }

        return RandomValueTypeQuantities[--RandomValueTypeQuantitiesCount];
    }

    internal static (ValueType quantity, TypeCode targetTypeCode) GetRandomValueTypeQuantityTargetTypeCodePair(Enum measureUnit)
    {
        TypeCode typeCode = measureUnit.GetQuantityTypeCode();
        ValueType quantity = GetRandomValueTypeQuantity(typeCode);

        TypeCode targetTypeCode;
        bool isWithinTypeLimits;

        do
        {
            targetTypeCode = GetRandomQuantityTypeCode();
            isWithinTypeLimits = quantity.IsWithinTypeLimits(targetTypeCode);
        }
        while (targetTypeCode == typeCode || !isWithinTypeLimits);

        return (quantity, targetTypeCode);
    }

    private static bool IsWithinTypeLimits(this ValueType quantity, TypeCode typeCode)
    {
        ValueType? converted = quantity.ToQuantity(typeCode);

        return converted != null;
    }

    internal static decimal GetRandomPositiveDecimalQuantity()
    {
        if (RandomPositiveDecimalQuantitiesCount == 0)
        {
            RandomPositiveDecimalQuantitiesCount = MaxCountOfRandom;
        }

        return RandomPositiveDecimalQuantities.ElementAt(--RandomPositiveDecimalQuantitiesCount);
    }

    internal static LimitType GetRandomLimitType()
    {
        if (RandomLimitTypesCount == 0)
        {
            RandomLimitTypesCount = MaxCountOfRandom;
        }

        return RandomLimitTypes.ElementAt(--RandomLimitTypesCount);
    }

    private static LimitType CreateRandomLimitType()
    {
        int randomIndex = R.Next(LimitTypeNamesCount);

        return (LimitType)randomIndex;
    }

    internal static Type GetRandomConstantMeasureUnitType()
    {
        ICollection<Type> constantMeasureUnitTypes = ExchangeMeasures.ConstantMeasureUnitTypes;
        int count = constantMeasureUnitTypes.Count;

        int randomIndex = R.Next(count);

        return constantMeasureUnitTypes.ElementAt(randomIndex);
    }

    //private static LimitType[] CreateRandomLimitTypes()
    //{
    //    LimitType[] limitTypes = new LimitType[MaxCountOfRandom];

    //    for (int i = 0; i < MaxCountOfRandom; i++)
    //    {
    //        int randomIndex = R.Next(LimitTypeNamesCount);
    //        limitTypes[i] = (LimitType)randomIndex;
    //    }

    //    return limitTypes;
    //}

    internal static (ValueType quantity, Enum measureUnit) GetRandomBaseMeasureArgs()
    {
        Enum measureUnit = GetRandomDefaultMeasureUnit();
        ValueType quantity = GetRandomValueTypeQuantity(measureUnit);

        return (quantity, measureUnit);
    }

    private static Enum CreateRandomDefaultMeasureUnit()
    {
        int randomIndex = R.Next(DefaultMeasureUnitsCount);

        return ExchangeMeasures.DefaultMeasureUnits.ElementAt(randomIndex);
    }

    internal static Enum GetRandomNonDefaultMeasureUnit()
    {
        int randomIndex = R.Next(NonConstantMeasureUnitTypes.Count());

        Type nonConstantMeasureUnitType = NonConstantMeasureUnitTypes[randomIndex];

        string[] names = Enum.GetNames(nonConstantMeasureUnitType);

        int count = names.Length;

        randomIndex = R.Next(1, count);

        string name = names[randomIndex];

        return (Enum)Enum.Parse(nonConstantMeasureUnitType, name);
    }

    internal static Enum GetRandomConstantMeasureUnit()
    {
        int randomIndex = R.Next(ConstantMeasureUnitsCount);

        return ExchangeMeasures.ConstantMeasureUnits.ElementAt(randomIndex);
    }

    internal static Enum GetRandomConstantMeasureUnit(Type measureUnitType)
    {
        ValidateMeasures.ValidateConstantMeasureUnitType(measureUnitType);

        string[] names = Enum.GetNames(measureUnitType);
        int count = names.Length;

        int randomIndex = R.Next(count);
        string name = names[randomIndex];

        return (Enum)Enum.Parse(measureUnitType, name);
    }

    internal static (Enum measureUnit, Enum sameTypeMeasureUnit) GetRandomConstantMeasureUnitPair(Type? measureUnitType = null)
    {
        measureUnitType ??= GetRandomConstantMeasureUnitType();

        Enum measureUnit = GetRandomConstantMeasureUnit(measureUnitType);
        Enum sameTypeMeasureUnit;

        do
        {
            sameTypeMeasureUnit = GetRandomConstantMeasureUnit(measureUnitType);
        }
        while (sameTypeMeasureUnit == measureUnit);

        return (measureUnit, sameTypeMeasureUnit);
    }

    //internal static ValueType GetRandomValueTypeQuantity()
    //{
    //    if (ValueTypeQuantitiesCount == default)
    //    {
    //        ValueTypeQuantitiesCount = MaxCountOfRandom;
    //    }

    //    return RandomValueTypeQuantities[--ValueTypeQuantitiesCount];
    //}

    internal static ValueType GetRandomNotNegativeValueTypeQuantity()
    {
        TypeCode typeCode = GetRandomQuantityTypeCode();

       return GetRandomNotNegativeValueTypeQuantity(typeCode)!;
    }

    internal static ValueType GetRandomNotNegativeValueTypeQuantity(TypeCode typeCode)
    {
        decimal quantity = CreateRandomDecimalQuantity(typeCode);
        quantity = quantity < 0 ? 0 : quantity;

        return quantity.ToQuantity(typeCode) ?? 1.ToQuantity(typeCode)!;
    }


    //private static ValueType CreateRandomPositiveValueTypeQuantity()
    //{
    //    TypeCode typeCode = GetRandomQuantityTypeCode();
    //    double quantity;

    //    do
    //    {
    //        quantity = CreateRandomDoubleQuantityWithinTypeLimits(typeCode);
    //    }
    //    while (quantity <= 0);

    //    return quantity.ToQuantity(typeCode)!;
    //}

    internal static ValueType GetRandomPositiveValueTypeQuantity()
    {
        decimal quantity = GetRandomPositiveDecimalQuantity();

        TypeCode typeCode = GetRandomQuantityTypeCode();

        return quantity.ToQuantity(typeCode)!;
    }

    internal static ValueType GetRandomNegativeValueTypeQuantity()
    {
        long quantity = R.NextInt64(long.MinValue, 0);

        TypeCode typeCode = GetRandomQuantityTypeCode();

        return quantity.ToQuantity(typeCode) ?? -1;
    }

    internal static decimal GetRandomExchangeRate()
    {
        return GetRandomPositiveDecimalQuantity();
    }

    private static decimal CreateRandomPositiveDecimalQuantity()
    {
        decimal quantity = CreateRandomDecimalQuantity(TypeCode.UInt64);
        quantity += Convert.ToDecimal(R.NextDouble());

        return quantity == 0 ? decimal.One : quantity;
    }

    internal static ValueType GetRandomLimitQuantity()
    {
        ulong quantity = GetRandomUlongQuantity();

        TypeCode typeCode = GetRandomQuantityTypeCode();

        return quantity.ToQuantity(typeCode) ?? 0;
    }

    private static ulong GetRandomUlongQuantity()
    {
        long quantity = R.NextInt64(0, long.MaxValue);

        return (ulong)quantity - 1 + (ulong)quantity;
    }

    internal static ValueType GetRandomValueTypeQuantity(TypeCode typeCode)
    {
        decimal quantity = CreateRandomDecimalQuantity(typeCode);

        return quantity.ToQuantity(typeCode) ?? 0.ToQuantity(typeCode)!;
    }

    private static ValueType CreateRandomValueTypeQuantity()
    {
        TypeCode typeCode = GetRandomQuantityTypeCode();

        return GetRandomValueTypeQuantity(typeCode);

    }

    internal static ValueType GetRandomValueTypeQuantity(Enum measureUnit)
    {
        TypeCode typeCode = measureUnit.GetQuantityTypeCode();

        return GetRandomValueTypeQuantity(typeCode);
    }

    internal static Type GetRandomQuantityType()
    {
        int typeIndex = R.Next(ValidQuantityTypesCount);

        return ValidQuantityTypes.ElementAt(typeIndex);
    }

    internal static TypeCode GetRandomQuantityTypeCode()
    {
        int typeCodeIndex = R.Next(ValidQuantityTypeCodesCount);

        return ValidQuantityTypeCodes.ElementAt(typeCodeIndex);
    }

    private static decimal CreateRandomDecimalQuantity(TypeCode typeCode = TypeCode.Decimal)
    {
        return typeCode switch
        {
            TypeCode.Int32 => R.Next(int.MinValue, int.MaxValue),
            TypeCode.UInt32 => (decimal)R.Next() + R.Next(),
            TypeCode.Int64 => R.NextInt64(long.MinValue, long.MaxValue),
            TypeCode.UInt64 => (decimal)R.NextInt64() + R.NextInt64(),
            TypeCode.Double or
            TypeCode.Decimal => Convert.ToDecimal(R.NextDouble()) + R.NextInt64(long.MinValue, long.MaxValue) + R.NextInt64(),

            _ => throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null),
        };
    }

    //private static decimal CreateRandomDecimalQuantity()
    //{
    //    return CreateRandomDecimalQuantity(TypeCode.Decimal);
    //}

    //private static double CreateRandomDoubleQuantity()
    //{
    //    double quantity = default;

    //    for (int i = 0; i < 4; i++)
    //    {
    //        long random = R.NextInt64(long.MinValue, long.MaxValue);
    //        random *= R.Next(int.MinValue, int.MaxValue);

    //        quantity += random;
    //    }

    //    switch (R.Next(2))
    //    {
    //        case 0:
    //            quantity -= R.NextDouble();
    //            break;
    //        default:
    //            quantity += R.NextDouble();
    //            break;
    //    }

    //    return quantity;
    //}

    //private static decimal CreateRandomDecimalQuantityWithinTypeLimits(TypeCode typeCode)
    //{
    //    var (minValue, maxValue) = ValidateMeasures.GetQuantityValueLimits(typeCode);
    //    decimal quantity;
    //    bool fitsIn;

    //    do
    //    {
    //        quantity = (decimal?)CreateRandomDoubleQuantity().ToQuantity(TypeCode.Decimal) ?? decimal.One;
    //        fitsIn = quantity >= minValue && quantity <= maxValue;
    //    }
    //    while (!fitsIn);

    //    return quantity;
    //}

    internal static Enum GetRandomNotDefinedMeasureUnit()
    {
        ICollection<Type> measureUnitTypes = ExchangeMeasures.ConstantMeasureUnitTypes;
        int count = measureUnitTypes.Count;

        switch (R.Next(count))
        {
            case 0:
                count = Enum.GetNames(typeof(AreaUnit)).Length;
                return (AreaUnit)count;
            case 1:
                count = Enum.GetNames(typeof(DistanceUnit)).Length;
                return (DistanceUnit)count;
            case 2:
                count = Enum.GetNames(typeof(ExtentUnit)).Length;
                return (ExtentUnit)count;
            case 3:
                count = Enum.GetNames(typeof(TimeUnit)).Length;
                return (TimeUnit)count;
            case 4:
                count = Enum.GetNames(typeof(VolumeUnit)).Length;
                return (VolumeUnit)count;
            default:
                count = Enum.GetNames(typeof(WeightUnit)).Length;
                return (WeightUnit)count;
        }
    }

    internal static (ValueType quantity, ValueType exchanged) GetRandomExchangedQuantityPair(Enum measureUnit, decimal targetExchangeRate)
    {
        ValueType quantity;
        TypeCode typeCode;
        decimal exchangedDecimalQuantity, minValue, maxValue;
        bool canExchange;

        do
        {
            typeCode = measureUnit.GetQuantityTypeCode();
            quantity = GetRandomValueTypeQuantity(typeCode);
            (minValue, maxValue) = ValidateMeasures.GetQuantityValueLimits(typeCode);
            canExchange = TryExchange(measureUnit, quantity, targetExchangeRate, out exchangedDecimalQuantity)
                && exchangedDecimalQuantity >= minValue
                && exchangedDecimalQuantity <= maxValue;
        }
        while (!canExchange);

        ValueType exchanged = exchangedDecimalQuantity.ToQuantity(typeCode)!;

        return (quantity, exchanged);
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
        decimalQuantity = (decimal)quantity.ToQuantity(TypeCode.Decimal)!;
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
}
