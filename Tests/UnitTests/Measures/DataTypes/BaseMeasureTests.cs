using CsabaDu.FooVar.Measures.Interfaces.Behaviors;
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

        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();
        ValueType quantity = RandomParams.GetRandomValueTypeQuantity();

        _baseMeasure = new BaseMeasureChild(quantity, measureUnit, null);
        _factory = _baseMeasure.MeasurementFactory;
    }
    #endregion

    #region Constructor
    #region Quantity type validation
    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NullQuantityArg_ThrowsArgumentNullException()
    {
        // Arrange
        ValueType nullQuantity = null;
        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();

        // Act
        void action() => _ = new BaseMeasureChild(nullQuantity, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(action);
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_EnumTypeInvalidQuantityArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        ValueType enumQuantity = RandomParams.GetRandomDefaultMeasureUnit();
        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();

        // Act
        void action() => _ = new BaseMeasureChild(enumQuantity, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(action);
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_BoolTypeInvalidQuantityArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        ValueType boolQuantity = true;
        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();

        // Act
        void action() => _ = new BaseMeasureChild(boolQuantity, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(action);
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_CharTypeInvalidQuantityArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        ValueType charQuantity = char.MaxValue;
        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();

        // Act
        void action() => _ = new BaseMeasureChild(charQuantity, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(action);
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_IntPtrTypeInvalidQuantityArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        ValueType intPtrQuantity = IntPtr.Zero;
        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();

        // Act
        void action() => _ = new BaseMeasureChild(intPtrQuantity, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(action);
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_UIntPtrTypeInvalidQuantityArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        ValueType uIntPtrQuantity = UIntPtr.Zero;
        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();

        // Act
        void action() => _ = new BaseMeasureChild(uIntPtrQuantity, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(action);
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }
    #endregion

    #region BaseMeasure(ValueType quantity, Enum measureUnit, decimal? exchangeRate = null)
    #region MeasureUnitType validation
    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NullMeasureUnitArg_ThrowsArgumentNullException()
    {
        // Arrange
        ValueType quantity = RandomParams.GetRandomValueTypeQuantity();
        Enum nullMeasureUnit = null;

        // Act
        void action() => _ = new BaseMeasureChild(quantity, nullMeasureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(action);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NotMeasureUnitTypeEnumArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        ValueType quantity = RandomParams.GetRandomValueTypeQuantity();
        Enum notMeasureUnitTypeEnum = SampleParams.NotMeasureUnitTypeEnum;

        // Act
        void action() => _ = new BaseMeasureChild(quantity, notMeasureUnitTypeEnum);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(action);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NotDefinedMeasureUnitArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        ValueType quantity = RandomParams.GetRandomValueTypeQuantity();
        Enum notDefinedMeasureUnit = SampleParams.NotDefinedSampleMeasureUnit;

        // Act
        void action() => _ = new BaseMeasureChild(quantity, notDefinedMeasureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(action);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }
    #endregion

    #region ExchangeRate validation
    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_MeasureUnitDoesNotHaveExchangeRateArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        ValueType quantity = RandomParams.GetRandomValueTypeQuantity();
        Enum measureUnitNotHavingAdHocRate = SampleParams.MeasureUnitShouldHaveAdHocRate;

        // Act
        void action() => _ = new BaseMeasureChild(quantity, measureUnitNotHavingAdHocRate);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(action);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_DefaultMeasureUnitAndDifferentExchangeRateArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        ValueType quantity = RandomParams.GetRandomValueTypeQuantity();
        Enum definedMeasureUnit = SampleParams.MediumValueSampleMeasureUnit;
        decimal? differentExchangeRate = SampleParams.DecimalOne;

        // Act
        void action() => _ = new BaseMeasureChild(quantity, definedMeasureUnit, differentExchangeRate);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(action);
        Assert.AreEqual(ParamNames.exchangeRate, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NonDefaultMeasureUnitAndZeroExchangeRateArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        ValueType quantity = RandomParams.GetRandomValueTypeQuantity();
        Enum measureUnit = SampleParams.MeasureUnitShouldHaveAdHocRate;
        decimal? zeroExchangeRate = SampleParams.DecimalZero;

        // Act
        void action() => _ = new BaseMeasureChild(quantity, measureUnit, zeroExchangeRate);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(action);
        Assert.AreEqual(ParamNames.exchangeRate, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NonDefaultMeasureUnitAndNegativeExchangeRateArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        ValueType quantity = RandomParams.GetRandomValueTypeQuantity();
        Enum measureUnit = SampleParams.MeasureUnitShouldHaveAdHocRate;
        decimal? negativeExchangeRate = SampleParams.DecimalMinusOne;

        // Act
        void action() => _ = new BaseMeasureChild(quantity, measureUnit, negativeExchangeRate);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(action);
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
        void action() => _ = new BaseMeasureChild(nullQuantity, nullMeasureUnit, nullExchangeRate);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(action);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NullMeasureUnitAndNullExchangeRateArg_ThrowsArgumentNullException()
    {
        // Arrange
        ValueType quantity = RandomParams.GetRandomValueTypeQuantity();
        Enum nullMeasureUnit = null;
        decimal? nullExchangeRate = null;

        // Act
        void action() => _ = new BaseMeasureChild(quantity, nullMeasureUnit, nullExchangeRate);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(action);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NullQuantityAndNullExchangeRateArg_ThrowsArgumentNullException()
    {
        // Arrange
        ValueType nullQuantity = null;
        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();
        decimal? nullExchangeRate = null;

        // Act
        void action() => _ = new BaseMeasureChild(nullQuantity, measureUnit, nullExchangeRate);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(action);
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
        decimal? nullExchangeRate = null;

        // Act
        var actual = new BaseMeasureChild(expectedQuantity, expectedMeasureUnit, nullExchangeRate);

        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(expectedQuantity, actual.Quantity);
        Assert.AreEqual(expectedMeasureUnit, actual.MeasureUnit);

        // Restore
        TestSupport.RemoveIfNotDefaultMeasureUnit(expectedMeasureUnit);
    }

    [DataTestMethod, TestCategory("UnitTest")]
    [DataRow(0, default(AreaUnit), null)]
    [DataRow(0, default(DistanceUnit), 1.0)]
    [DataRow(0, default(ExtentUnit), null)]
    [DataRow(0, default(ExtentUnit), 1.0)]
    [DataRow(0, default(TimeUnit), null)]
    [DataRow(0, default(VolumeUnit), 1.0)]
    [DataRow(0, default(WeightUnit), null)]
    [DataRow(15, WeightUnit.kg, 1000.0)]
    [DataRow(627.2, (WeightUnit)2, null)]
    [DataRow(-4.5, VolumeUnit.meterCubic, 1000000000.0)]
    [DataRow(12.4, default(Currency), null)]
    [DataRow(124, default(Pieces), 1.0)]
    [DataRow(657196259.4617, (Currency)1, 409.6885)]
    public void Ctor_ThreeValidArgs_CreatesInstance(ValueType expectedQuantity, Enum expectedMeasureUnit, double? exchangeRate)
    {
        // Arrange
        decimal? decimalExchangeRate = (decimal?)exchangeRate?.ToQuantity(typeof(decimal));
        _ = expectedMeasureUnit.TryAddExchangeRate(decimalExchangeRate ?? SampleParams.DecimalOne);

        // Act
        var actual = new BaseMeasureChild(expectedQuantity, expectedMeasureUnit, decimalExchangeRate);

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
        var actual = new BaseMeasureChild(quantity, SampleParams.MediumValueSampleMeasureUnit, null);

        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(expectedQuantity, actual.Quantity);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_ValidDecimalTypeQuantityArg_CreatesInstance()
    {
        // Arrange
        ValueType expectedQuantity = (decimal)RandomParams.GetRandomValueTypeQuantity().ToQuantity(typeof(decimal));

        // Act
        var actual = new BaseMeasureChild(expectedQuantity, SampleParams.MediumValueSampleMeasureUnit , null);

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
        void action() => _ = new BaseMeasureChild(nullQuantity, nullMeasurement);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(action);
        Assert.AreEqual(ParamNames.measurement, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_ValidQuantityAndNullMeasurementArg_ThrowsArgumentNullException()
    {
        // Arrange
        ValueType quantity = RandomParams.GetRandomValueTypeQuantity();
        IMeasurement nullMeasurement = null;

        // Act
        void action() => _ = new BaseMeasureChild(quantity, nullMeasurement);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(action);
        Assert.AreEqual(ParamNames.measurement, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NullQuantityAndValidMeasurementArg_ThrowsArgumentNullException()
    {
        // Arrange
        ValueType nullQuantity = null;
        IMeasurement measurement = _factory.GetMeasurement(SampleParams.MediumValueSampleMeasureUnit);

        // Act
        void action() => _ = new BaseMeasureChild(nullQuantity, measurement);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(action);
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }

    [DataTestMethod, TestCategory("UnitTest")]
    [DataRow(0, default(AreaUnit), null)]
    [DataRow(0, default(DistanceUnit), 1.0)]
    [DataRow(0, default(ExtentUnit), null)]
    [DataRow(0, default(ExtentUnit), 1.0)]
    [DataRow(0, default(TimeUnit), null)]
    [DataRow(0, default(VolumeUnit), 1.0)]
    [DataRow(0, default(WeightUnit), null)]
    [DataRow(15, WeightUnit.kg, 1000.0)]
    [DataRow(627.2, (WeightUnit)2, null)]
    [DataRow(-4.5, VolumeUnit.meterCubic, 1000000000.0)]
    [DataRow(12.4, default(Currency), null)]
    [DataRow(124, default(Pieces), 1.0)]
    [DataRow(657196259.4617, (Currency)1, 409.2561)]
    public void Ctor_ValidQuantityAndValidMeasurementArgs_CreatesInstance(ValueType expectedQuantity, Enum measureUnit, double? exchangeRate)
    {
        // Arrange
        decimal? decimalExchangeRate = (decimal?)exchangeRate?.ToQuantity(typeof(decimal));
        IMeasurement expectedMeasurement = _factory.GetMeasurement(measureUnit, decimalExchangeRate);

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
        void action() => _ = new BaseMeasureChild(baseMeasure);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(action);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_ValidArg_CreatesInstance()
    {
        // Arrange
        var (quantity, measureUnit) = RandomParams.GetRandomBaseMeasureArgs();
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
    public void ExchangeTo_InvalidMeasureUnitArg_ReturnsExpected(Enum measureUnit, IBaseMeasure expected)
    {
        // Arrange
        ValueType quantity = RandomParams.GetRandomValueTypeQuantity();
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, SampleParams.MediumValueSampleMeasureUnit, null);

        // Act
        var actual = baseMeasure.ExchangeTo(measureUnit);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ExchangeTo_ValidMeasureUnitArg_ReturnsExpected()
    {
        // Arrange
        Enum measureUnit = SampleParams.MediumValueSampleMeasureUnit;
        ValueType quantity = RandomParams.GetRandomValueTypeQuantity();
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit, null);

        decimal decimalQuantity = (decimal)quantity.ToQuantity(typeof(decimal));
        decimalQuantity *= baseMeasure.GetExchangeRate();
        measureUnit = SampleParams.MaxValueSampleMeasureUnit;
        decimalQuantity /= measureUnit.GetExchangeRate();
        Type type = quantity.GetType();
        quantity = decimalQuantity.ToQuantity(type);

        IBaseMeasure expected = new BaseMeasureChild(quantity, measureUnit, null);

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
    public void ExchangeTo_ZeroExchangeRateArg_ReturnsExpected()
    {
        // Arrange
        // Act
        var result = _baseMeasure.ExchangeTo(SampleParams.DecimalZero);

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ExchangeTo_NegativeExchangeRateArg_ReturnsExpected()
    {
        // Arrange
        // Act
        var result = _baseMeasure.ExchangeTo(SampleParams.DecimalNegative);

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ExchangeTo_PositiveExchangeRateArg_ReturnsExpected()
    {
        // Arrange
        var (quantity, measureUnit) = RandomParams.GetRandomBaseMeasureArgs(RandomParams.RandomMeasureUnitType.Constant);
        Type expectedQuantityType = quantity.GetType();
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit, null);
        decimal exchangeRate = (decimal)RandomParams.GetRandomPositiveValueTypeQuantity().ToQuantity(typeof(decimal));

        decimal expectedValue = (decimal)quantity.ToQuantity(typeof(decimal));
        expectedValue /= exchangeRate;
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
        ValueType quantity = RandomParams.GetRandomValueTypeQuantity();
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, SampleParams.MediumValueSampleMeasureUnit, null);

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
        var (quantity, measureUnit) = RandomParams.GetRandomBaseMeasureArgs();
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
        var (quantity, measureUnit) = RandomParams.GetRandomBaseMeasureArgs();
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
        var (quantity, measureUnit) = RandomParams.GetRandomBaseMeasureArgs();
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

    #region GetDecimalQuantity
    [TestMethod, TestCategory("UnitTest")]
    public void GetDecimalQuantity_ReturnsExpected()
    {
        // Arrange
        var (quantity, measureUnit) = RandomParams.GetRandomBaseMeasureArgs();
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
    [DataRow(Currency.EUR, 409.2987)] // SampleParams.EurExchangeRate
    public void GetExchangeRate_ReturnsExpected(Enum measureUnit, double expected)
    {
        // Arrange
        if (measureUnit is Currency.EUR)
        {
            _ = measureUnit.TryAddExchangeRate(SampleParams.EurExchangeRate);
        }

        var baseMeasure = new BaseMeasureChild(SampleParams.ZeroQuantity, measureUnit, null);

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
        var baseMeasure = new BaseMeasureChild(SampleParams.ZeroQuantity, expected, null);

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
        var (quantity, measureUnit) = RandomParams.GetRandomBaseMeasureArgs();
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
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit, null);
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
    public void GetQuantity_ValidRoundingModeArg_ReturnsExpected(RoundingMode roundingMode, ValueType expected)
    {
        // Arrange
        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();
        ValueType quantity = Math.PI;
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit, null);

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
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit, null);

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
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit, null);

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
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit, null);
        Type type = RandomParams.GetRandomQuantityType();

        var expected = quantity.ToQuantity(type);

        // Act
        var actual = baseMeasure.GetQuantity(type);

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
        ValueType quantity = RandomParams.GetRandomValueTypeQuantity();
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, SampleParams.MediumValueSampleMeasureUnit, null);

        // Act
        var result = baseMeasure.TryExchangeTo(measureUnit, out IBaseMeasure actual);

        // Assert
        Assert.IsFalse(result);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void TryExchangeTo_ValidMeasureUnitArg_ReturnsTrue_OutExpected()
    {
        // Arrange
        Enum measureUnit = SampleParams.DefaultSampleMeasureUnit;
        ValueType quantity = RandomParams.GetRandomValueTypeQuantity();
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit, null);

        decimal decimalQuantity = (decimal)quantity.ToQuantity(typeof(decimal));
        decimalQuantity *= baseMeasure.GetExchangeRate();
        measureUnit = SampleParams.MediumValueSampleMeasureUnit;
        decimalQuantity /= measureUnit.GetExchangeRate();
        Type type = quantity.GetType();
        quantity = decimalQuantity.ToQuantity(type);

        IBaseMeasure expected = new BaseMeasureChild(quantity, measureUnit, null);

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
        var result = _baseMeasure.TryExchangeTo(SampleParams.DecimalZero, out ValueType actual);

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
        var result = _baseMeasure.TryExchangeTo(SampleParams.DecimalNegative, out ValueType actual);

        // Assert
        Assert.IsFalse(result);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void TryExchangeTo_PositiveExchangeRateArg_ReturnsExpected()
    {
        // Arrange
        var (quantity, measureUnit) = RandomParams.GetRandomBaseMeasureArgs(RandomParams.RandomMeasureUnitType.Constant);
        Type expectedQuantityType = quantity.GetType();
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit, null);
        decimal exchangeRate = (decimal)RandomParams.GetRandomPositiveValueTypeQuantity().ToQuantity(typeof(decimal));

        decimal expectedValue = (decimal)quantity.ToQuantity(typeof(decimal));
        expectedValue /= exchangeRate;
        expectedValue *= measureUnit.GetExchangeRate();
        expectedValue = TestSupport.GetQuantityDecimalValue(expectedValue, expectedQuantityType);

        // Act
        var result = baseMeasure.TryExchangeTo(exchangeRate, out ValueType actual);
        var actualValue = (decimal)actual.ToQuantity(typeof(decimal));
        var actualQuantityType = actual.GetType();

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(expectedValue, actualValue);
        Assert.AreEqual(expectedQuantityType, actualQuantityType);
    }
    #endregion
    #endregion
}
#nullable enable
