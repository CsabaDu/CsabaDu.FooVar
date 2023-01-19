using CsabaDu.Foo_Var.Measures.DataTypes;
using CsabaDu.Foo_Var.Measures.Factories;
using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;
using CsabaDu.Foo_Var.Measures.Interfaces.Factories;

namespace CsabaDu.Foo_Var.Tests.UnitTests.Measures.Factories;

#nullable disable
[TestClass]
public class MeasurementFactoryTests
{

    #region Private fields
    private IMeasurementFactory _factory;
    #endregion

    #region TestInitialize
    [TestInitialize]
    public void InitializeMeasurementTests()
    {
        TestSupport.RestoreDefaultMeasureUnits();

        _factory = new MeasurementFactory();
    }
    #endregion

    #region GetMeasurement
    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurement_NullArg_ThrowsArgumentNullException()
    {
        // Arrange
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(() => _factory.GetMeasurement(null));
        Assert.AreEqual(ParamNames.measurement, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurement_NullArgs_ThrowsArgumentNullException()
    {
        // Arrange
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(() => _factory.GetMeasurement(null, null));
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurement_NullAndValidArg_ThrowsArgumentNullException()
    {
        // Arrange
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(() => _factory.GetMeasurement(null, SampleParams.DecimalOne));
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurement_NotMeasureUnitTypeEnumArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => _factory.GetMeasurement(SampleParams.NotMeasureUnitTypeEnum));
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurement_NotDefinedMeasureUnitArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => _factory.GetMeasurement(SampleParams.NotDefinedSampleMeasureUnit));
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurement_NotConstantMeasureUnitWithoutExchangeRateArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => _factory.GetMeasurement(SampleParams.MeasureUnitShouldHaveAdHocRate));
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurement_ComstantMeasureUnitWithDifferentExchangeRateArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => _factory.GetMeasurement(SampleParams.MediumValueSampleMeasureUnit, SampleParams.DecimalPositive));
        Assert.AreEqual(ParamNames.exchangeRate, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurement_NotComstantMeasureUnitWithDifferentExchangeRateArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        Enum measureUnit = Pieces.Default;
        decimal exchangeRate = SampleParams.DecimalOne;
        measureUnit.TryAddExchangeRate(exchangeRate);

        decimal differentExchangeRate = SampleParams.DecimalPositive;

        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => _factory.GetMeasurement(measureUnit, differentExchangeRate));
        Assert.AreEqual(ParamNames.exchangeRate, ex.ParamName);

        // Rearrange
        TestSupport.RemoveIfNotDefaultMeasureUnit(measureUnit);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurement_ValidNonConstantMeasureUnitArgs_ReturnsExpected()
    {
        // Arrange
        Enum expectedMeasureUnit = Currency.EUR;
        decimal expectedExchangeRate = SampleParams.DecimalPositive;

        // Act
        var actual = _factory.GetMeasurement(expectedMeasureUnit, expectedExchangeRate);

        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(expectedMeasureUnit, actual.MeasureUnit);
        Assert.AreEqual(expectedExchangeRate, actual.ExchangeRate);

        IMeasurement expected = _factory.GetMeasurement(expectedMeasureUnit);
        Assert.AreEqual(expected, actual);

        // Rearrange
        TestSupport.RemoveIfNotDefaultMeasureUnit(expectedMeasureUnit);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurement_ValidNonConstantAndNullMeasureUnitArgs_ReturnsExpected()
    {
        // Arrange
        Enum expectedMeasureUnit = Currency.EUR;
        decimal expectedExchangeRate = SampleParams.DecimalPositive;

        // Act
        var actual = _factory.GetMeasurement(expectedMeasureUnit, expectedExchangeRate);

        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(expectedMeasureUnit, actual.MeasureUnit);
        Assert.AreEqual(expectedExchangeRate, actual.ExchangeRate);

        IMeasurement expected = _factory.GetMeasurement(expectedMeasureUnit, null);
        Assert.AreEqual(expected, actual);

        // Rearrange
        TestSupport.RemoveIfNotDefaultMeasureUnit(expectedMeasureUnit);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurement_ValidNonConstantAndExchangeRateMeasureUnitArgs_ReturnsExpected()
    {
        // Arrange
        Enum expectedMeasureUnit = Currency.EUR;
        decimal expectedExchangeRate = SampleParams.DecimalPositive;

        // Act
        var actual = _factory.GetMeasurement(expectedMeasureUnit, expectedExchangeRate);

        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(expectedMeasureUnit, actual.MeasureUnit);
        Assert.AreEqual(expectedExchangeRate, actual.ExchangeRate);

        // Rearrange
        TestSupport.RemoveIfNotDefaultMeasureUnit(expectedMeasureUnit);
    }

    [DataTestMethod, TestCategory("UnitTest")]
    [DataRow(default(AreaUnit), null)]
    [DataRow(default(AreaUnit), 1)]
    [DataRow(default(Currency), null)]
    [DataRow(default(Currency), 1)]
    [DataRow(default(Pieces), null)]
    [DataRow(default(Pieces), 1)]
    [DataRow(default(DistanceUnit), null)]
    [DataRow(default(DistanceUnit), 1)]
    [DataRow(default(ExtentUnit), null)]
    [DataRow(default(TimeUnit), 1)]
    [DataRow(default(VolumeUnit), null)]
    [DataRow(default(WeightUnit), 1)]
    [DataRow(WeightUnit.kg, 1000)]
    [DataRow((WeightUnit)2, null)]
    public void GetMeasurement_ValidConstantMeasureUnitArgs_ReturnsExpected(Enum measureUnit, int? exchangeRate)
    {
        // Arrange
        decimal expectedExchangeRate = measureUnit.GetExchangeRate();
        IMeasurement expected = new Measurement(measureUnit);

        // Act
        var actual = _factory.GetMeasurement(measureUnit, exchangeRate);

        // Assert
        Assert.AreEqual(expected, actual);
        Assert.AreEqual(expectedExchangeRate, actual.ExchangeRate);
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
    [DataRow(Currency.HUF)]
    public void GetMeasurement_MeasurementArg_ReturnsExpected(Enum expectedMeasureUnit)
    {
        // Arrange
        _ = Currency.HUF.TryAddExchangeRate(1 / 409m);

        IMeasurement expected = _factory.GetMeasurement(expectedMeasureUnit);

        // Act
        var actual = _factory.GetMeasurement(expected);

        // Assert
        Assert.AreEqual(expected, actual);

        // Rearrange
        TestSupport.RemoveIfNotDefaultMeasureUnit(Currency.HUF);
    }
    #endregion
}
#nullable enable
