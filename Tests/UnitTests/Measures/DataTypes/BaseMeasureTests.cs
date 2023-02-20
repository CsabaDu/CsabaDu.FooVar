using CsabaDu.FooVar.Measures.Interfaces.DataTypes;
using CsabaDu.FooVar.Measures.Interfaces.Factories;
using CsabaDu.FooVar.Measures.Statics;
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
    public void IniitializeMaseMeasureTests()
    {
        TestSupport.RestoreDefaultMeasureUnits();

        ValueType quantity = RandomParams.GetRandomValueTypeQuantity();
        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();

        _baseMeasure = new BaseMeasureChild(quantity, measureUnit);
        _factory = _baseMeasure.MeasurementFactory;
    }

    #endregion

    #region Constructor

    #region Quantity validation
    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_EnumTypeInvalidQuantityArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        ValueType invalidValueTypeQuantity = SampleParams.DefaultPieces;

        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new BaseMeasureChild(invalidValueTypeQuantity, SampleParams.MediumValueSampleMeasureUnit));
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_BoolTypeInvalidQuantityArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        ValueType invalidValueTypeQuantity = true;

        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new BaseMeasureChild(invalidValueTypeQuantity, SampleParams.MediumValueSampleMeasureUnit));
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_CharTypeInvalidQuantityArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        ValueType invalidValueTypeQuantity = char.MaxValue;

        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new BaseMeasureChild(invalidValueTypeQuantity, SampleParams.MediumValueSampleMeasureUnit));
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_IntPtrTypeInvalidQuantityArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        ValueType invalidValueTypeQuantity = IntPtr.Zero;

        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new BaseMeasureChild(invalidValueTypeQuantity, SampleParams.MediumValueSampleMeasureUnit));
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_UIntPtrTypeInvalidQuantityArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        ValueType invalidValueTypeQuantity = UIntPtr.Zero;

        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new BaseMeasureChild(invalidValueTypeQuantity, SampleParams.MediumValueSampleMeasureUnit));
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }
    #endregion

    #region BaseMeasure(ValueType quantity, Enum measureUnit, decimal? exchangeRate = null)
    #region MeasureUnitType validation

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NotMeasureUnitTypeEnumArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new BaseMeasureChild(SampleParams.ZeroQuantity, SampleParams.NotMeasureUnitTypeEnum));
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NotDefinedMeasureUnitArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new BaseMeasureChild(SampleParams.ZeroQuantity, SampleParams.NotDefinedSampleMeasureUnit));
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    #endregion

    #region ExchangeRate validation
    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_MeasureUnitDoesNotHaveExchangeRateArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new BaseMeasureChild(SampleParams.ZeroQuantity, SampleParams.MeasureUnitShouldHaveAdHocRate));
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_MeasureUnitAndDifferentExchangeRateArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new BaseMeasureChild(SampleParams.ZeroQuantity, SampleParams.MediumValueSampleMeasureUnit, SampleParams.DecimalOne));
        Assert.AreEqual(ParamNames.exchangeRate, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_MeasureUnitAndZeroExchangeRateArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new BaseMeasureChild(SampleParams.ZeroQuantity, SampleParams.MeasureUnitShouldHaveAdHocRate, SampleParams.DecimalZero));
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_MeasureUnitAndNegativeExchangeRateArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new BaseMeasureChild(SampleParams.ZeroQuantity, SampleParams.MeasureUnitShouldHaveAdHocRate, SampleParams.DecimalMinusOne));
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }
    #endregion

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_ThreeNullArgs_ThrowsArgumentNullException()
    {
        // Arrange
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(() => new BaseMeasureChild(null, null, null));
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NullMeasureUnitAndNullExchangeRateArg_ThrowsArgumentNullException()
    {
        // Arrange
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(() => new BaseMeasureChild(SampleParams.ZeroQuantity, null, null));
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NullQuantityAndNullExchangeRateArg_ThrowsArgumentNullException()
    {
        // Arrange
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(() => new BaseMeasureChild(null, SampleParams.MediumValueSampleMeasureUnit, null));
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NullQuantityAndValidMeasureUnitArg_ThrowsArgumentNullException()
    {
        // Arrange
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(() => new BaseMeasureChild(null, SampleParams.MediumValueSampleMeasureUnit));
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }

    [DataTestMethod, TestCategory("UnitTest")]
    [DataRow(0, default(AreaUnit))]
    [DataRow(0, default(Currency))]
    [DataRow(0, default(Pieces))]
    [DataRow(0, default(DistanceUnit))]
    [DataRow(0, default(ExtentUnit))]
    [DataRow(0, default(TimeUnit))]
    [DataRow(0, default(VolumeUnit))]
    [DataRow(0, default(WeightUnit))]
    [DataRow(15, WeightUnit.kg)]
    [DataRow(627.2, (WeightUnit)2)]
    [DataRow(-4.5, VolumeUnit.meterCubic)]
    [DataRow(12.4, default(Currency))]
    [DataRow(124, default(Pieces))]
    public void Ctor_ValidQuantityAndMeasureUnitArgs_CreatesInstance(ValueType expectedQuantity, Enum expectedMeasureUnit)
    {
        // Arrange
        _ = expectedMeasureUnit.TryAddExchangeRate(SampleParams.DecimalOne);

        // Act
        var actual = new BaseMeasureChild(expectedQuantity, expectedMeasureUnit);

        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(expectedQuantity, actual.Quantity);
        Assert.AreEqual(expectedMeasureUnit, actual.MeasureUnit);

        // Restore
        TestSupport.RemoveIfNotDefaultMeasureUnit(expectedMeasureUnit);
    }

    [DataTestMethod, TestCategory("UnitTest")]
    [DataRow(0, default(AreaUnit), null)]
    [DataRow(0, default(DistanceUnit), 1)]
    [DataRow(0, default(ExtentUnit), null)]
    [DataRow(0, default(ExtentUnit), 1)]
    [DataRow(0, default(TimeUnit), null)]
    [DataRow(0, default(VolumeUnit), 1)]
    [DataRow(0, default(WeightUnit), null)]
    [DataRow(15, WeightUnit.kg, 1000)]
    [DataRow(627.2, (WeightUnit)2, null)]
    [DataRow(-4.5, VolumeUnit.meterCubic, 1000000000)]
    [DataRow(12.4, default(Currency), null)]
    [DataRow(124, default(Pieces), 1)]
    [DataRow(657196259.4617, (Currency)1, 409)]
    public void Ctor_ThreeValidArgs_CreatesInstance(ValueType expectedQuantity, Enum expectedMeasureUnit, int? exchangeRate)
    {
        // Arrange
        decimal notNullExchangeRate = exchangeRate ?? SampleParams.DecimalOne;
        _ = expectedMeasureUnit.TryAddExchangeRate(notNullExchangeRate);

        // Act
        var actual = new BaseMeasureChild(expectedQuantity, expectedMeasureUnit, exchangeRate);

        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(expectedQuantity, actual.Quantity);
        Assert.AreEqual(expectedMeasureUnit, actual.MeasureUnit);

        // Restore
        TestSupport.RemoveIfNotDefaultMeasureUnit(expectedMeasureUnit);
    }

    [DataTestMethod, TestCategory("UnitTest")]
    [DataRow(byte.MaxValue)]
    [DataRow(sbyte.MinValue)]
    [DataRow(short.MinValue)]
    [DataRow(ushort.MaxValue)]
    [DataRow(int.MinValue)]
    [DataRow(uint.MaxValue)]
    [DataRow(long.MinValue)]
    [DataRow(ulong.MaxValue)]
    [DataRow(float.Epsilon)]
    [DataRow(double.Epsilon * -1)]
    public void Ctor_ValidPrimitiveTypeQuantityArg_CreatesInstance(ValueType quantity)
    {
        // Arrange
        ValueType expectedQuantity = ValidateMeasures.GetValidQuantity(quantity);

        // Act
        var actual = new BaseMeasureChild(quantity, SampleParams.MediumValueSampleMeasureUnit);

        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(expectedQuantity, actual.Quantity);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_ValidDecimalTypeQuantityArg_CreatesInstance()
    {
        // Arrange
        ValueType expectedQuantity = SampleParams.DecimalPositive;
        // Act
        var actual = new BaseMeasureChild(expectedQuantity, SampleParams.MediumValueSampleMeasureUnit);

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
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(() => new BaseMeasureChild(null, null));
        Assert.AreEqual(ParamNames.measurement, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_ValidQuantityAndNullMeasurementArg_ThrowsArgumentNullException()
    {
        // Arrange
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(() => new BaseMeasureChild(SampleParams.ZeroQuantity, null));
        Assert.AreEqual(ParamNames.measurement, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NullQuantityAndValidMeasurementArg_ThrowsArgumentNullException()
    {
        // Arrange
        IMeasurement measurement = _factory.GetMeasurement(SampleParams.MediumValueSampleMeasureUnit);

        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(() => new BaseMeasureChild(null, measurement));
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }

    [DataTestMethod, TestCategory("UnitTest")]
    [DataRow(0, default(AreaUnit), null)]
    [DataRow(0, default(DistanceUnit), 1)]
    [DataRow(0, default(ExtentUnit), null)]
    [DataRow(0, default(ExtentUnit), 1)]
    [DataRow(0, default(TimeUnit), null)]
    [DataRow(0, default(VolumeUnit), 1)]
    [DataRow(0, default(WeightUnit), null)]
    [DataRow(15, WeightUnit.kg, 1000)]
    [DataRow(627.2, (WeightUnit)2, null)]
    [DataRow(-4.5, VolumeUnit.meterCubic, 1000000000)]
    [DataRow(12.4, default(Currency), null)]
    [DataRow(124, default(Pieces), 1)]
    [DataRow(657196259.4617, (Currency)1, 409)]
    public void Ctor_ValidQuantityAndValidMeasurementArgs_CreatesInstance(ValueType expectedQuantity, Enum measureUnit, int? exchangeRate)
    {
        // Arrange
        IMeasurement expectedMeasurement = _factory.GetMeasurement(measureUnit, exchangeRate);

        // Act
        var actual = new BaseMeasureChild(expectedQuantity, expectedMeasurement);

        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(expectedQuantity, actual.Quantity);
        Assert.AreEqual(expectedMeasurement, actual.Measurement);

        // Restore
        TestSupport.RemoveIfNotDefaultMeasureUnit(measureUnit);
    }
    #endregion

    #region BaseMeasure(IBaseMeasure? other)
    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NullMeasurementArg_ThrowsArgumentNullException()
    {
        // Arrange
        IBaseMeasure baseMeasure = null;

        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(() => new BaseMeasureChild(baseMeasure));
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_ValidArg_CreatesInstance()
    {
        // Arrange
        var (quantity, measureUnit) = RandomParams.GetRandomBaseMeasureArgs();
        IBaseMeasure expected = new BaseMeasureChild(quantity, measureUnit);

        // Act
        var actual = new BaseMeasureChild(expected);

        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(expected, actual);
    }

    #endregion

    #endregion

    #region GetBaseMeasure

    #region GetBaseMeasure(IBaseMeasure? other = null)
    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseMeasure_ReturnsExpected()
    {
        // Arrange
        var (quantity, measureUnit) = RandomParams.GetRandomBaseMeasureArgs();
        IBaseMeasure expected = new BaseMeasureChild(quantity, measureUnit);

        // Act
        var actual = expected.GetBaseMeasure();

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseMeasure_NullArg_ReturnsExpected()
    {
        // Arrange
        var (quantity, measureUnit) = RandomParams.GetRandomBaseMeasureArgs();
        IBaseMeasure expected = new BaseMeasureChild(quantity, measureUnit);

        // Act
        var actual = expected.GetBaseMeasure(null);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseMeasure_ValidIBaseMeasureArg_ReturnsExpected()
    {
        // Arrange
        var (quantity, measureUnit) = RandomParams.GetRandomBaseMeasureArgs();
        IBaseMeasure expected = new BaseMeasureChild(quantity, measureUnit);

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
        var (quantity, measureUnit) = RandomParams.GetRandomBaseMeasureArgs();
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
        var (quantity, measureUnit) = RandomParams.GetRandomBaseMeasureArgs();
        IMeasurement measurement = _factory.GetMeasurement(measureUnit);
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measurement);

        quantity = RandomParams.GetRandomValueTypeQuantity();
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
        var (quantity, measureUnit) = RandomParams.GetRandomBaseMeasureArgs();
        IMeasurement measurement = _factory.GetMeasurement(measureUnit);
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measurement);

        quantity = RandomParams.GetRandomValueTypeQuantity();
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
        TestSupport.RemoveIfNotDefaultMeasureUnit(measureUnit);
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
        TestSupport.RemoveIfNotDefaultMeasureUnit(Pieces.Default);
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
        TestSupport.RemoveIfNotDefaultMeasureUnit(Pieces.Default);
    }
    #endregion

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
    [DataRow(Currency.EUR, 409.2987)] // SampleParams.EurExchangeRate
    public void GetExchangeRate_ReturnsExpected(Enum measureUnit, double expected)
    {
        // Arrange
        if (measureUnit is Currency.EUR)
        {
            _ = measureUnit.TryAddExchangeRate(SampleParams.EurExchangeRate);
        }

        var baseMeasure = new BaseMeasureChild(SampleParams.ZeroQuantity, measureUnit);

        // Act
        var result = baseMeasure.GetExchangeRate();
        double actual = (double)result.ToQuantity(typeof(double))!;

        // Assert
        Assert.AreEqual(expected, actual);

        // Restore
        TestSupport.RemoveIfNotDefaultMeasureUnit(measureUnit);
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
        var baseMeasure = new BaseMeasureChild(SampleParams.ZeroQuantity, expected);

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
        var (quantity, measureUnit) = RandomParams.GetRandomBaseMeasureArgs();
        IBaseMeasure expected = new BaseMeasureChild(quantity, measureUnit);

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
        var (quantity, measureUnit) = RandomParams.GetRandomBaseMeasureArgs();
        IBaseMeasure expected = new BaseMeasureChild(quantity, measureUnit);

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
        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();

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
        var (quantity, measureUnit) = RandomParams.GetRandomBaseMeasureArgs();
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit);
        var expected = quantity;

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
        var (quantity, measureUnit) = RandomParams.GetRandomBaseMeasureArgs();
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit);

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
    public void GetQuantity_ValidRoundingModeArg_ReturnsExpected(RoundingMode roundingMode, ValueType expected)
    {
        // Arrange
        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();
        ValueType quantity = Math.PI;
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit);

        // Act
        var actual = baseMeasure.GetQuantity(roundingMode);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    #endregion

    #region GetQuantity(Type type)

    [TestMethod, TestCategory("UnitTest")]
    public void GetQuantity_NullArg_ThrowsArgumentNullException()
    {
        // Arrange
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(() => _baseMeasure.GetQuantity(null));
        Assert.AreEqual(ParamNames.type, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetQuantity_InvalidTypeArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        Type invalidType = typeof(bool);

        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => _baseMeasure.GetQuantity(invalidType));
        Assert.AreEqual(ParamNames.type, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetQuantity_UIntTypeArgWhenNegativeQuantity_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();
        ValueType quantity = SampleParams.NegativeQuantity;
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit);

        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => baseMeasure.GetQuantity(typeof(uint)));
        Assert.AreEqual(ParamNames.type, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetQuantity_ULongTypeArgWhenNegativeQuantity_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();
        ValueType quantity = SampleParams.NegativeQuantity;
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit);

        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => baseMeasure.GetQuantity(typeof(ulong)));
        Assert.AreEqual(ParamNames.type, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetQuantity_ValidTypeArg_ReturnsExpected()
    {
        // Arrange
        ValueType quantity = RandomParams.GetRandomNotNegativeValueTypeQuantity();
        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit);
        Type type = RandomParams.GetRandomQuantityType();

        var expected = quantity.ToQuantity(type);

        // Act
        var actual = baseMeasure.GetQuantity(type);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    #endregion
    #endregion

    #region GetDecimalQuaintity

    [TestMethod, TestCategory("UnitTest")]
    public void GetDecimalQuaintity_ReturnsExpected()
    {
        // Arrange
        var (quantity, measureUnit) = RandomParams.GetRandomBaseMeasureArgs();
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit);
        var expected = (decimal)quantity.ToQuantity(typeof(decimal));

        // Act
        var actual = baseMeasure.GetDecimalQuantity();

        // Assert
        Assert.AreEqual(expected, actual);
    }

    #endregion

    #region ExchangeTo
    #region ExchangeTo(Enum measureUnit)
    [DataTestMethod, TestCategory("UnitTest")]
    [DataRow(default(LimitType), null)]
    [DataRow(Currency.EUR, null)]
    [DataRow(default(VolumeUnit), null)]
    [DataRow((WeightUnit)3, null)]
    public void ExchangeTo_InvalidArg_ReturnsExpected(Enum measureUnit, IBaseMeasure expected)
    {
        // Arrange
        ValueType quantity = RandomParams.GetRandomValueTypeQuantity();
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, SampleParams.MediumValueSampleMeasureUnit);

        // Act
        var actual = baseMeasure.ExchangeTo(measureUnit);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ExchangeTo_ValidArg_ReturnsExpected()
    {
        // Arrange
        Enum measureUnit = SampleParams.MediumValueSampleMeasureUnit;
        ValueType quantity = RandomParams.GetRandomValueTypeQuantity();
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit);

        decimal decimalQuantity = (decimal)quantity.ToQuantity(typeof(decimal));
        decimalQuantity *= baseMeasure.GetExchangeRate();
        measureUnit = SampleParams.MaxValueSampleMeasureUnit;
        decimalQuantity /= measureUnit.GetExchangeRate();
        Type type = quantity.GetType();
        quantity = decimalQuantity.ToQuantity(type);

        IBaseMeasure expected = new BaseMeasureChild(quantity, measureUnit);

        // Act
        var actual = baseMeasure.ExchangeTo(measureUnit);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ExchangeTo_SameMeasureUnitArg_ReturnsExpected()
    {
        // Arrange
        var (quantity, measureUnit) = RandomParams.GetRandomBaseMeasureArgs();
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit);

        IBaseMeasure expected = new BaseMeasureChild(quantity, measureUnit);

        // Act
        var actual = baseMeasure.ExchangeTo(measureUnit);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region ExchangeTo(decimal exchangeRate)
    [TestMethod, TestCategory("UnitTest")]
    public void ExchangeTo_ZeroValueArg_ReturnsExpected()
    {
        // Arrange
        // Act
        var result = _baseMeasure.ExchangeTo(decimal.Zero);

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ExchangeTo_NegativeValueArg_ReturnsExpected()
    {
        // Arrange
        // Act
        var result = _baseMeasure.ExchangeTo(decimal.MinusOne);

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ExchangeTo_PositiveValueArg_ReturnsExpected()
    {
        // Arrange
        var (quantity, measureUnit) = RandomParams.GetRandomBaseMeasureArgs(RandomParams.RandomMeasureUnitType.Constant);
        Type expectedQuantityType = quantity.GetType();
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit);
        decimal exchangeRate = (decimal)RandomParams.GetRandomPositiveValueTypeQuantity().ToQuantity(typeof(decimal));

        decimal expectedValue = (decimal)quantity.ToQuantity(typeof(decimal)) / exchangeRate;
        expectedValue *= measureUnit.GetExchangeRate();
        expectedValue = TestSupport.GetQuantityDecimalValue(expectedValue, expectedQuantityType);

        // Act
        var actual = baseMeasure.ExchangeTo(exchangeRate);
        var actualValue = (decimal)actual.ToQuantity(typeof(decimal));
        var actualQuantityType = actual.GetType();

        // Assert
        Assert.AreEqual(expectedValue, actualValue);
        Assert.AreEqual(expectedQuantityType, actualQuantityType);
    }

    #endregion
    #endregion

    #region TryExchangeTo
    [DataTestMethod, TestCategory("UnitTest")]
    [DataRow(default(LimitType), null)]
    [DataRow(default(Currency), null)]
    [DataRow(default(VolumeUnit), null)]
    [DataRow((WeightUnit)3, null)]
    public void TryExchangeTo_InvalidArg_ReturnsExpected_OutNull(Enum measureUnit, IBaseMeasure expected)
    {
        // Arrange
        ValueType quantity = RandomParams.GetRandomValueTypeQuantity();
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, SampleParams.MediumValueSampleMeasureUnit);

        // Act
        var result = baseMeasure.TryExchangeTo(measureUnit, out IBaseMeasure actual);

        // Assert
        Assert.IsFalse(result);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void TryExchangeTo_ValidArg_ReturnsTrue_OutExpected()
    {
        // Arrange
        Enum measureUnit = SampleParams.DefaultSampleMeasureUnit;
        ValueType quantity = RandomParams.GetRandomValueTypeQuantity();
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit);

        decimal decimalQuantity = (decimal)quantity.ToQuantity(typeof(decimal));
        decimalQuantity *= baseMeasure.GetExchangeRate();
        measureUnit = SampleParams.MediumValueSampleMeasureUnit;
        decimalQuantity /= measureUnit.GetExchangeRate();
        Type type = quantity.GetType();
        quantity = decimalQuantity.ToQuantity(type);

        IBaseMeasure expected = new BaseMeasureChild(quantity, measureUnit);

        // Act
        var result = baseMeasure.TryExchangeTo(measureUnit, out IBaseMeasure actual);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void TryExchangeTo_SameMeasureUnitArg_ReturnsTrue_OutExpected()
    {
        // Arrange
        var (quantity, measureUnit) = RandomParams.GetRandomBaseMeasureArgs();
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit);

        IBaseMeasure expected = new BaseMeasureChild(quantity, measureUnit);

        // Act
        var result = baseMeasure.TryExchangeTo(measureUnit, out IBaseMeasure actual);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(expected, actual);
    }
    #endregion
}
#nullable enable
