using CsabaDu.FooVar.Measures.Factories;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes;
using CsabaDu.FooVar.Measures.Interfaces.Factories;
using CsabaDu.FooVar.Tests.Fakes.Measures;

namespace CsabaDu.FooVar.Tests.UnitTests.Measures.DataTypes;

#nullable disable
[TestClass]
public class MeasurableTests
{
    #region Private fields

    private IMeasurable _measurableChild;
    private IMeasurementFactory _factory;

    #endregion

    #region TestInitialize

    [TestInitialize]
    public void IntitializeMeasurableTests()
    {
        TestSupport.RestoreDefaultMeasureUnits();

        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();

        _factory = new MeasurementFactory();
        _measurableChild = new MeasurableChild(_factory, measureUnit);
    }

    #endregion

    #region Constructor

    #region Measurable(IMeasurementFactory measurementFactory, Enum measureUnit)
    #region MeasurementFactory validation

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_TwoNullArgs_ThrowsArgumentNullException()
    {
        // Arrange
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(() => new MeasurableChild(null, null));
        Assert.AreEqual(ParamNames.measurementFactory, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NullMeasurementFactoryArg_ThrowsArgumentNullException()
    {
        // Arrange
        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();

        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(() => new MeasurableChild(null, measureUnit));
        Assert.AreEqual(ParamNames.measurementFactory, ex.ParamName);
    }

    #endregion

    #region MeasureUnit validation

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NullMeasureUnitArg_ThrowsArgumentNullException()
    {
        // Arrange
        Enum measureUnit = null;

        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(() => new MeasurableChild(_factory, measureUnit));
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NotMeasureUnitTypeEnumArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new MeasurableChild(_factory, SampleParams.NotMeasureUnitTypeEnum));
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NotDefinedMeasureUnitArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new MeasurableChild(_factory, SampleParams.NotDefinedSampleMeasureUnit));
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
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
    [DataRow((Currency)2)]
    public void Ctor_ValidMeasureUnitArg_CreatesInstance(Enum expected)
    {
        // Arrange
        // Act
        var measurableChild = new MeasurableChild(_factory, expected);

        // Assert
        Assert.IsNotNull(measurableChild);
        Assert.AreEqual(expected, measurableChild.MeasureUnit);
    }

    #endregion
    #endregion

    #region Measurable(IMeasurement measurement)
    #region IMeasurement validation

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NullArg_ThrowsArgumentNullException()
    {
        // Arrange
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(() => new MeasurableChild(null));
        Assert.AreEqual(ParamNames.measurement, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NullMeasurementArg_ThrowsArgumentNullException()
    {
        // Arrange
        IMeasurement measurement = null;

        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(() => new MeasurableChild(measurement));
        Assert.AreEqual(ParamNames.measurement, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_MeasurementArg_CreatesInstance()
    {
        // Arrange
        Enum expectedMeasureUnit = RandomParams.GetRandomDefaultMeasureUnit();
        IMeasurement measurement = _factory.GetMeasurement(expectedMeasureUnit);

        // Act
        var actual = new MeasurableChild(measurement);

        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(expectedMeasureUnit, actual.MeasureUnit);
    }

    #endregion
    #endregion

    #endregion

    #region GetMeasurable

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurable_NotMeasureUnitTypeEnumArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => _measurableChild.GetMeasurable(SampleParams.NotMeasureUnitTypeEnum));
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurable_NotDefinedMeasureUnitArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => _measurableChild.GetMeasurable(SampleParams.NotDefinedSampleMeasureUnit));
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurable_ValidArg_ReturnsExpected()
    {
        // Arrange
        var expectedMeasureUnit = RandomParams.GetRandomDefaultMeasureUnit();

        // Act
        var actual = _measurableChild.GetMeasurable(expectedMeasureUnit);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IMeasurable));
        Assert.AreEqual(expectedMeasureUnit, actual.MeasureUnit);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasurable_NullArg_ReturnsExpected()
    {
        // Arrange
        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();
        IMeasurable expected = new MeasurableChild(_factory, measureUnit);

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
        IMeasurable expected = new MeasurableChild(_factory, measureUnit);

        // Act
        var actual = expected.GetMeasurable();

        // Assert
        Assert.AreEqual(expected, actual);
    }

    #endregion

    #region GetMeasureUnitType

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasureUnitType_ReturnsExpected()
    {
        // Arrange
        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();
        IMeasurable measurable = _measurableChild.GetMeasurable(measureUnit);
        Type expected = measureUnit.GetType();

        // Act
        var actual = measurable.GetMeasureUnitType();

        // Assert
        Assert.AreEqual(expected, actual);
    }

    #endregion

    #region HasSameMeasureUnitType

    [DataTestMethod, TestCategory("UnitTest")]
    [DataRow(null, false)]
    [DataRow(default(LimitType), false)]
    [DataRow(default(AreaUnit), false)]
    [DataRow(default(Currency), false)]
    [DataRow(default(Pieces), false)]
    [DataRow(default(DistanceUnit), false)]
    [DataRow(default(ExtentUnit), false)]
    [DataRow(default(TimeUnit), true)]
    [DataRow(default(VolumeUnit), false)]
    [DataRow(default(WeightUnit), false)]
    public void HasSameMeasureUnitType_AnyArg_ReturnsExpected(Enum measureUnit, bool expected)
    {
        // Arrange
        IMeasurable measurable = _measurableChild.GetMeasurable(TimeUnit.hour);

        // Act
        var actual = measurable.HasSameMeasureUnitType(measureUnit!);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    #endregion

    #region ValidateMeasureUnitType

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnitType_NullArg_ThrowsArgumentNullException()
    {
        // Arrange
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(
            () => _measurableChild.ValidateMeasureUnitType(null));
        Assert.AreEqual(ParamNames.type, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnitType_DifferentMeasureUnitTypeArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        IMeasurable measurable = new MeasurableChild(_factory, SampleParams.DifferentTypeSampleMeasureUnit);

        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => measurable.ValidateMeasureUnitType(SampleParams.SampleMeasureUnitType));
        Assert.AreEqual(ParamNames.type, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnitType_ValidArg_VoidPasses()
    {
        // Arrange
        IMeasurable measurable = new MeasurableChild(_factory, SampleParams.DefaultSampleMeasureUnit);
        bool hasCompleted = false;

        try
        {
            // Act
            measurable.ValidateMeasureUnitType(SampleParams.SampleMeasureUnitType);
            hasCompleted = true;
        }
        finally
        {
            // Assert
            Assert.IsTrue(hasCompleted);
        }
    }

    #endregion
}
#nullable enable
