using CsabaDu.FooVar.Measures.Factories;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes;
using CsabaDu.FooVar.Measures.Interfaces.Factories;

namespace CsabaDu.FooVar.Tests.UnitTests.Measures.DataTypes;

#nullable disable
[TestClass]
public class MeasurementTests
{
    #region Private fields
    private IMeasurementFactory _factory;
    private IMeasurement _measurement;
    #endregion

    #region TestInitialize
    [TestInitialize]
    public void InitializeMeasurementTests()
    {
        TestSupport.RestoreDefaultMeasureUnits();

        _factory = new MeasurementFactory();

        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();
        _measurement = _factory.GetMeasurement(measureUnit);
    }
    #endregion

    #region Private methods
    private static IEnumerable<object[]> GetInvalidMeasureUnitArg()
    {
        return TestSupport.GetInvalidMeasureUnitArg();
    }
    #endregion

    #region Constructor
    #region Null args
    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NullArgs_ThrowsArgumentNullException()
    {
        // Arrange
        Enum nullMeasureUnit = null;
        decimal? nullExchangeRate = null;

        // Act
        void action() => _ = new Measurement(nullMeasureUnit, nullExchangeRate);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(action);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }
    #endregion

    #region MeasureUnit validation
    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NullMeasureUnitArg_ThrowsArgumentNullException()
    {
        // Arrange
        Enum measureUnit = null;
        decimal? exchangeRate = RandomParams.GetRandomExchangeRate();

        // Act
        void action() => _ = new Measurement(measureUnit, exchangeRate);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(action);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [DataTestMethod]
    [DynamicData(nameof(GetInvalidMeasureUnitArg), DynamicDataSourceType.Method)]
    public void Ctor_InvalidMeasureUnitArg_ThrowsArgumentOutOfRangeException(Enum measureUnit)
    {
        // Arrange
        decimal? exchangeRate = null;

        // Act
        void action() => _ = new Measurement(measureUnit, exchangeRate);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(action);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    //[TestMethod, TestCategory("UnitTest")]
    //public void Ctor_NotMeasureUnitTypeEnumArg_ThrowsArgumentOutOfRangeException()
    //{
    //    // Arrange
    //    Enum notMeasureUnitTypeEnum = SampleParams.NotMeasureUnitTypeEnum;
    //    decimal? exchangeRate = RandomParams.GetRandomExchangeRate();

    //    // Act
    //    void action() => _ = new Measurement(notMeasureUnitTypeEnum, exchangeRate);

    //    // Assert
    //    var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(action);
    //    Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void Ctor_NotDefinedMeasureUnitArg_ThrowsArgumentOutOfRangeException()
    //{
    //    // Arrange
    //    Enum notDefinedMeasureUnit = SampleParams.NotDefinedSampleMeasureUnit;
    //    decimal? exchangeRate = RandomParams.GetRandomExchangeRate();

    //    // Act
    //    void action() => _ = new Measurement(notDefinedMeasureUnit, exchangeRate);

    //    // Assert
    //    var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(action);
    //    Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void Ctor_MeasureUnitDoesNotHaveExchangeRateArg_ThrowsArgumentOutOfRangeException()
    //{
    //    // Arrange
    //    Enum measureUnitNotHavingAdHocRate = SampleParams.MeasureUnitShouldHaveAdHocRate;
    //    decimal? nullExchangeRate = null;

    //    // Act
    //    void action() => _ = new Measurement(measureUnitNotHavingAdHocRate, nullExchangeRate);

    //    // Assert
    //    var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(action);
    //    Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    //}
    #endregion

    #region ExchangeRate validation
    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_ConstantMeasureUnitAndDifferentExchangeRateArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        Enum constantMeasureUnit = SampleParams.MediumValueSampleMeasureUnit;
        decimal? differentExchangeRate = SampleParams.DecimalOne;

        // Act
        void action() => _ = new Measurement(constantMeasureUnit, differentExchangeRate);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(action);
        Assert.AreEqual(ParamNames.exchangeRate, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NotConstantMeasureUnitAndDifferentExchangeRateArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        Enum measureUnitHasAdHocRate = SampleParams.MeasureUnitShouldHaveAdHocRate;
        _ = measureUnitHasAdHocRate.TryAddExchangeRate(SampleParams.DecimalOne);
        decimal? differentExchangeRate = SampleParams.DecimalPositive;

        void action() => _ = new Measurement(measureUnitHasAdHocRate, differentExchangeRate);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(action);
        Assert.AreEqual(ParamNames.exchangeRate, ex.ParamName);

        // Restore
        TestSupport.RemoveIfNonDefaultMeasureUnit(measureUnitHasAdHocRate);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NonConstantMeasureUnitAndZeroExchangeRateArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        Enum measureUnitNotHavingExchangeRate = SampleParams.MeasureUnitShouldHaveAdHocRate;
        decimal? zeroExchangeRate = SampleParams.DecimalZero;

        // Act
        void action() => _ = new Measurement(measureUnitNotHavingExchangeRate, zeroExchangeRate);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(action);
        Assert.AreEqual(ParamNames.exchangeRate, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NonConstantMeasureUnitAndNegativeExchangeRateArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        Enum measureUnitNotHavingExchangeRate = SampleParams.MeasureUnitShouldHaveAdHocRate;
        decimal? negativeExchangeRate = SampleParams.DecimalNegative;

        // Act
        void action() => _ = new Measurement(measureUnitNotHavingExchangeRate, negativeExchangeRate);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(action);
        Assert.AreEqual(ParamNames.exchangeRate, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_ConstantMeasureUnitAndDifferenteExchangeRateArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        Enum measureUnitShouldHaveAdHocRate = SampleParams.MeasureUnitShouldHaveAdHocRate;
        _ = measureUnitShouldHaveAdHocRate.TryAddExchangeRate(SampleParams.DecimalPositive);
        decimal? differentExchangeRate = SampleParams.DecimalNegative;

        // Act
        void action() => _ = new Measurement(measureUnitShouldHaveAdHocRate, differentExchangeRate);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(action);
        Assert.AreEqual(ParamNames.exchangeRate, ex.ParamName);

        // Restore
        TestSupport.RemoveIfNonDefaultMeasureUnit(SampleParams.MeasureUnitShouldHaveAdHocRate);
    }
    #endregion

    #region Valid args
    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_DefaultMeasureUnitAndSameExchangeRateArgs_CreatesInstance()
    {
        // Arrange
        Enum expectedMeasureUnit = SampleParams.DefaultSampleMeasureUnit;
        decimal? expectedExchangeRate = SampleParams.DecimalOne;

        // Act
        var actual = new Measurement(expectedMeasureUnit, expectedExchangeRate);

        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(expectedMeasureUnit, actual.MeasureUnit);
        Assert.AreEqual(expectedExchangeRate, actual.ExchangeRate);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NonDefaultMeasureUnitAndValidExchangeRateArgs_CreatesInstance()
    {
        // Arrange
        Enum expectedMeasureUnit = SampleParams.MeasureUnitShouldHaveAdHocRate;
        decimal? expectedExchangeRate = RandomParams.GetRandomExchangeRate();

        // Act
        var actual = new Measurement(expectedMeasureUnit, expectedExchangeRate);

        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(expectedMeasureUnit, actual.MeasureUnit);
        Assert.AreEqual(expectedExchangeRate, actual.ExchangeRate);

        // Restore
        TestSupport.RemoveIfNonDefaultMeasureUnit(expectedMeasureUnit);
    }

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
    [DataRow(VolumeUnit.meterCubic)]
    public void Ctor_ConstantMeasureUnitAndNullExchangeRateArgs_CreatesInstance(Enum expectedMeasureUnit)
    {
        // Arrange
        decimal? nullExchangeRate = null;
        decimal expectedExchangeRate = expectedMeasureUnit.GetExchangeRate();

        // Act
        var actual = _ = new Measurement(expectedMeasureUnit, nullExchangeRate);

        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(expectedMeasureUnit, actual.MeasureUnit);
        Assert.AreEqual(expectedExchangeRate, actual.ExchangeRate);
    }

    [DataTestMethod, TestCategory("UnitTest")]
    [DataRow(default(AreaUnit), 1)]
    [DataRow(default(Currency), 1)]
    [DataRow(default(Pieces), 1)]
    [DataRow(default(DistanceUnit), 1)]
    [DataRow(default(ExtentUnit), 1)]
    [DataRow(default(TimeUnit), 1)]
    [DataRow(default(VolumeUnit), 1)]
    [DataRow(default(WeightUnit), 1)]
    [DataRow(WeightUnit.kg, 1000)]
    [DataRow((WeightUnit)2, 1000000)]
    [DataRow((Currency)1, 2.3217)]
    public void Ctor_ValidMeasureUnitAndNullExchangeRateArg_CreatesInstance(Enum expectedMeasureUnit, double exchangeRate)
    {
        // Arrange
        decimal expectedExchangeRate = (decimal)exchangeRate;
        _ = expectedMeasureUnit.TryAddExchangeRate(expectedExchangeRate);

        // Act
        var actual = new Measurement(expectedMeasureUnit, null);

        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(expectedMeasureUnit, actual.MeasureUnit);
        Assert.AreEqual(expectedExchangeRate, actual.ExchangeRate);

        // Restore
        TestSupport.RemoveIfNonDefaultMeasureUnit(expectedMeasureUnit);
    }
    #endregion
    #endregion

    #region CanExchangeTo
    [DataTestMethod, TestCategory("UnitTest")]
    [DataRow(null, false)]
    [DataRow(default(LimitType), false)]
    [DataRow(default(AreaUnit), false)]
    [DataRow(default(Currency), false)]
    [DataRow(default(Pieces), false)]
    [DataRow(default(DistanceUnit), false)]
    [DataRow(default(ExtentUnit), false)]
    [DataRow(default(TimeUnit), false)]
    [DataRow(default(VolumeUnit), false)]
    [DataRow(default(WeightUnit), true)]
    [DataRow(WeightUnit.kg, true)]
    [DataRow((WeightUnit)2, true)]
    [DataRow((WeightUnit)3, false)]
    public void CanExchangeTo_AnyArg_ReturnsExpected(Enum measureUnit, bool expected)
    {
        // Arrange
        IMeasurement measurement = _factory.GetMeasurement(SampleParams.MediumValueSampleMeasureUnit);

        // Act
        var actual = measurement.IsExchangeableTo(measureUnit);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region CompareTo
    [TestMethod, TestCategory("UnitTest")]
    public void CompareTo_NullArg_ReturnsExpected()
    {
        // Arrange
        IMeasurement nullMeasurement = null;

        // Act
        var result = _measurement.CompareTo(nullMeasurement);

        // Assert
        Assert.IsTrue(result > 0);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CompareTo_DifferentTypeMeasurementArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        Enum sampleMeasureUnit = SampleParams.MediumValueSampleMeasureUnit;
        IMeasurement measurement = _factory.GetMeasurement(sampleMeasureUnit);
        Enum differentTypeMeasureUnit = SampleParams.DifferentTypeSampleMeasureUnit;
        IMeasurement differentMeasurement = _factory.GetMeasurement(differentTypeMeasureUnit);

        // Act
        void action() => _ = measurement.CompareTo(differentMeasurement);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(action);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [DataTestMethod, TestCategory("UnitTest")]
    [DataRow(WeightUnit.g, 1)]
    [DataRow(WeightUnit.kg, 0)]
    [DataRow(WeightUnit.ton, -1)]
    public void CompareTo_ValidArg_ReturnsExpectedValue(Enum otherMeasureUnit, int expected)
    {
        // Arrange
        IMeasurement measurement = _factory.GetMeasurement(SampleParams.MediumValueSampleMeasureUnit);
        IMeasurement other = _factory.GetMeasurement(otherMeasureUnit);

        // Act
        var actual = measurement.CompareTo(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region Equals
    [TestMethod, TestCategory("UnitTest")]
    public void Equals_NullArg_ReturnsFalse()
    {
        // Arrange
        IMeasurement nullMeasurement = null;

        // Act
        var actual = _measurement.Equals(nullMeasurement);

        // Assert
        Assert.IsFalse(actual);
    }

    [DataTestMethod, TestCategory("UnitTest")]
    [DataRow(default(AreaUnit), 1.0, false)]
    [DataRow(default(Currency), 1.0, false)]
    [DataRow(default(Pieces), 1.0, false)]
    [DataRow(default(DistanceUnit), null, false)]
    [DataRow(default(ExtentUnit), null, false)]
    [DataRow(default(TimeUnit), null, false)]
    [DataRow(default(VolumeUnit), null, false)]
    [DataRow(default(WeightUnit), null, false)]
    [DataRow(WeightUnit.kg, null, true)]
    [DataRow((WeightUnit)2, null, false)]
    [DataRow((Currency)1, 409.6885, false)]
    public void Equals_ValidArg_ReturnsExpected(Enum otherMeasureUnit, double? exchangeRate, bool expected)
    {
        // Arrange
        Enum sampleMeasureUnit = SampleParams.MediumValueSampleMeasureUnit;
        IMeasurement measurement = _factory.GetMeasurement(sampleMeasureUnit);

        decimal? decimalExchangeRate = (decimal?)exchangeRate?.ToQuantity(typeof(decimal));
        IMeasurement other = _factory.GetMeasurement(otherMeasureUnit, decimalExchangeRate);

        // Act
        var actual = measurement.Equals((object)other);

        // Assert
        Assert.AreEqual(expected, actual);

        // Restore
        TestSupport.RemoveIfNonDefaultMeasureUnit(otherMeasureUnit);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Equals_NotMeasurementObjectArg_ReturnsFalse()
    {
        // Arrange
        object notMeasurementObject = new();

        // Act
        var actual = _measurement.Equals(notMeasurementObject);

        // Assert
        Assert.IsFalse(actual);
    }
    #endregion

    #region GetHashCode
    [TestMethod, TestCategory("UnitTest")]
    public void GetHashCode_ReturnsExpectedValue()
    {
        // Arrange
        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();
        IMeasurement measurement = _factory.GetMeasurement(measureUnit);
        Type measureUnitType = measurement.MeasureUnitType;
        decimal exchangeRate = measurement.ExchangeRate;
        int expected = HashCode.Combine(measureUnitType, exchangeRate);

        // Act
        var actual = measurement.GetHashCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region GetMeasurable
    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurable_NullArg_ReturnsExpected()
    {
        // Arrange
        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();
        IMeasurement expected = _factory.GetMeasurement(measureUnit);

        // Act
        var actual = expected.GetMeasurable(null);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurable_ReturnsExpected()
    {
        // Arrange
        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();
        IMeasurement expected = _factory.GetMeasurement(measureUnit);

        // Act
        var actual = expected.GetMeasurable();

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurable_ValidMeasureUnitArg_ReturnsExpected()
    {
        // Arrange
        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();
        IMeasurement expected = _factory.GetMeasurement(measureUnit);

        // Act
        var actual = _measurement.GetMeasurable(measureUnit);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region GetMeasurement
    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurement_ReturnsExpected()
    {
        // Arrange
        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();
        IMeasurement expected = _factory.GetMeasurement(measureUnit);

        // Act
        var actual = expected.GetMeasurement();

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurement_NullArg_ReturnsExpected()
    {
        // Arrange
        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();
        IMeasurement expected = _factory.GetMeasurement(measureUnit);

        // Act
        var actual = expected.GetMeasurement(null);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurement_ValidMeasurementArg_ReturnsExpected()
    {
        // Arrange
        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();
        IMeasurement expected = _factory.GetMeasurement(measureUnit);

        // Act
        var actual = _measurement.GetMeasurement(expected);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurement_ValidConstantMeasureUnitArg_ReturnsExpected()
    {
        // Arrange
        Enum measureUnit = RandomParams.GetRandomConstantMeasureUnit();
        IMeasurement expected = _factory.GetMeasurement(measureUnit);

        // Act
        var actual = _measurement.GetMeasurement(measureUnit);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurement_ValidConstantMeasureUnitAndSameExchangeRateArg_ReturnsExpected()
    {
        // Arrange
        Enum expectedMeasureUnit = RandomParams.GetRandomConstantMeasureUnit();
        decimal expectedExchangeRate = expectedMeasureUnit.GetExchangeRate();

        // Act
        var actual = _measurement.GetMeasurement(expectedMeasureUnit, expectedExchangeRate);

        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(expectedMeasureUnit, actual.GetMeasureUnit());
        Assert.AreEqual(expectedExchangeRate, actual.ExchangeRate);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurement_ValidMeasurementAndDifferentExchangeRateArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        Enum measureUnit = SampleParams.DefaultPieces;
        decimal differentExchangeRate = SampleParams.DecimalPositive;

        // Act
        void action() => _ = _measurement.GetMeasurement(measureUnit, differentExchangeRate);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(action);
        Assert.AreEqual(ParamNames.exchangeRate, ex.ParamName);
    }
    #endregion

    #region GetMeasureUnit
    [DataTestMethod, TestCategory("UnitTest")]
    [DataRow(default(AreaUnit), 1)]
    [DataRow(default(Currency), 1)]
    [DataRow(default(Pieces), 1)]
    [DataRow(default(DistanceUnit), null)]
    [DataRow(default(ExtentUnit), null)]
    [DataRow(default(TimeUnit), null)]
    [DataRow(default(VolumeUnit), null)]
    [DataRow(default(WeightUnit), null)]
    [DataRow(WeightUnit.kg, null)]
    [DataRow((WeightUnit)2, null)]
    public void GetMeasureUnit_ReturnsExpected(Enum excpected, int? exchangeRate)
    {
        // Arrange
        IMeasurement measurement = _factory.GetMeasurement(excpected, exchangeRate);

        // Act
        var actual = measurement.GetMeasureUnit();

        // Assert
        Assert.AreEqual(excpected, actual);

        // Restore
        TestSupport.RemoveIfNonDefaultMeasureUnit(excpected);
    }
    #endregion

    #region GetMeasureUnitType
    [DataTestMethod, TestCategory("UnitTest")]
    [DataRow(default(AreaUnit), 1, typeof(AreaUnit))]
    [DataRow(default(Currency), 1, typeof(Currency))]
    [DataRow(default(Pieces), 1, typeof(Pieces))]
    [DataRow(default(DistanceUnit), null, typeof(DistanceUnit))]
    [DataRow(default(ExtentUnit), null, typeof(ExtentUnit))]
    [DataRow(default(TimeUnit), null, typeof(TimeUnit))]
    [DataRow(default(VolumeUnit), null, typeof(VolumeUnit))]
    [DataRow(default(WeightUnit), null, typeof(WeightUnit))]
    public void GetMeasureUnitType_ReturnsExpected(Enum measureUnit, int? exchangeRate, Type expected)
    {
        // Arrange
        IMeasurement measurement = _factory.GetMeasurement(measureUnit, exchangeRate);

        // Act
        var actual = measurement.GetMeasureUnitType();

        // Assert
        Assert.AreEqual(expected, actual);

        // Restore
        TestSupport.RemoveIfNonDefaultMeasureUnit(measureUnit);
    }
    #endregion

    #region ProportionalTo
    [TestMethod, TestCategory("UnitTest")]
    public void ProportionalTo_NullArg_ThrowsArgumentNullException()
    {
        // Arrange
        IMeasurement nullMeasurement = null;

        // Act
        void action() => _ = _measurement.ProportionalTo(nullMeasurement);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(action);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ProportionalTo_DifferentMeasurementArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        Enum measureUnit = SampleParams.MediumValueSampleMeasureUnit;
        IMeasurement measurement = _factory.GetMeasurement(measureUnit);
        Enum differentTypeMeasureUnit = SampleParams.DifferentTypeSampleMeasureUnit;
        IMeasurement differentMeasurement = _factory.GetMeasurement(differentTypeMeasureUnit);

        // Act
        void action() => _ = measurement.ProportionalTo(differentMeasurement);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(action);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ProportionalTo_ValidArg_ReturnsExpectedValue()
    {
        // Arrange
        Enum measureUnit = SampleParams.MediumValueSampleMeasureUnit;
        IMeasurement measurement = _factory.GetMeasurement(measureUnit);
        Enum sameTypeMeasureUnit = SampleParams.DefaultSampleMeasureUnit;
        IMeasurement sameTypeMeasurement = _factory.GetMeasurement(sameTypeMeasureUnit);
        decimal expected = measurement.ExchangeRate / sameTypeMeasurement.ExchangeRate;

        // Act
        var actual = measurement.ProportionalTo(sameTypeMeasurement);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
}
#nullable enable

#region Static operator Equal
////[TestMethod, TestCategory("UnitTest")]
////public void TM42_StaticOperatorEqual_NullLeftArg_ReturnsFalse()
////{
////    // Arrange

////    Measurement result = _factory.GetMeasurement(SampleParams.MediumValueSampleMeasureUnit) as Measurement;
////    // Act

////    var result = result == null;
////    // Assert

////    Assert.IsFalse(result);
////}

////[TestMethod, TestCategory("UnitTest")]
////public void TM43_StaticOperatorEqual_NullRightArg_ReturnsFalse()
////{
////    // Arrange

////    Measurement result = _factory.GetMeasurement(SampleParams.MediumValueSampleMeasureUnit) as Measurement;
////    // Act

////    var result = null == result;
////    // Assert

////    Assert.IsFalse(result);
////}

//[DataTestMethod, TestCategory("UnitTest")]
//[DataRow(default(AreaUnit), 1, false)]
//[DataRow(default(Currency), 1, false)]
//[DataRow(default(Pieces), 1, false)]
//[DataRow(default(DistanceUnit), null, false)]
//[DataRow(default(ExtentUnit), null, false)]
//[DataRow(default(TimeUnit), null, false)]
//[DataRow(default(VolumeUnit), null, false)]
//[DataRow(default(WeightUnit), null, false)]
//[DataRow(WeightUnit.kg, null, true)]
//[DataRow((WeightUnit)2, null, false)]
//public void TM44_StaticOperatorEqual_ValidArgs_ReturnsExpected(Enum otherMeasureUnit, int? exchangerate, bool expected)
//{
//    // Arrange
//    Measurement result = _factory.GetMeasurement(SampleParams.MediumValueSampleMeasureUnit) as Measurement;
//    IMeasurement nullMeasurement = _factory.GetMeasurement(otherMeasureUnit, exchangerate);

//    // Act
//    var result = result == nullMeasurement;

//    // Assert
//    Assert.AreEqual(expected, result);

//    // Restore
//    TestSupport.RemoveIfNonDefaultMeasureUnit(otherMeasureUnit);
//}
//#endregion

//#region Static operator NotEqual
////[TestMethod, TestCategory("UnitTest")]
////public void TM45_StaticOperatorNotEqual_NullLeftArg_ReturnsFalse()
////{
////    // Arrange

////    Measurement result = _factory.GetMeasurement(SampleParams.MediumValueSampleMeasureUnit) as Measurement;
////    // Act

////    var result = result != null;
////    // Assert

////    Assert.IsTrue(result);
////}

////[TestMethod, TestCategory("UnitTest")]
////public void TM46_StaticOperatorNotEqual_NullRightArg_ReturnsFalse()
////{
////    // Arrange

////    Measurement result = _factory.GetMeasurement(SampleParams.MediumValueSampleMeasureUnit) as Measurement;
////    // Act

////    var result = null != result;
////    // Assert

////    Assert.IsTrue(result);
////}

//[DataTestMethod, TestCategory("UnitTest")]
//[DataRow(default(AreaUnit), 1, true)]
//[DataRow(default(Currency), 1, true)]
//[DataRow(default(Pieces), 1, true)]
//[DataRow(default(DistanceUnit), null, true)]
//[DataRow(default(ExtentUnit), null, true)]
//[DataRow(default(TimeUnit), null, true)]
//[DataRow(default(VolumeUnit), null, true)]
//[DataRow(default(WeightUnit), null, true)]
//[DataRow(WeightUnit.kg, null, false)]
//[DataRow((WeightUnit)2, null, true)]
//public void TM47_StaticOperatorNotEqual_ValidArgs_ReturnsExpected(Enum otherMeasureUnit, int? exchangerate, bool expected)
//{
//    // Arrange
//    Measurement result = _factory.GetMeasurement(SampleParams.MediumValueSampleMeasureUnit) as Measurement;
//    IMeasurement nullMeasurement = _factory.GetMeasurement(otherMeasureUnit, exchangerate);

//    // Act
//    var result = result != nullMeasurement;

//    // Assert
//    Assert.AreEqual(expected, result);

//    // Restore
//    TestSupport.RemoveIfNonDefaultMeasureUnit(otherMeasureUnit);
//}
//#endregion

//#region Static operator GreaterThan
////[TestMethod, TestCategory("UnitTest")]
////public void TM48_StaticOperatorGreaterThan_NullLeftArg_ReturnsExpected()
////{
////    // Arrange
////    Measurement result = _factory.GetMeasurement(SampleParams.MediumValueSampleMeasureUnit) as Measurement;

////    // Act
////    // Assert
////    Assert.IsFalse(null > result);
////}

////[TestMethod, TestCategory("UnitTest")]
////public void TM49_StaticOperatorGreaterThan_NullRightArg_ReturnsExpected()
////{
////    // Arrange
////    Measurement result = _factory.GetMeasurement(SampleParams.MediumValueSampleMeasureUnit) as Measurement;

////    // Act
////    // Assert
////    Assert.IsTrue(result > null);
////}

//[TestMethod, TestCategory("UnitTest")]
//public void TM50_StaticOperatorGreaterThan_DifferentTypeMeasurementArgs_ThrowsArgumentOutOfRangeException()
//{
//    // Arrange
//    Measurement result = _factory.GetMeasurement(SampleParams.MediumValueSampleMeasureUnit) as Measurement;
//    IMeasurement differentTypeMeasurement = _factory.GetMeasurement(SampleParams.DifferentTypeSampleMeasureUnit);

//    // Act
//    // Assert
//    var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => result > differentTypeMeasurement);
//    Assert.AreEqual(ParamNames.nullMeasurement, ex.ParamName);
//}

//[DataTestMethod, TestCategory("UnitTest")]
//[DataRow(WeightUnit.kg, WeightUnit.g, true)]
//[DataRow(WeightUnit.kg, WeightUnit.kg, false)]
//[DataRow(WeightUnit.kg, WeightUnit.ton, false)]
//public void TM51_StaticOperatorGreaterThan_ValidArgs_ReturnsExpected(Enum nullMeasureUnit, Enum otherMeasureUnit, bool expected)
//{
//    Measurement result = _factory.GetMeasurement(nullMeasureUnit) as Measurement;
//    IMeasurement otherMeasurement = _factory.GetMeasurement(otherMeasureUnit);

//    // Act
//    var result = result > otherMeasurement;

//    // Assert
//    Assert.AreEqual(expected, result);
//}
//#endregion

//#region Static operator SmallerThan
////[TestMethod, TestCategory("UnitTest")]
////public void TM52_StaticOperatorSmallerThan_NullLeftArg_ReturnsExpected()
////{
////    // Arrange
////    Measurement result = _factory.GetMeasurement(SampleParams.MediumValueSampleMeasureUnit) as Measurement;

////    // Act
////    // Assert
////    Assert.IsTrue(null < result);
////}

////[TestMethod, TestCategory("UnitTest")]
////public void TM53_StaticOperator_SmallerThan_NullRightArg_ReturnsExpected()
////{
////    // Arrange
////    Measurement result = _factory.GetMeasurement(SampleParams.MediumValueSampleMeasureUnit) as Measurement;

////    // Act
////    // Assert
////    Assert.IsFalse(result < null);
////}

//[TestMethod, TestCategory("UnitTest")]
//public void TM54_StaticOperatorSmallerThan_DifferentTypeMeasurementArgs_ThrowsArgumentOutOfRangeException()
//{
//    // Arrange
//    Measurement result = _factory.GetMeasurement(SampleParams.MediumValueSampleMeasureUnit) as Measurement;
//    IMeasurement differentTypeMeasurement = _factory.GetMeasurement(SampleParams.DifferentTypeSampleMeasureUnit);

//    // Act
//    // Assert
//    var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => result < differentTypeMeasurement);
//    Assert.AreEqual(ParamNames.nullMeasurement, ex.ParamName);
//}

//[DataTestMethod, TestCategory("UnitTest")]
//[DataRow(WeightUnit.kg, WeightUnit.g, false)]
//[DataRow(WeightUnit.kg, WeightUnit.kg, false)]
//[DataRow(WeightUnit.kg, WeightUnit.ton, true)]
//public void TM55_StaticOperatorSmallerThan_ValidArgs_ReturnsExpected(Enum nullMeasureUnit, Enum otherMeasureUnit, bool expected)
//{
//    // Arrange
//    Measurement result = _factory.GetMeasurement(nullMeasureUnit) as Measurement;
//    IMeasurement otherMeasurement = _factory.GetMeasurement(otherMeasureUnit);

//    // Act
//    var result = result < otherMeasurement;

//    // Assert
//    Assert.AreEqual(expected, result);
//}
//#endregion

//#region Static operator GreaterThanOrEqual
////[TestMethod, TestCategory("UnitTest")]
////public void TM56_StaticOperatorGreaterThanOrEqual_NullLeftArg_ReturnsExpected()
////{
////    // Arrange
////    Measurement result = _factory.GetMeasurement(SampleParams.MediumValueSampleMeasureUnit) as Measurement;

////    // Act
////    // Assert
////    Assert.IsFalse(null >= result);
////}

////[TestMethod, TestCategory("UnitTest")]
////public void TM57_StaticOperatorGreaterThanOrEqual_NullRightArg_ReturnsExpected()
////{
////    // Arrange
////    Measurement result = _factory.GetMeasurement(SampleParams.MediumValueSampleMeasureUnit) as Measurement;

////    // Act
////    // Assert
////    Assert.IsTrue(result >= null);
////}

////[TestMethod, TestCategory("UnitTest")]
////public void TM58_StaticOperatorGreaterThanOrEqual_DifferentTypeMeasurementArgs_ThrowsArgumentOutOfRangeException()
////{
////    // Arrange
////    Measurement result = _factory.GetMeasurement(SampleParams.MediumValueSampleMeasureUnit) as Measurement;
////    IMeasurement differentTypeMeasurement = _factory.GetMeasurement(SampleParams.DifferentTypeSampleMeasureUnit);

////    // Act
////    // Assert
////    var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => result >= differentTypeMeasurement);
////    Assert.AreEqual(ParamNames.nullMeasurement, ex.ParamName);
////}

////[DataTestMethod, TestCategory("UnitTest")]
////[DataRow(WeightUnit.kg, WeightUnit.g, true)]
////[DataRow(WeightUnit.kg, WeightUnit.kg, true)]
////[DataRow(WeightUnit.kg, WeightUnit.ton, false)]
////public void TM59_StaticOperatorGreaterThanOrEqual_ValidArgs_ReturnsExpected(Enum nullMeasureUnit, Enum otherMeasureUnit, bool expected)
////{
////    // Arrange
////    Measurement result = _factory.GetMeasurement(nullMeasureUnit) as Measurement;
////    IMeasurement otherMeasurement = _measurement.GetMeasurement(otherMeasureUnit);

////    // Act
////    var result = result >= otherMeasurement;

////    // Assert
////    Assert.AreEqual(expected, result);
////}
//#endregion

//#region Static operator SmallerThanOrEqual
////[TestMethod, TestCategory("UnitTest")]
////public void TM60_StaticOperatorSmallerThanOrEqual_NullLeftArg_ReturnsExpected()
////{
////    // Arrange
////    Measurement result = _factory.GetMeasurement(SampleParams.MediumValueSampleMeasureUnit) as Measurement;

////    // Act
////    // Assert
////    Assert.IsFalse(result <= null);
////}

////[TestMethod, TestCategory("UnitTest")]
////public void TM61_StaticOperatorSmallerThanOrEqual_NullRightArg_ReturnsExpected()
////{
////    // Arrange
////    Measurement result = _factory.GetMeasurement(SampleParams.MediumValueSampleMeasureUnit) as Measurement;

////    // Act
////    // Assert
////    Assert.IsTrue(null <= result);
////}

//[TestMethod, TestCategory("UnitTest")]
//public void TM62_StaticOperatorSmallerThanOrEqual_DifferentTypeMeasurementArgs_ThrowsArgumentOutOfRangeException()
//{
//    // Arrange
//    Measurement result = _factory.GetMeasurement(SampleParams.MediumValueSampleMeasureUnit) as Measurement;
//    IMeasurement differentTypeMeasurement = _factory.GetMeasurement(SampleParams.DifferentTypeSampleMeasureUnit);

//    // Act
//    // Assert
//    var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => result <= differentTypeMeasurement);
//    Assert.AreEqual(ParamNames.nullMeasurement, ex.ParamName);
//}

//[DataTestMethod, TestCategory("UnitTest")]
//[DataRow(WeightUnit.kg, WeightUnit.g, false)]
//[DataRow(WeightUnit.kg, WeightUnit.kg, true)]
//[DataRow(WeightUnit.kg, WeightUnit.ton, true)]
//public void TM63_StaticOperatorSmallerOrEqual_ValidArgs_ReturnsExpected(Enum nullMeasureUnit, Enum otherMeasureUnit, bool expected)
//{
//    // Arrange
//    Measurement result = _factory.GetMeasurement(nullMeasureUnit) as Measurement;
//    IMeasurement otherMeasurement = _measurement.GetMeasurement(otherMeasureUnit);

//    // Act
//    var result = result <= otherMeasurement;

//    // Assert
//    Assert.AreEqual(expected, result);
//}
//#endregion

//#region Static operator Divide
////[TestMethod, TestCategory("UnitTest")]
////public void TM64_StaticOperatorDivide_NullLeftArg_ReturnsExpected()
////{
////    // Arrange
////    Measurement result = _factory.GetMeasurement(SampleParams.MediumValueSampleMeasureUnit) as Measurement;
////    decimal expected = SampleParams.DecimalZero;

////    // Act
////    var result = null / result;

////    // Assert
////    Assert.AreEqual(expected, result);
////}

////[TestMethod, TestCategory("UnitTest")]
////public void TM65_StaticOperatorDivide_NullRightArg_ThrowsArgumentNullExceptions()
////{
////    // Arrange
////    Measurement result = _factory.GetMeasurement(SampleParams.MediumValueSampleMeasureUnit) as Measurement;

////    // Act
////    // Assert
////    var ex = Assert.ThrowsException<ArgumentNullException>(() => result / null);
////    Assert.AreEqual(ParamNames.nullMeasurement, ex.ParamName);
////}

//[TestMethod, TestCategory("UnitTest")]
//public void TM66_StaticOperatorDivide_DifferentTypeArgs_ThrowsArgumentOutOfRangeExceptions()
//{
//    // Arrange
//    Measurement result = _factory.GetMeasurement(SampleParams.MediumValueSampleMeasureUnit) as Measurement;
//    IMeasurement otherMeasurement = _factory.GetMeasurement(SampleParams.DifferentTypeSampleMeasureUnit);

//    // Act
//    // Assert
//    var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => result / otherMeasurement);
//    Assert.AreEqual(ParamNames.nullMeasurement, ex.ParamName);
//}

////[DataTestMethod, TestCategory("UnitTest")]
////[DataRow(WeightUnit.kg, WeightUnit.g, 1000.0)]
////[DataRow(WeightUnit.kg, WeightUnit.kg, 1.0)]
////[DataRow(WeightUnit.kg, WeightUnit.ton, 0.001)]
////[TestMethod, TestCategory("UnitTest")]
////public void TM67_StaticOperatorDivide_ValidArgs_ReturnsExpectedValue(Enum nullMeasureUnit, Enum otherMeasureUnit, double nullExchangeRate)
////{
////    // Arrange
////    Measurement result = _factory.GetMeasurement(nullMeasureUnit) as Measurement;
////    Measurement otherMeasurement = _factory.GetMeasurement(otherMeasureUnit) as Measurement;
////    decimal expected = (decimal)nullExchangeRate;

////    // Act
////    var result = result / otherMeasurement;

////    // Assert
////    Assert.AreEqual(expected, result);
////}
#endregion    
