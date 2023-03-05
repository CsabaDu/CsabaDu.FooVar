namespace CsabaDu.FooVar.Tests.Statics;

internal static class RandomParams
{
    internal enum RandomMeasureUnitType { Default, Constant, }
    private static Type[] NonDefaultMeasureUnitTypes => new[] { typeof(Currency), typeof(Pieces), };

    private const uint MaxCountOfRandom = 100;

    private static readonly int DefaultMeasureUnitsCount = ExchangeMeasures.DefaultMeasureUnits.Count;
    private static readonly int ConstantMeasureUnitsCount = ExchangeMeasures.ConstantMeasureUnits.Count;
    private static readonly int ValidQuantityTypesCount = ValidateMeasures.GetValidQuantityTypes().Count;
    private static readonly int LimitTypeNamesCount = Enum.GetNames(typeof(LimitType)).Length;

    private const int QuantityMinValue = (int.MinValue) / 2;
    private const int QuantityMaxValue = (int.MaxValue - 1) / 2;

    private static readonly ICollection<Type> ValidQuantityTypes = ValidateMeasures.GetValidQuantityTypes();
    private static readonly Random R = Random.Shared;

    private static uint LimitQuantitiesCount;
    private static uint ValueTypeQuantitiesCount;
    private static uint DenominatorQuantitiesCount;
    private static uint LimitTypesCount;

    private static decimal[] RandomDenominatorQuantities => GetRandomDenominatorQuantities();
    private static ValueType[] RandomValueTypeQuantities => GetRandomValueTypeQuantities();
    private static ulong[] RandomLimitQuantities => GetRandomLimitQuantities();
    private static LimitType[] RandomLimitTypes => GetRandomLimitTypes();

    private static LimitType[] GetRandomLimitTypes()
    {
        LimitType[] limitTypes = new LimitType[MaxCountOfRandom];

        for (int i = 0; i < MaxCountOfRandom; i++)
        {
            int randomIndex = R.Next(LimitTypeNamesCount);
            limitTypes[i] = (LimitType)randomIndex;
        }

        return limitTypes;
    }

    internal static (ValueType, Enum) GetRandomBaseMeasureArgs(RandomMeasureUnitType randomMeasureUnitType = default)
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
        ValueType quantity = GetRandomValueTypeQuantity();

        while ((double)quantity.ToQuantity(typeof(double))! < 0)
        {
            quantity = GetRandomValueTypeQuantity();
        }

        return quantity;
    }

    internal static ValueType GetRandomPositiveValueTypeQuantity()
    {
        ValueType quantity = GetRandomNotNegativeValueTypeQuantity();

        while ((double)quantity.ToQuantity(typeof(double))! == 0)
        {
            quantity = GetRandomNotNegativeValueTypeQuantity();
        }

        return quantity;
    }

    internal static ValueType GetRandomDenominatorQuantity()
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
        if (DenominatorQuantitiesCount == default)
        {
            DenominatorQuantitiesCount = MaxCountOfRandom;
        }

        return RandomDenominatorQuantities[--DenominatorQuantitiesCount];
    }

    internal static ValueType GetRandomLimitQuantity()
    {
        ulong randomULongQuantity = GetRandomUInt64Quantity();

        Type quantityType = GetRandomQuantityType();

        return randomULongQuantity.ToQuantity(quantityType)!;
    }

    internal static ulong GetRandomUInt64Quantity()
    {
        if (LimitQuantitiesCount == default)
        {
            LimitQuantitiesCount = MaxCountOfRandom;
        }

        return RandomLimitQuantities[--LimitQuantitiesCount];
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
        Type randomType = GetRandomQuantityType();

        if (randomType == typeof(uint)) return CreateRandomLimitQuantity();
        else if (randomType == typeof(ulong)) return (ulong)CreateRandomLimitQuantity();

        double quantity = R.Next(QuantityMinValue, QuantityMaxValue);

        return quantity.ToQuantity(randomType)!;
    }

    private static decimal CreateRandomDenominatorQuantity()
    {
        double quantity = CreateRandomLimitQuantity();

        quantity -= R.NextDouble();

        quantity = quantity >= 1 ? quantity : 1;

        return (decimal)quantity.ToQuantity(typeof(decimal))!;
    }

    private static uint CreateRandomLimitQuantity()
    {
        return (uint)R.Next(int.MaxValue);
    }

    internal static Type GetRandomQuantityType()
    {
        int typeIndex = R.Next(ValidQuantityTypesCount);
        return ValidQuantityTypes.ElementAt(typeIndex);
    }
}
