using CsabaDu.Foo_Var.Measures.Factories;
using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;
using CsabaDu.Foo_Var.Measures.Interfaces.Factories;
using CsabaDu.Foo_Var.Tests.Fakes.Measures;

namespace CsabaDu.Foo_Var.Tests.UnitTests.Measures.DataTypes;

#nullable disable
[TestClass]
public class MeasurableTests
{
    #region Private fields

    private IMeasurable _measurableChild;

    #endregion

    #region TestInitialize

    [TestInitialize]
    public void IntitializeMeasurableTests()
    {
        TestSupport.RestoreDefaultMeasureUnits();

        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();

        _measurableChild = new MeasurableChild(measureUnit);
    }

    #endregion

    #region Constructor

    #region MeasureUnit validation

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NullMeasureUnitArg_ThrowsArgumentNullException()
    {
        // Arrange
        Enum measureUnit = null;

        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(() => new MeasurableChild(measureUnit));
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NotMeasureUnitTypeEnumArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new MeasurableChild(SampleParams.NotMeasureUnitTypeEnum));
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NotDefinedMeasureUnitArg_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        // Act
        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new MeasurableChild(SampleParams.NotDefinedSampleMeasureUnit));
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
        var measurableChild = new MeasurableChild(expected);

        // Assert
        Assert.IsNotNull(measurableChild);
        Assert.AreEqual(expected, measurableChild.MeasureUnit);
    }

    #endregion

    #region IMeasurement validation

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
        IMeasurementFactory factory = new MeasurementFactory();
        IMeasurement measurement = factory.GetMeasurement(expectedMeasureUnit);

        // Act
        var actual = new MeasurableChild(measurement);

        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(expectedMeasureUnit, actual.MeasureUnit);
    }

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
        IMeasurable expected = new MeasurableChild(measureUnit);

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
        IMeasurable expected = new MeasurableChild(measureUnit);

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
        IMeasurable measurable = new MeasurableChild(SampleParams.DifferentTypeSampleMeasureUnit);

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
        IMeasurable measurable = new MeasurableChild(SampleParams.DefaultSampleMeasureUnit);
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
