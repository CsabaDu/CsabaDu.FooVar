namespace CsabaDu.FooVar.Tests.Statics;

internal static class RandomParams
{
    internal enum RandomMeasureUnitType { Default, Constant, }
    private static Type[] NonDefaultMeasureUnitTypes => new[] { typeof(Currency), typeof(Pieces), };

    private const int MaxCountOfRandom = 100;

    private static readonly int DefaultMeasureUnitsCount = ExchangeMeasures.DefaultMeasureUnits.Count;
    private static readonly int ConstantMeasureUnitsCount = ExchangeMeasures.ConstantMeasureUnits.Count;

    private static readonly ICollection<Type> ValidQuantityTypes = ValidateMeasures.GetValidQuantityTypes();
    private static readonly ICollection<TypeCode> ValidQuantityTypeCodes = ValidateMeasures.GetValidQuantityTypeCodes();

    private static readonly int ValidQuantityTypesCount = ValidQuantityTypes.Count;
    private static readonly int ValidQuantityTypeCodeCount = ValidQuantityTypeCodes.Count;

    private static readonly int LimitTypeNamesCount = Enum.GetNames(typeof(LimitType)).Length;

    //private const int QuantityMinValue = (int.MinValue) / 2;
    //private const int QuantityMaxValue = (int.MaxValue - 1) / 2;


    private static readonly Random R = Random.Shared;

    private static uint UlongQuantitiesCount;
    private static uint ValueTypeQuantitiesCount;
    private static uint PositiveDecimalQuantitiesCount;
    private static int LimitTypesCount;

    private static decimal[] RandomPositiveDecimalQuantities => GetRandomDenominatorQuantities();
    private static ValueType[] RandomValueTypeQuantities => GetRandomValueTypeQuantities();
    private static ulong[] RandomUlongQuantities => GetRandomLimitQuantities();
    private static LimitType[] RandomLimitTypes => CreateRandomLimitTypes();

    private static LimitType[] CreateRandomLimitTypes()
    {
        LimitType[] limitTypes = new LimitType[MaxCountOfRandom];

        for (int i = 0; i < MaxCountOfRandom; i++)
        {
            int randomIndex = R.Next(LimitTypeNamesCount);
            limitTypes[i] = (LimitType)randomIndex;
        }

        return limitTypes;
    }

    //private static IEnumerable<LimitType> CreateRandomLimitTypes()
    //{
    //    for (int i = 0; i < LimitTypeNamesCount; i++)
    //    {
    //        int randomIndex = R.Next(LimitTypeNamesCount);

    //        yield return (LimitType)randomIndex;
    //    }
    //}

    internal static (ValueType quantity, Enum measureUnit) GetRandomBaseMeasureArgs(RandomMeasureUnitType randomMeasureUnitType = default)
    {
        Enum measureUnit = GetRandomMeasureUnit(randomMeasureUnitType);
        ValueType quantity = GetRandomValueTypeQuantity();

        return (quantity, measureUnit);
    }

    internal static Enum GetRandomDefaultMeasureUnit()
    {
        int randomIndex = R.Next(DefaultMeasureUnitsCount);

        return ExchangeMeasures.DefaultMeasureUnits.ElementAt(randomIndex);
    }

    internal static Enum GetRandomNonDefaultMeasureUnit()
    {
        int randomIndex = R.Next(2);

        Type nonDefaultMeasureUnitType = NonDefaultMeasureUnitTypes[randomIndex];

        string[] names = Enum.GetNames(nonDefaultMeasureUnitType);

        int count = names.Length;

        randomIndex = R.Next(1, count);

        string name = names[randomIndex];

        return (Enum)Enum.Parse(nonDefaultMeasureUnitType, name);
    }

    internal static Enum GetRandomConstantMeasureUnit()
    {
        int randomIndex = R.Next(ConstantMeasureUnitsCount);

        return ExchangeMeasures.ConstantMeasureUnits.ElementAt(randomIndex);
    }

    internal static Enum GetRandomMeasureUnit(RandomMeasureUnitType randomMeasureUnitType)
    {
        return randomMeasureUnitType switch
        {
            RandomMeasureUnitType.Default => GetRandomDefaultMeasureUnit(),
            RandomMeasureUnitType.Constant => GetRandomConstantMeasureUnit(),

            _ => throw new ArgumentOutOfRangeException(nameof(randomMeasureUnitType)),
        };
    }

    internal static ValueType GetRandomValueTypeQuantity()
    {
        if (ValueTypeQuantitiesCount == default)
        {
            ValueTypeQuantitiesCount = MaxCountOfRandom;
        }

        return RandomValueTypeQuantities[--ValueTypeQuantitiesCount];
    }

    internal static ValueType GetRandomNotNegativeValueTypeQuantity()
    {
        TypeCode randomTypeCode = GetRandomQuantityTypeCode();
        double quantity;

        do
        {
            quantity = CreateRandomQuantityWithinTypeLimits(randomTypeCode);
        }
        while (quantity < 0);

       return quantity.ToQuantity(randomTypeCode)!;
    }

    private static ValueType CreateRandomPositiveValueTypeQuantity()
    {
        TypeCode randomTypeCode = GetRandomQuantityTypeCode();
        double quantity;

        do
        {
            quantity = CreateRandomQuantityWithinTypeLimits(randomTypeCode);
        }
        while (quantity <= 0);

        return quantity.ToQuantity(randomTypeCode)!;
    }

