using CsabaDu.FooVar.Measures.Interfaces.DataTypes;
using CsabaDu.FooVar.Measures.Interfaces.Factories;
using CsabaDu.FooVar.Tests.Fakes.Measures;

namespace CsabaDu.FooVar.Tests.UnitTests.Measures.DataTypes;

#nullable disable
[TestClass]
public class BaseMeasureTests
{
    #region Fields
    private IMeasurementFactory _factory;
    private IBaseMeasure _baseMeasure;
    #endregion

    #region TestInitialize
    [TestInitialize]
    public void InitializeMaseMeasureTests()
    {
        RestoreDefaultMeasureUnits();

        Enum measureUnit =  DefaultSampleMeasureUnit;
        ValueType quantity =  0;

        _baseMeasure = new BaseMeasureChild(quantity, measureUnit, null);
        _factory = _baseMeasure.MeasurementFactory;
    }
    #endregion

    #region Private methods
    #region DynamicData
    private static IEnumerable<object[]> GetInvalidTypeQuantityArgs()
    {
        return TestSupport.GetInvalidTypeQuantityArgs();
    }

    private static IEnumerable<object[]> GetAllDefaultMeasureUnitExchangeRatePairs()
    {
        return TestSupport.GetAllDefaultMeasureUnitExchangeRatePairs();
    }

    private static IEnumerable<object[]> GetUnsignedIntegerTypeCodeArg()
    {
        return TestSupport.GetUnsignedIntegerTypeCodeArg();
    }
    #endregion

    private void Test_GetQuantity_UnsignedIntegerTypeCodeArgWhenNegativeQuantity_ThrowsOutOfRangeException(TypeCode typeCode)
    {
        // Arrange
        Enum measureUnit = GetRandomDefaultMeasureUnit();
        ValueType quantity = NegativeQuantity;
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit, null);

