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
    private static readonly int ValidQuantityTypeCodeCount = ValidQuantityTypeCodes.Count;

    private static readonly int LimitTypeNamesCount = Enum.GetNames(typeof(LimitType)).Length;

    private static readonly Random R = Random.Shared;

    //private static uint UlongQuantitiesCount;
    //private static uint ValueTypeQuantitiesCount;
    //private static uint PositiveDecimalQuantitiesCount;
    //private static int LimitTypesCount;

    //private static decimal[] RandomPositiveDecimalQuantities => GetRandomDenominatorQuantities();
    //private static ValueType[] RandomValueTypeQuantities => GetRandomValueTypeQuantities();
    //private static ulong[] RandomUlongQuantities => GetRandomLimitQuantities();
    //private static LimitType[] RandomLimitTypes => CreateRandomLimitTypes();

    internal static LimitType GetRandomLimitType()
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
        decimal quantity = CreateRandomDecimalQuantityWithinTypeLimits(typeCode);
        quantity = quantity < 0 ? 0 : quantity;

       return quantity.ToQuantity(typeCode)!;
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
        decimal quantity = GetRandomPositiveDecimal();

        TypeCode typeCode = GetRandomQuantityTypeCode();

        return quantity.ToQuantity(typeCode)!;
    }

    internal static decimal GetRandomExchangeRate()
    {
        return GetRandomPositiveDecimal();
    }

    private static decimal GetRandomPositiveDecimal()
    {
        decimal quantity = CreateRandomDecimalQuantityWithinTypeLimits(TypeCode.Decimal);

        return quantity > 0 ? quantity : 1;
    }
    //private static decimal GetRandomPositiveDecimal()
    //{
    //    if (PositiveDecimalQuantitiesCount == default)
    //    {
    //        PositiveDecimalQuantitiesCount = MaxCountOfRandom;
    //    }

    //    return RandomPositiveDecimalQuantities[--PositiveDecimalQuantitiesCount];
    //}

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

    //internal static ulong GetRandomUlongQuantity()
    //{
    //    if (UlongQuantitiesCount == default)
    //    {
    //        UlongQuantitiesCount = MaxCountOfRandom;
    //    }

    //    return RandomUlongQuantities[--UlongQuantitiesCount];
    //}

    //internal static LimitType GetRandomLimitType()
    //{
    //    if (LimitTypesCount == default)
    //    {
    //        LimitTypesCount = MaxCountOfRandom;
    //    }

    //    return RandomLimitTypes[--LimitTypesCount];
    //}

    //private static ValueType[] GetRandomValueTypeQuantities()
    //{
    //    ValueType[] randomValueTypeQuantities = new ValueType[MaxCountOfRandom];

    //    for (int i = 0; i < MaxCountOfRandom; i++)
    //    {
    //        randomValueTypeQuantities[i] = CreateRandomValueTypeQuantity();
    //    }

    //    return randomValueTypeQuantities;
    //}

    //private static decimal[] GetRandomDenominatorQuantities()
    //{
    //    decimal[] randomDenominatorQuantities = new decimal[MaxCountOfRandom];

    //    for (int i = 0; i < MaxCountOfRandom; i++)
    //    {
    //        randomDenominatorQuantities[i] = CreateRandomDenominatorQuantity();
    //    }

    //    return randomDenominatorQuantities;
    //}

    //private static ulong[] GetRandomLimitQuantities()
    //{
    //    ulong[] randomLimitQuantities = new ulong[MaxCountOfRandom];

    //    for (int i = 0; i < MaxCountOfRandom; i++)
    //    {
    //        randomLimitQuantities[i] = CreateRandomLimitQuantity();
    //    }

    //    return randomLimitQuantities;
    //}

    internal static ValueType GetRandomValueTypeQuantity(TypeCode? typeCode = null)
    {
        if (typeCode is not TypeCode notNullTypeCode)
        {
            notNullTypeCode = GetRandomQuantityTypeCode();
        }

        return CreateRandomValueTypeQuantity(notNullTypeCode)!;
    }

    //private static decimal CreateRandomDenominatorQuantity()
    //{
    //    //double quantity = CreateRandomLimitQuantity();

    //    double quantity = R.NextDouble();

    //    quantity = quantity >= 0 ? quantity : 1;

    //    return (decimal)quantity.ToQuantity(TypeCode.Decimal)!;
    //}

    private static ulong CreateRandomLimitQuantity()
    {
        return (ulong)CreateRandomDoubleQuantityWithinTypeLimits(TypeCode.UInt64);
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

    private static decimal CreateRandomDecimalQuantityWithinTypeLimits(TypeCode typeCode)
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
                double quantity = CreateRandomDoubleQuantityWithinTypeLimits(typeCode);
                return (decimal)quantity.ToQuantity(TypeCode.Decimal)!;

            default:
                throw new InvalidOperationException(nameof(typeCode));
        }
    }

    private static ValueType CreateRandomValueTypeQuantity(TypeCode typeCode)
    {
        decimal quantity = CreateRandomDecimalQuantityWithinTypeLimits(typeCode);

        return quantity.ToQuantity(typeCode)!;
    }

    private static double CreateRandomDoubleQuantityWithinTypeLimits(TypeCode typeCode)
    {
        TypeCode randomLimitsTypeCode = TypeCode.Int64;
        var (minValue, maxValue) = ValidateMeasures.GetQuantityValueLimits(randomLimitsTypeCode);

        long minRandom = (long)minValue.ToQuantity(randomLimitsTypeCode)!;
        long maxRandom = (long)maxValue.ToQuantity(randomLimitsTypeCode)!;

        decimal quantity = R.NextInt64(minRandom, maxRandom) + (decimal)R.NextDouble().ToQuantity(TypeCode.Decimal)!;
        (minValue, maxValue) = ValidateMeasures.GetQuantityValueLimits(typeCode);
        quantity = (quantity >= minValue && quantity <= maxValue) ? quantity : 0;

        return (double)quantity.ToQuantity(TypeCode.Double)!;
    }

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

    internal static ValueType GetRandomNegativeQuantity()
    {
        long quantity = R.NextInt64(long.MinValue, 0);

        TypeCode typeCode = GetRandomQuantityTypeCode();

        return quantity.ToQuantity(typeCode) ?? -1;
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

        ValueType exchangedQuantity = exchangedDecimalQuantity.ToQuantity(typeCode)!;

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