    internal static ValueType GetRandomPositiveValueTypeQuantity()
    {
        decimal randomDecimalQuantity = GetRandomPositiveDecimal();

        Type quantityType = GetRandomQuantityType();

        return randomDecimalQuantity.ToQuantity(quantityType)!;
    }

    internal static decimal GetRandomExchangeRate()
    {
        return GetRandomPositiveDecimal();
    }

    private static decimal GetRandomPositiveDecimal()
    {
        if (PositiveDecimalQuantitiesCount == default)
        {
            PositiveDecimalQuantitiesCount = MaxCountOfRandom;
        }

        return RandomPositiveDecimalQuantities[--PositiveDecimalQuantitiesCount];
    }

    internal static ValueType GetRandomLimitQuantity()
    {
        ulong randomUlongQuantity = GetRandomUlongQuantity();

        TypeCode quantityTypeCode = GetRandomQuantityTypeCode();

        return randomUlongQuantity.ToQuantity(quantityTypeCode)!;
    }

    internal static ulong GetRandomUlongQuantity()
    {
        if (UlongQuantitiesCount == default)
        {
            UlongQuantitiesCount = MaxCountOfRandom;
        }

        return RandomUlongQuantities[--UlongQuantitiesCount];
    }

    internal static LimitType GetRandomLimitType()
    {
        if (LimitTypesCount == default)
        {
            LimitTypesCount = MaxCountOfRandom;
        }

        return RandomLimitTypes[--LimitTypesCount];
    }

    private static ValueType[] GetRandomValueTypeQuantities()
    {
        ValueType[] randomValueTypeQuantities = new ValueType[MaxCountOfRandom];

        for (int i = 0; i < MaxCountOfRandom; i++)
        {
            randomValueTypeQuantities[i] = CreateRandomValueTypeQuantity();
        }

        return randomValueTypeQuantities;
    }

    private static decimal[] GetRandomDenominatorQuantities()
    {
        decimal[] randomDenominatorQuantities = new decimal[MaxCountOfRandom];

        for (int i = 0; i < MaxCountOfRandom; i++)
        {
            randomDenominatorQuantities[i] = CreateRandomDenominatorQuantity();
        }

        return randomDenominatorQuantities;
    }

    private static ulong[] GetRandomLimitQuantities()
    {
        ulong[] randomLimitQuantities = new ulong[MaxCountOfRandom];

        for (int i = 0; i < MaxCountOfRandom; i++)
        {
            randomLimitQuantities[i] = CreateRandomLimitQuantity();
        }

        return randomLimitQuantities;
    }

    private static ValueType CreateRandomValueTypeQuantity()
    {
        TypeCode randomTypeCode = GetRandomQuantityTypeCode();

        decimal quantity = CreateRandomDecimalQuantity(randomTypeCode);

        return quantity.ToQuantity(randomTypeCode)!;
    }

    private static decimal CreateRandomDenominatorQuantity()
    {
        double quantity = CreateRandomLimitQuantity();

        quantity -= R.NextDouble();

        quantity = quantity >= 1 ? quantity : 1;

        return (decimal)quantity.ToQuantity(TypeCode.Decimal)!;
    }

    private static ulong CreateRandomLimitQuantity()
    {
        return (ulong)CreateRandomQuantityWithinTypeLimits(TypeCode.UInt64);
    }

    internal static Type GetRandomQuantityType()
    {
        int typeIndex = R.Next(ValidQuantityTypesCount);
        return ValidQuantityTypes.ElementAt(typeIndex);
    }

    internal static TypeCode GetRandomQuantityTypeCode()
    {
        int typeCodeIndex = R.Next(ValidQuantityTypeCodeCount);

        return ValidQuantityTypeCodes.ElementAt(typeCodeIndex);
    }

    //internal static (decimal minValue, decimal maxValue) GetQuantityValueLimits(TypeCode typeCode)
    //{
    //    switch (typeCode)
    //    {
    //        case TypeCode.Int32:
    //            return new (int.MinValue, int.MaxValue);
    //        case TypeCode.UInt32:
    //            return new(uint.MinValue, uint.MaxValue);
    //        case TypeCode.Int64:
    //            return new(long.MinValue, long.MaxValue);
    //        case TypeCode.UInt64:
    //            return new(ulong.MinValue, ulong.MaxValue);
    //        default:
    //            return new(decimal.MinValue, decimal.MaxValue);
    //    }
    //}

    private static decimal CreateRandomDecimalQuantity(TypeCode typeCode)
    {
        switch (typeCode)
        {
            case TypeCode.Int32:
                return R.Next();
            case TypeCode.Int64:
                return R.NextInt64();
            case TypeCode.UInt32:
            case TypeCode.UInt64:
            case TypeCode.Double:
            case TypeCode.Decimal:
                double quantity = CreateRandomQuantityWithinTypeLimits(typeCode);
                return (decimal)quantity.ToQuantity(TypeCode.Decimal)!;
            default:
                throw new InvalidOperationException(nameof(typeCode));
        }
    }

    private static double CreateRandomQuantityWithinTypeLimits(TypeCode typeCode)
    {
        var (minValue, maxValue) = ValidateMeasures.GetQuantityValueLimits(typeCode);

        double min = (double)minValue.ToQuantity(TypeCode.Double)!;
        double max = (double)maxValue.ToQuantity(TypeCode.Double)!;
        double quantity;

        do
        {
            quantity = R.NextDouble();
        }
        while (quantity > max || quantity < min);

        return quantity;
    }
}