        // Act
        void attempt() => baseMeasure.GetQuantity(typeCode);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(ParamNames.typeCode, ex.ParamName);
    }

    private void Test_Ctor_InvalidValueTypeQuantityArg_ThrowsOutOfRangeException(ValueType quantity)
    {
        // Arrange
        Enum measureUnit = GetRandomDefaultMeasureUnit();

        // Act
        void attempt() => _ = new BaseMeasureChild(quantity, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }
    #endregion

    #region Constructor
    #region Quantity type validation
    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NullQuantityArg_ThrowsArgumentNullException()
    {
        // Arrange
        ValueType nullQuantity = null;
        Enum measureUnit =  GetRandomDefaultMeasureUnit();

        // Act
        void attempt() => _ = new BaseMeasureChild(nullQuantity, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }

    [DataTestMethod]
    [DynamicData(nameof(GetInvalidTypeQuantityArgs), DynamicDataSourceType.Method)]
    public void Ctor_InvalidValueTypeQuantityArg_ThrowsOutOfRangeException(ValueType quantity)
    {
        // Arrange
        Enum measureUnit = GetRandomDefaultMeasureUnit();

        // Act
        void attempt() => _ = new BaseMeasureChild(quantity, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }
    #endregion

    #region BaseMeasure(ValueType quantity, Enum measureUnit, decimal? exchangeRate = null)
    #region MeasureUnitType validation
    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NullMeasureUnitArg_ThrowsArgumentNullException()
    {
        // Arrange
        ValueType quantity =  GetRandomValueTypeQuantity();
        Enum nullMeasureUnit = null;

        // Act
        void attempt() => _ = new BaseMeasureChild(quantity, nullMeasureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NotMeasureUnitTypeEnumArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        Enum notMeasureUnitTypeEnum = NotMeasureUnitTypeEnum;
        ValueType quantity =  GetRandomValueTypeQuantity();

        // Act
        void attempt() => _ = new BaseMeasureChild(quantity, notMeasureUnitTypeEnum);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NotDefinedMeasureUnitArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        Enum notDefinedMeasureUnit = GetRandomNotDefinedMeasureUnit();
        TypeCode typeCode = notDefinedMeasureUnit.GetQuantityTypeCode();
        ValueType quantity =  GetRandomValueTypeQuantity(typeCode);

        // Act
        void attempt() => _ = new BaseMeasureChild(quantity, notDefinedMeasureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_MeasureUnitDoesNotHaveExchangeRateArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        Enum measureUnitNotHavingAdHocRate = GetRandomNonDefaultMeasureUnit();
        ValueType quantity =  GetRandomValueTypeQuantity(measureUnitNotHavingAdHocRate);

        // Act
        void attempt() => _ = new BaseMeasureChild(quantity, measureUnitNotHavingAdHocRate);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }
    #endregion

    #region ExchangeRate validation
    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_ConstantMeasureUnitAndDifferentExchangeRateArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        Enum measureUnit = MediumValueSampleMeasureUnit;
        ValueType quantity =  GetRandomValueTypeQuantity(measureUnit);
        decimal? differentExchangeRate = DecimalOne;

        // Act
        void attempt() => _ = new BaseMeasureChild(quantity, measureUnit, differentExchangeRate);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(ParamNames.exchangeRate, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NonDefaultMeasureUnitAndZeroExchangeRateArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        Enum measureUnit = MeasureUnitShouldHaveAdHocRate;
        ValueType quantity =  GetRandomValueTypeQuantity(measureUnit);
        decimal? zeroExchangeRate = DecimalZero;

        // Act
        void attempt() => _ = new BaseMeasureChild(quantity, measureUnit, zeroExchangeRate);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(ParamNames.exchangeRate, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NonDefaultMeasureUnitAndNegativeExchangeRateArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        Enum measureUnit = MeasureUnitShouldHaveAdHocRate;
        ValueType quantity =  GetRandomValueTypeQuantity(measureUnit);
        decimal? negativeExchangeRate = DecimalMinusOne;

        // Act
        void attempt() => _ = new BaseMeasureChild(quantity, measureUnit, negativeExchangeRate);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(ParamNames.exchangeRate, ex.ParamName);
    }
    #endregion

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_ThreeNullArgs_ThrowsArgumentNullException()
    {
        // Arrange
        ValueType nullQuantity = null;
        Enum nullMeasureUnit = null;
        decimal? nullExchangeRate = null;

        // Act
        void attempt() => _ = new BaseMeasureChild(nullQuantity, nullMeasureUnit, nullExchangeRate);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NullMeasureUnitAndNullExchangeRateArg_ThrowsArgumentNullException()
    {
        // Arrange
        ValueType quantity =  GetRandomValueTypeQuantity();
        Enum nullMeasureUnit = null;
        decimal? nullExchangeRate = null;

        // Act
        void attempt() => _ = new BaseMeasureChild(quantity, nullMeasureUnit, nullExchangeRate);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NullQuantityAndNullExchangeRateArg_ThrowsArgumentNullException()
    {
        // Arrange
        ValueType nullQuantity = null;
        Enum measureUnit = GetRandomDefaultMeasureUnit();
        decimal? nullExchangeRate = null;

        // Act
        void attempt() => _ = new BaseMeasureChild(nullQuantity, measureUnit, nullExchangeRate);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }

    [DataTestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetAllDefaultMeasureUnitExchangeRatePairs), DynamicDataSourceType.Method)]
    public void Ctor_ValidQuantityAndDefaultMeasureUnitAndValidExchangeRateArgs_CreatesInstance(Enum expectedMeasureUnit, decimal? exchangeRate)
    {
        // Arrange
        TypeCode typeCode = expectedMeasureUnit.GetQuantityTypeCode();
        ValueType expectedQuantity = GetRandomValueTypeQuantity(typeCode);
        decimal? expectedExchangeRate = expectedMeasureUnit.GetExchangeRate();

        // Act
        var actual = new BaseMeasureChild(expectedQuantity, expectedMeasureUnit, exchangeRate);

        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(expectedQuantity, actual.Quantity);
        Assert.AreEqual(expectedMeasureUnit, actual.MeasureUnit);
        Assert.AreEqual(expectedExchangeRate, actual.GetExchangeRate());
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_ValidQuantityAndNonDefaultMeasureUnitAndValidExchangeRateArg_CreatesInstance()
    {
        // Arrange
        Enum expectedMeasureUnit = GetRandomNonDefaultMeasureUnit();
        ValueType expectedQuantity = GetRandomValueTypeQuantity(expectedMeasureUnit);

        decimal? expectedExchangeRate = GetRandomExchangeRate();

        // Act
        var actual = new BaseMeasureChild(expectedQuantity, expectedMeasureUnit, expectedExchangeRate);

        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(expectedQuantity, actual.Quantity);
        Assert.AreEqual(expectedMeasureUnit, actual.MeasureUnit);
        Assert.AreEqual(expectedExchangeRate, actual.GetExchangeRate());

        // Restore
        RemoveIfNonDefaultMeasureUnit(expectedMeasureUnit);
    }

    [DataTestMethod, TestCategory("UnitTest")] // TODO decimal
    [DataRow(int.MinValue)]
    [DataRow(uint.MaxValue)]
    [DataRow(long.MinValue)]
    [DataRow(ulong.MaxValue)]
    [DataRow(double.Epsilon * -1)]
    public void Ctor_ValidPrimitiveTypeQuantityArg_CreatesInstance(ValueType quantity)
    {
        // Arrange
        ValueType expectedQuantity = ValidateMeasures.GetValidQuantity(quantity);

        // Act
        var actual = new BaseMeasureChild(quantity, MediumValueSampleMeasureUnit, null);

        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(expectedQuantity, actual.Quantity);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_ValidDecimalTypeQuantityArg_CreatesInstance()
    {
        // Arrange
        ValueType expectedQuantity = (decimal)GetRandomValueTypeQuantity(TypeCode.Decimal);

        // Act
        var actual = new BaseMeasureChild(expectedQuantity, default(Currency), null);

        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(expectedQuantity, actual.Quantity);
    }
    #endregion

    #region BaseMeasure(ValueType quantity, IMeasurement measurement)
    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_TwoNullArgs_ThrowsArgumentNullException()
    {
        // Arrange
        ValueType nullQuantity = null;
        IMeasurement nullMeasurement = null;

        // Act
        void attempt() => _ = new BaseMeasureChild(nullQuantity, nullMeasurement);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.measurement, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_ValidQuantityAndNullMeasurementArg_ThrowsArgumentNullException()
    {
        // Arrange
        ValueType quantity =  GetRandomValueTypeQuantity();
        IMeasurement nullMeasurement = null;

        // Act
        void attempt() => _ = new BaseMeasureChild(quantity, nullMeasurement);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.measurement, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NullQuantityAndValidMeasurementArg_ThrowsArgumentNullException()
    {
        // Arrange
        ValueType nullQuantity = null;
        IMeasurement measurement = _factory.GetMeasurement(MediumValueSampleMeasureUnit);

        // Act
        void attempt() => _ = new BaseMeasureChild(nullQuantity, measurement);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }

    [DataTestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetAllDefaultMeasureUnitExchangeRatePairs), DynamicDataSourceType.Method)]
    public void Ctor_ValidQuantityAndValidMeasurementArgs_CreatesInstance(Enum measureUnit, decimal? exchangeRate)
    {
        // Arrange
        IMeasurement expectedMeasurement = _factory.GetMeasurement(measureUnit, exchangeRate);
        TypeCode typeCode = measureUnit.GetQuantityTypeCode();
        ValueType expectedQuantity = GetRandomValueTypeQuantity(typeCode);

        // Act
        var actual = new BaseMeasureChild(expectedQuantity, expectedMeasurement);

        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(expectedQuantity, actual.GetQuantity());
        Assert.AreEqual(expectedMeasurement, actual.Measurement);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_ValidQuantityAndNonDefaultMeasurementArg_CreatesInstance()
    {
        // Arrange
        Enum measureUnit = GetRandomNonDefaultMeasureUnit();
        TypeCode typeCode = measureUnit.GetQuantityTypeCode();
        ValueType quantity = GetRandomValueTypeQuantity(typeCode);
        ValueType expectedQuantity = quantity.ToQuantity(typeCode);

        decimal? exchangeRate = GetRandomExchangeRate();
        IMeasurement expectedMeasurement = _factory.GetMeasurement(measureUnit, exchangeRate);

        // Act
        var actual = new BaseMeasureChild(expectedQuantity, expectedMeasurement);

        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(expectedQuantity, actual.GetQuantity());
        Assert.AreEqual(expectedMeasurement, actual.Measurement);

        // Restore
        RemoveIfNonDefaultMeasureUnit(measureUnit);
    }
    #endregion

    #region BaseMeasure(IBaseMeasure? other)
    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NullMeasurementArg_ThrowsArgumentNullException()
    {
        // Arrange
        IBaseMeasure baseMeasure = null;

        // Act
        void attempt() => _ = new BaseMeasureChild(baseMeasure);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_ValidArg_CreatesInstance()
    {
        // Arrange
        var (quantity, measureUnit) =  GetRandomBaseMeasureArgs();
        IBaseMeasure expected = new BaseMeasureChild(quantity, measureUnit, null);

        // Act
        var actual = new BaseMeasureChild(expected);

        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region CompareTo
    [TestMethod, TestCategory("UnitTest")]
    public void K()
    {

    }
    #endregion

    #region ExchangeTo
    #region ExchangeTo(Enum measureUnit)
    [DataTestMethod, TestCategory("UnitTest")]
    [DataRow(null, null)]
    [DataRow(default(LimitType), null)]
    [DataRow(Currency.EUR, null)]
    [DataRow(default(VolumeUnit), null)]
    [DataRow((WeightUnit)3, null)]
    public void ExchangeTo_InvalidMeasureUnitArg_ReturnsNull(Enum measureUnit, IBaseMeasure expected)
    {
        // Arrange
        //Type measureUnitType = measureUnit?.GetType();
        //bool isMeasureUnit = ExchangeMeasures.DefaultMeasureUnitTypes.Contains(measureUnitType);
        //TypeCode? typeCode = isMeasureUnit ? measureUnit?.GetQuantityTypeCode() : null;
        ValueType quantity =  GetRandomValueTypeQuantity(MediumValueSampleMeasureUnit);
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, MediumValueSampleMeasureUnit, null);

        // Act
        var actual = baseMeasure.ExchangeTo(measureUnit);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ExchangeTo_ValidMeasureUnitArg_ReturnsExpected()
    {
        // Arrange
        Enum measureUnit = MediumValueSampleMeasureUnit;
        Enum targetMeasureUnit = MaxValueSampleMeasureUnit;
        decimal targetExchangeRate = targetMeasureUnit.GetExchangeRate();

        var (quantity, exchanged) = GetRandomExchangedQuantityPair(measureUnit, targetExchangeRate);

        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit, null);
        IBaseMeasure expected = new BaseMeasureChild(exchanged, targetMeasureUnit, null);

        // Act
        var actual = baseMeasure.ExchangeTo(targetMeasureUnit);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ExchangeTo_SameMeasureUnitArg_ReturnsExpected()
    {
        // Arrange
        var (quantity, measureUnit) =  GetRandomBaseMeasureArgs();
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit, null);

        IBaseMeasure expected = new BaseMeasureChild(quantity, measureUnit, null);

        // Act
        var actual = baseMeasure.ExchangeTo(measureUnit);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region ExchangeTo(decimal exchangeRate)
    [TestMethod, TestCategory("UnitTest")]
    public void ExchangeTo_ZeroExchangeRateArg_ReturnsNull()
    {
        // Arrange
        // Act
        var result = _baseMeasure.ExchangeTo(DecimalZero);

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ExchangeTo_NegativeExchangeRateArg_ReturnsNull()
    {
        // Arrange
        // Act
        var result = _baseMeasure.ExchangeTo(DecimalNegative);

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ExchangeTo_PositiveExchangeRateArg_ReturnsExpected()
    {
        Enum measureUnit =  GetRandomDefaultMeasureUnit();
        decimal exchangeRate =  GetRandomExchangeRate();

        var (quantity, exchanged) = GetRandomExchangedQuantityPair(measureUnit, exchangeRate);
        TypeCode typeCode = measureUnit.GetQuantityTypeCode();
        ValueType expected = exchanged.ToQuantity(typeCode);

        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit, null);

        // Act
        var actual = baseMeasure.ExchangeTo(exchangeRate);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region IsExchangeableTo
    [DataTestMethod, TestCategory("UnitTest")]
    [DataRow(null, false)]
    [DataRow(default(LimitType), false)]
    [DataRow(default(Currency), false)]
    [DataRow(default(VolumeUnit), false)]
    [DataRow((WeightUnit)3, false)]
    [DataRow(default(WeightUnit), true)]
    public void IsExchangeableTo_MeasureUnitArg_ReturnsExpected(Enum measureUnit, bool expected)
    {
        // Arrange
        ValueType quantity =  GetRandomValueTypeQuantity(MediumValueSampleMeasureUnit);
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, MediumValueSampleMeasureUnit, null);

        // Act
        var actual = baseMeasure.IsExchangeableTo(measureUnit);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region GetBaseMeasure
    #region GetBaseMeasure(IBaseMeasure? other = null)
    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseMeasure_ReturnsExpected()
    {
        // Arrange
        var (quantity, measureUnit) =  GetRandomBaseMeasureArgs();
        IBaseMeasure expected = new BaseMeasureChild(quantity, measureUnit, null);

        // Act
        var actual = expected.GetBaseMeasure();

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseMeasure_NullArg_ReturnsExpected()
    {
        // Arrange
        var (quantity, measureUnit) =  GetRandomBaseMeasureArgs();
        IBaseMeasure expected = new BaseMeasureChild(quantity, measureUnit, null);

        // Act
        var actual = expected.GetBaseMeasure(null);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseMeasure_ValidIBaseMeasureArg_ReturnsExpected()
    {
        // Arrange
        var (quantity, measureUnit) =  GetRandomBaseMeasureArgs();
        IBaseMeasure expected = new BaseMeasureChild(quantity, measureUnit, null);

        // Act
        var actual = _baseMeasure.GetBaseMeasure(expected);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region GetBaseMeasure(ValueType quantity, IMeasurement? measurement = null)
    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseMeasure_QuantityAndMeasurementArgs_ReturnsExpected()
    {
        // Arrange
        var (quantity, measureUnit) =  GetRandomBaseMeasureArgs();
        IMeasurement measurement = _factory.GetMeasurement(measureUnit);
        IBaseMeasure expected = new BaseMeasureChild(quantity, measurement);

        // Act
        var actual = _baseMeasure.GetBaseMeasure(quantity, measurement);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseMeasure_QuantityAndNullArgs_ReturnsExpected()
    {
        // Arrange
        var (quantity, measureUnit) =  GetRandomBaseMeasureArgs();
        IMeasurement measurement = _factory.GetMeasurement(measureUnit);
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measurement);

        IBaseMeasure expected = new BaseMeasureChild(quantity, measurement);

        // Act
        var actual = baseMeasure.GetBaseMeasure(quantity, null);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseMeasure_QuantityArg_ReturnsExpected()
    {
        // Arrange
        var (quantity, measureUnit) =  GetRandomBaseMeasureArgs();
        IMeasurement measurement = _factory.GetMeasurement(measureUnit);
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measurement);

        quantity =  GetRandomValueTypeQuantity(measureUnit);
        IBaseMeasure expected = new BaseMeasureChild(quantity, measurement);

        // Act
        var actual = baseMeasure.GetBaseMeasure(quantity);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region GetBaseMeasure(ValueType quantity, Enum measureUnit, decimal? exchangeRate = null)
    [DataTestMethod, TestCategory("UnitTest")]
    [DataRow(1.52, default(AreaUnit), 1)]
    [DataRow(-6489214.6547, default(Currency), 1)]
    [DataRow(941, Pieces.Default, 1)]
    [DataRow(0, ExtentUnit.dm, 100)]
    public void GeBaseMeasure_ThreeValidArgs_ReturnsExpected(double quantity, Enum measureUnit, int exchangeRate)
    {
        // Arrange
        IBaseMeasure expected = new BaseMeasureChild(quantity, measureUnit, exchangeRate);

        // Act
        var actual = _baseMeasure.GetBaseMeasure(quantity, measureUnit, exchangeRate);

        // Assert
        Assert.AreEqual(expected, actual);

        // Restore
        RemoveIfNonDefaultMeasureUnit(measureUnit);
    }

    [DataTestMethod, TestCategory("UnitTest")]
    [DataRow(1.52, default(AreaUnit))]
    [DataRow(-6489214.6547, default(Currency))]
    [DataRow(941, Pieces.Default)]
    [DataRow(0, ExtentUnit.dm)]
    public void GeBaseMeasure_TwoValidAndNullExchangeRateArgs_ReturnsExpected(double expectedQuantity, Enum expectedMeasureUnit)
    {
        // Arrange
        _ = Pieces.Default.TryAddExchangeRate(10);
        decimal expectedExchangeRate = expectedMeasureUnit.GetExchangeRate();

        // Act
        var actual = _baseMeasure.GetBaseMeasure(expectedQuantity, expectedMeasureUnit, null);

        // Assert
        Assert.AreEqual(expectedQuantity, actual.Quantity);
        Assert.AreEqual(expectedMeasureUnit, actual.MeasureUnit);
        Assert.AreEqual(expectedExchangeRate, actual.GetExchangeRate());

        // Restore
        RemoveIfNonDefaultMeasureUnit(Pieces.Default);
    }

    [DataTestMethod, TestCategory("UnitTest")]
    [DataRow(1.52, default(AreaUnit))]
    [DataRow(-6489214.6547, default(Currency))]
    [DataRow(941, Pieces.Default)]
    [DataRow(0, ExtentUnit.dm)]
    public void GeBaseMeasure_ValidQuantityAndMeasureUnitArgs_ReturnsExpected(double expectedQuantity, Enum expectedMeasureUnit)
    {
        // Arrange
        _ = Pieces.Default.TryAddExchangeRate(10);
        decimal expectedExchangeRate = expectedMeasureUnit.GetExchangeRate();

        // Act
        var actual = _baseMeasure.GetBaseMeasure(expectedQuantity, expectedMeasureUnit);

        // Assert
        Assert.AreEqual(expectedQuantity, actual.Quantity);
        Assert.AreEqual(expectedMeasureUnit, actual.MeasureUnit);
        Assert.AreEqual(expectedExchangeRate, actual.GetExchangeRate());

        // Restore
        RemoveIfNonDefaultMeasureUnit(Pieces.Default);
    }
    #endregion
    #endregion

    #region GetDecimalQuantity
    [TestMethod, TestCategory("UnitTest")]
    public void GetDecimalQuantity_ReturnsExpected()
    {
        // Arrange
        var (quantity, measureUnit) =  GetRandomBaseMeasureArgs();
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit, null);
        var expected = (decimal)quantity.ToQuantity(typeof(decimal));

        // Act
        var actual = baseMeasure.GetDecimalQuantity();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region GetExchangeRate
    [DataTestMethod, TestCategory("UnitTest")]
    [DataRow(default(WeightUnit), 1.0)]
    [DataRow(WeightUnit.kg, 1000.0)]
    [DataRow((WeightUnit)2, 1000000.0)]
    [DataRow(VolumeUnit.mmCubic, 1.0)]
    [DataRow(VolumeUnit.cmCubic, 1000.0)]
    [DataRow(VolumeUnit.dmCubic, 1000000.0)]
    [DataRow(VolumeUnit.meterCubic, 1000000000.0)]
    [DataRow(default(Currency), 1.0)]
    [DataRow(Currency.EUR, 409.2987)] // EurExchangeRate
    public void GetExchangeRate_ReturnsExpected(Enum measureUnit, double expected)
    {
        // Arrange
        if (measureUnit is Currency.EUR)
        {
            _ = measureUnit.TryAddExchangeRate(EurExchangeRate);
        }

        var baseMeasure = new BaseMeasureChild(ZeroQuantity, measureUnit, null);

        // Act
        var result = baseMeasure.GetExchangeRate();
        double actual = (double)result.ToQuantity(typeof(double))!;

        // Assert
        Assert.AreEqual(expected, actual);

        // Restore
        RemoveIfNonDefaultMeasureUnit(measureUnit);
    }
    #endregion

    #region GetMeasureUnit
    [DataTestMethod, TestCategory("UnitTest")]
    [DataRow(default(AreaUnit))]
    [DataRow(default(Currency))]
    [DataRow(default(Pieces))]
    [DataRow(default(DistanceUnit))]
    [DataRow(default(ExtentUnit))]
    [DataRow(default(TimeUnit))]
    [DataRow(default(VolumeUnit))]
    [DataRow(default(WeightUnit))]
    [DataRow(WeightUnit.kg)]
    [DataRow((WeightUnit)2)]
    public void GetMeasureUnit_ReturnsExpected(Enum expected)
    {
        // Arrange
        var baseMeasure = new BaseMeasureChild(ZeroQuantity, expected, null);

        // Act
        var actual = baseMeasure.MeasureUnit;

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region GetMeasurable
    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurable_ReturnsExpected()
    {
        // Arrange
        var (quantity, measureUnit) =  GetRandomBaseMeasureArgs();
        IBaseMeasure expected = new BaseMeasureChild(quantity, measureUnit, null);

        // Act
        var actual = expected.GetMeasurable();

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IMeasurable));
        Assert.AreEqual(measureUnit, actual.MeasureUnit);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurable_NullArg_ReturnsExpected()
    {
        // Arrange
        var (quantity, measureUnit) =  GetRandomBaseMeasureArgs();
        IBaseMeasure expected = new BaseMeasureChild(quantity, measureUnit, null);

        // Act
        var actual = expected.GetMeasurable(null);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IMeasurable));
        Assert.AreEqual(measureUnit, actual.MeasureUnit);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurable_ValidArg_ReturnsExpected()
    {
        // Arrange
        Enum measureUnit =  GetRandomDefaultMeasureUnit();

        // Act
        var actual = _baseMeasure.GetMeasurable(measureUnit);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IMeasurable));
        Assert.AreEqual(measureUnit, actual.MeasureUnit);
    }
    #endregion

    #region GetQuantity
    #region GetQuantity()
    [TestMethod, TestCategory("UnitTest")]
    public void GetQuantity_ReturnsExpected()
    {
        // Arrange
        var (quantity, measureUnit) =  GetRandomBaseMeasureArgs();
        TypeCode typeCode = measureUnit.GetQuantityTypeCode();
        ValueType expected = quantity.ToQuantity(typeCode);

        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit, null);

        // Act
        var actual = baseMeasure.GetQuantity();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region GetQuantity(RoundingMode roundingMode)
    [TestMethod, TestCategory("UnitTest")]
    public void GetQuantity_InvalidRoundingModeArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        int roundingModeMaxValue = Enum.GetNames(typeof(RoundingMode)).Length;
        RoundingMode invalidRoundingMode = (RoundingMode)roundingModeMaxValue;
        var (quantity, measureUnit) =  GetRandomBaseMeasureArgs();
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit, null);

        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => baseMeasure.GetQuantity(invalidRoundingMode));
        Assert.AreEqual(ParamNames.roundingMode, ex.ParamName);
    }

    [DataTestMethod, TestCategory("UnitTest")]
    [DataRow(default(RoundingMode), 3.0)]
    [DataRow(RoundingMode.Ceiling, 4.0)]
    [DataRow(RoundingMode.Floor, 3.0)]
    [DataRow(RoundingMode.Half, 3.5)]
    public void GetQuantity_ValidRoundingModeArg_ReturnsExpected(RoundingMode roundingMode, ValueType exchanged)
    {
        // Arrange
        Enum measureUnit =  DefaultSampleMeasureUnit;
        TypeCode typeCode = measureUnit.GetQuantityTypeCode();
        ValueType quantity = Math.PI.ToQuantity(typeCode);
        ValueType expected = exchanged.ToQuantity(typeCode);

        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit, null);

        // Act
        var actual = baseMeasure.GetQuantity(roundingMode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region GetQuantity(Type type)
    //[TestMethod, TestCategory("UnitTest")]
    //public void GetQuantity_NullArg_ThrowsArgumentNullException()
    //{
    //    // Arrange
    //    // Act
    //    // Assert
    //    var ex = Assert.ThrowsException<ArgumentNullException>(() => _baseMeasure.GetQuantity(TypeCode.Empty));
    //    Assert.AreEqual(ParamNames.targetTypeCode, ex.ParamName);
    //}

    [TestMethod, TestCategory("UnitTest")]
    public void GetQuantity_InvalidTypeArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        TypeCode invalidTypeCode = TypeCode.Boolean;

        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => _baseMeasure.GetQuantity(invalidTypeCode));
        Assert.AreEqual(ParamNames.typeCode, ex.ParamName);
    }

    [DataTestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetUnsignedIntegerTypeCodeArg), DynamicDataSourceType.Method)]
    public void GetQuantity_UnsignedIntegerTypeCodeArgWhenNegativeQuantity_ThrowsArgumentOutOfRangeException(TypeCode typeCode)
    {
        // Arrange
        Enum measureUnit = GetRandomDefaultMeasureUnit();
        ValueType quantity = GetRandomNegativeValueTypeQuantity();
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit, null);

        // Act
        void attempt() => baseMeasure.GetQuantity(typeCode);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(ParamNames.typeCode, ex.ParamName);
    }

    //[TestMethod, TestCategory("UnitTest")]
    //public void GetQuantity_ULongTypeArgWhenNegativeQuantity_ThrowsArgumentOutOfRangeException()
    //{
    //    Test_GetQuantity_UnsignedIntegerTypeCodeArgWhenNegativeQuantity_ThrowsOutOfRangeException(TypeCode.UInt64);
    //}

    [TestMethod, TestCategory("UnitTest")]
    public void GetQuantity_ValidTypeArg_ReturnsExpected()
    {
        // Arrange
        Enum measureUnit = GetRandomDefaultMeasureUnit();
        var (quantity, targetTypeCode) = GetRandomValueTypeQuantityTargetTypeCodePair(measureUnit);
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit, null);

        var expected = quantity.ToQuantity(targetTypeCode);

        // Act
        var actual = baseMeasure.GetQuantity(targetTypeCode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region TryExchangeTo
    #region TryExchangeTo(Enum measureUnit, [NotNullWhen(true)] out IBaseMeasure? exchanged)
    [DataTestMethod, TestCategory("UnitTest")]
    [DataRow(null, null)]
    [DataRow(default(LimitType), null)]
    [DataRow(default(Currency), null)]
    [DataRow(default(VolumeUnit), null)]
    [DataRow((WeightUnit)3, null)]
    public void TryExchangeTo_InvalidMeasureUnitArg_ReturnsFalse_OutNull(Enum measureUnit, IBaseMeasure expected)
    {
        // Arrange
        ValueType quantity =  GetRandomValueTypeQuantity(MediumValueSampleMeasureUnit);
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, MediumValueSampleMeasureUnit, null);

        // Act
        var result = baseMeasure.TryExchangeTo(measureUnit, out IBaseMeasure actual);

        // Assert
        Assert.IsFalse(result);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void TryExchangeTo_ValidMeasureUnitArg_ReturnsTrue_OutExpected() // TODO
    {
        // Arrange
        Enum measureUnit = MaxValueSampleMeasureUnit;
        Enum targetMeasureUnit = MediumValueSampleMeasureUnit;
        decimal targetExchangeRate = targetMeasureUnit.GetExchangeRate();

        var (quantity, exchanged) = GetRandomExchangedQuantityPair(measureUnit, targetExchangeRate);
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit, null);
        IBaseMeasure expected = new BaseMeasureChild(exchanged, targetMeasureUnit, null);

        // Act
        var result = baseMeasure.TryExchangeTo(targetMeasureUnit, out IBaseMeasure actual);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void TryExchangeTo_SameMeasureUnitArg_ReturnsTrue_OutExpected()
    {
        // Arrange
        var (quantity, measureUnit) =  GetRandomBaseMeasureArgs();
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit, null);

        IBaseMeasure expected = new BaseMeasureChild(quantity, measureUnit, null);

        // Act
        var result = baseMeasure.TryExchangeTo(measureUnit, out IBaseMeasure actual);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region TryExchangeTo(decimal exchangeRate, [NotNullWhen(true)] out ValueType? exchanged)
    [TestMethod, TestCategory("UnitTest")]
    public void TryExchangeTo__ZeroExchangeRateArg_ReturnsExpected()
    {
        // Arrange
        ValueType expected = null;

        // Act
        var result = _baseMeasure.TryExchangeTo(DecimalZero, out ValueType actual);

        // Assert
        Assert.IsFalse(result);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void TryExchangeTo__NegativeExchangeRateArg_ReturnsExpected()
    {
        // Arrange
        ValueType expected = null;

        // Act
        var result = _baseMeasure.TryExchangeTo(DecimalNegative, out ValueType actual);

        // Assert
        Assert.IsFalse(result);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void TryExchangeTo_PositiveExchangeRateArg_ReturnsExpected()
    {
        // Arrange
        Enum measureUnit =  GetRandomDefaultMeasureUnit();
        decimal exchangeRate =  GetRandomExchangeRate();

        var (quantity, exchanged) = GetRandomExchangedQuantityPair(measureUnit, exchangeRate);
        TypeCode typeCode = measureUnit.GetQuantityTypeCode();
        ValueType expected = exchanged.ToQuantity(typeCode);
        
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit, null);

        // Act
        var result = baseMeasure.TryExchangeTo(exchangeRate, out ValueType actual);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion
}
#nullable enable
