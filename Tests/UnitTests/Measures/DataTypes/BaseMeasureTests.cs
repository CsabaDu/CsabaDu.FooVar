using CsabaDu.FooVar.Measures.Interfaces.DataTypes;
using CsabaDu.FooVar.Measures.Interfaces.Factories;
using CsabaDu.FooVar.Tests.Fakes.Measures;
using static CsabaDu.FooVar.Tests.Statics.RandomParams;
using static CsabaDu.FooVar.Tests.Statics.SampleParams;
using static CsabaDu.FooVar.Tests.Statics.TestSupport;


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
        RestoreDefaultMeasureUnits();

        Enum measureUnit =  GetRandomDefaultMeasureUnit();
        ValueType quantity =  GetRandomValueTypeQuantity();

        _baseMeasure = new BaseMeasureChild(quantity, measureUnit, null);
        _factory = _baseMeasure.MeasurementFactory;
    }
    #endregion

    #region Private methods
    private static IEnumerable<object[]> GetThreeBaseMeasureArgsWithEachDefaultMeasureUnit()
    {
        return TestSupport.GetThreeBaseMeasureArgsWithEachDefaultMeasureUnit();
    }

    private static IEnumerable<object[]> GetTwoBaseMeasureArgsWithEachDefaultMeasurement()
    {
        return TestSupport.GetTwoBaseMeasureArgsWithEachDefaultMeasurement();
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

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_EnumTypeInvalidQuantityArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        ValueType enumQuantity =  GetRandomDefaultMeasureUnit();
        Enum measureUnit =  GetRandomDefaultMeasureUnit();

        // Act
        void attempt() => _ = new BaseMeasureChild(enumQuantity, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_BoolTypeInvalidQuantityArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        ValueType boolQuantity = true;
        Enum measureUnit =  GetRandomDefaultMeasureUnit();

        // Act
        void attempt() => _ = new BaseMeasureChild(boolQuantity, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_CharTypeInvalidQuantityArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        ValueType charQuantity = char.MaxValue;
        Enum measureUnit =  GetRandomDefaultMeasureUnit();

        // Act
        void attempt() => _ = new BaseMeasureChild(charQuantity, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_IntPtrTypeInvalidQuantityArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        ValueType intPtrQuantity = IntPtr.Zero;
        Enum measureUnit =  GetRandomDefaultMeasureUnit();

        // Act
        void attempt() => _ = new BaseMeasureChild(intPtrQuantity, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_UIntPtrTypeInvalidQuantityArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        ValueType uIntPtrQuantity = UIntPtr.Zero;
        Enum measureUnit =  GetRandomDefaultMeasureUnit();

        // Act
        void attempt() => _ = new BaseMeasureChild(uIntPtrQuantity, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_ByteTypeInvalidQuantityArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        ValueType byteQuantity = byte.MaxValue;
        Enum measureUnit = GetRandomDefaultMeasureUnit();

        // Act
        void attempt() => _ = new BaseMeasureChild(byteQuantity, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_SbyteTypeInvalidQuantityArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        ValueType sbyteQuantity = sbyte.MinValue;
        Enum measureUnit = GetRandomDefaultMeasureUnit();

        // Act
        void attempt() => _ = new BaseMeasureChild(sbyteQuantity, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_ShortTypeInvalidQuantityArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        ValueType shortQuantity = short.MinValue;
        Enum measureUnit = GetRandomDefaultMeasureUnit();

        // Act
        void attempt() => _ = new BaseMeasureChild(shortQuantity, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_UshortTypeInvalidQuantityArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        ValueType ushortQuantity = ushort.MaxValue;
        Enum measureUnit = GetRandomDefaultMeasureUnit();

        // Act
        void attempt() => _ = new BaseMeasureChild(ushortQuantity, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_FloatTypeInvalidQuantityArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        ValueType floatQuantity = float.MinValue;
        Enum measureUnit = GetRandomDefaultMeasureUnit();

        // Act
        void attempt() => _ = new BaseMeasureChild(floatQuantity, measureUnit);

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
        ValueType quantity =  GetRandomValueTypeQuantity();
        Enum notMeasureUnitTypeEnum = GetRandomLimitType();

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
        ValueType quantity =  GetRandomValueTypeQuantity();
        Enum notDefinedMeasureUnit = NotDefinedSampleMeasureUnit;

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
        ValueType quantity =  GetRandomValueTypeQuantity();
        Enum measureUnitNotHavingAdHocRate = GetRandomNonDefaultMeasureUnit();

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
        ValueType quantity =  GetRandomValueTypeQuantity();
        Enum constantMeasureUnit = MediumValueSampleMeasureUnit;
        decimal? differentExchangeRate = DecimalOne;

        // Act
        void attempt() => _ = new BaseMeasureChild(quantity, constantMeasureUnit, differentExchangeRate);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(ParamNames.exchangeRate, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NonDefaultMeasureUnitAndZeroExchangeRateArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        ValueType quantity =  GetRandomValueTypeQuantity();
        Enum measureUnit = MeasureUnitShouldHaveAdHocRate;
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
        ValueType quantity =  GetRandomValueTypeQuantity();
        Enum measureUnit = MeasureUnitShouldHaveAdHocRate;
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

    //[DataTestMethod, TestCategory("UnitTest")]
    //[DataRow(0, default(AreaUnit))]
    //[DataRow(0, default(Currency))]
    //[DataRow(0, default(Pieces))]
    //[DataRow(0, default(DistanceUnit))]
    //[DataRow(0, default(ExtentUnit))]
    //[DataRow(0, default(TimeUnit))]
    //[DataRow(0, default(VolumeUnit))]
    //[DataRow(0, default(WeightUnit))]
    //[DataRow(15, WeightUnit.kg)]
    //[DataRow(627.2, (WeightUnit)2)]
    //[DataRow(-4.5, VolumeUnit.meterCubic)]
    //[DataRow(12.4, default(Currency))]
    //[DataRow(124, default(Pieces))]
    //public void Ctor_ValidQuantityAndMeasureUnitArgs_CreatesInstance(ValueType expectedQuantity, Enum expectedMeasureUnit)
    //{
    //    // Arrange
    //    _ = expectedMeasureUnit.TryAddExchangeRate(DecimalOne);
    //    decimal? nullExchangeRate = null;

    //    // Act
    //    var actual = new BaseMeasureChild(expectedQuantity, expectedMeasureUnit, nullExchangeRate);

    //    // Assert
    //    Assert.IsNotNull(actual);
    //    Assert.AreEqual(expectedQuantity, actual.Quantity);
    //    Assert.AreEqual(expectedMeasureUnit, actual.MeasureUnit);

    //    // Restore
    //    RemoveIfNonDefaultMeasureUnit(expectedMeasureUnit);
    //}

    [DataTestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetThreeBaseMeasureArgsWithEachDefaultMeasureUnit), DynamicDataSourceType.Method)]
    public void Ctor_ValidQuantityAndDefaultMeasureUnitAndValidExchangeRateArgs_CreatesInstance(ValueType expectedQuantity, Enum expectedMeasureUnit, decimal? exchangeRate)
    {
        // Arrange
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
        ValueType expectedQuantity = GetRandomValueTypeQuantity();
        Enum expectedMeasureUnit = GetRandomNonDefaultMeasureUnit();
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
        ValueType expectedQuantity = (decimal) GetRandomValueTypeQuantity().ToQuantity(typeof(decimal));

        // Act
        var actual = new BaseMeasureChild(expectedQuantity, MediumValueSampleMeasureUnit , null);

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
    [DynamicData(nameof(GetTwoBaseMeasureArgsWithEachDefaultMeasurement), DynamicDataSourceType.Method)]
    //[DataRow(0, default(AreaUnit), null)]
    //[DataRow(0, default(DistanceUnit), 1.0)]
    //[DataRow(0, default(ExtentUnit), null)]
    //[DataRow(0, default(ExtentUnit), 1.0)]
    //[DataRow(0, default(TimeUnit), null)]
    //[DataRow(0, default(VolumeUnit), 1.0)]
    //[DataRow(0, default(WeightUnit), null)]
    //[DataRow(15, WeightUnit.kg, 1000.0)]
    //[DataRow(627.2, (WeightUnit)2, null)]
    //[DataRow(-4.5, VolumeUnit.meterCubic, 1000000000.0)]
    //[DataRow(12.4, default(Currency), null)]
    //[DataRow(124, default(Pieces), 1.0)]
    //[DataRow(657196259.4617, (Currency)1, 409.2561)]
    public void Ctor_ValidQuantityAndValidMeasurementArgs_CreatesInstance(ValueType expectedQuantity, IMeasurement expectedMeasurement)
    {
        // Arrange
        // Act
        var actual = new BaseMeasureChild(expectedQuantity, expectedMeasurement);

        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(expectedQuantity, actual.Quantity);
        Assert.AreEqual(expectedMeasurement, actual.Measurement);

        //// Restore
        //RemoveIfNonDefaultMeasureUnit(measureUnit);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_ValidQuantityAndNonDefaultMeasurementArg_CreatesInstance()
    {
        // Arrange
        ValueType expectedQuantity = GetRandomValueTypeQuantity();
        Enum measureUnit = GetRandomNonDefaultMeasureUnit();
        decimal? exchangeRate = GetRandomExchangeRate();
        IMeasurement expectedMeasurement = _factory.GetMeasurement(measureUnit, exchangeRate);

        // Act
        var actual = new BaseMeasureChild(expectedQuantity, expectedMeasurement);

        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(expectedQuantity, actual.Quantity);
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
        ValueType quantity =  GetRandomValueTypeQuantity();
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

        var (quantity, exchangedQuantity) = GetAndExchangeRandomQuantity(measureUnit, targetExchangeRate);
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit, null);
        IBaseMeasure expected = new BaseMeasureChild(exchangedQuantity, targetMeasureUnit, null);

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

        var (quantity, expected) = GetAndExchangeRandomQuantity(measureUnit, exchangeRate);
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
        ValueType quantity =  GetRandomValueTypeQuantity();
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

        quantity =  GetRandomValueTypeQuantity();
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

        quantity =  GetRandomValueTypeQuantity();
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
    public void GetQuantity_ValidRoundingModeArg_ReturnsExpected(RoundingMode roundingMode, ValueType expected)
    {
        // Arrange
        Enum measureUnit =  GetRandomDefaultMeasureUnit();
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
        Enum measureUnit =  GetRandomDefaultMeasureUnit();
        ValueType quantity = NegativeQuantity;
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
        Enum measureUnit =  GetRandomDefaultMeasureUnit();
        ValueType quantity = NegativeQuantity;
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
        ValueType quantity =  GetRandomNotNegativeValueTypeQuantity();
        Enum measureUnit =  GetRandomDefaultMeasureUnit();
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit, null);
        Type type =  GetRandomQuantityType();

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
        ValueType quantity =  GetRandomValueTypeQuantity();
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

        var (quantity, exchangedQuantity) = GetAndExchangeRandomQuantity(measureUnit, targetExchangeRate);
        IBaseMeasure baseMeasure = new BaseMeasureChild(quantity, measureUnit, null);
        IBaseMeasure expected = new BaseMeasureChild(exchangedQuantity, targetMeasureUnit, null);

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

        var (quantity, expected) = GetAndExchangeRandomQuantity(measureUnit, exchangeRate);
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
