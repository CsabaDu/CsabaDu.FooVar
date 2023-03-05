using CsabaDu.FooVar.Measures.DataTypes;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes;

namespace CsabaDu.FooVar.Tests.UnitTests.Measures.DataTypes;

#nullable disable

[TestClass]
public class LimitTests
{
    #region Private fields
    private ILimit _limit;
    #endregion

    [TestInitialize]
    public void InitializeLimitTests()
    {
        TestSupport.RestoreDefaultMeasureUnits();

        _limit = new Limit(SampleParams.MediumValueSampleMeasureUnit);
    }

    //[TestMethod, TestCategory("UnitTest")]
    //public void TM01_Ctor_NullMeasureUnitArg_ThrowsArgumentNullException()
    //{
    //    // Arrange
    //    // Act
    //    // Assert
    //    var ex = Assert.ThrowsException<ArgumentNullException>(() => new Limit(null));
    //    Assert.AreEqual(Names.MeasureUnit, ex.ParamName);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void TM02_Ctor_InvalidMeasureUnitArg_ThrowsArgumentOutOfRangeException()
    //{
    //    // Arrange
    //    // Act
    //    // Assert
    //    var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Limit(SampleParams.NotMeasureUnitTypeEnum));
    //    Assert.AreEqual(ParamNames.MeasureUnit, ex.ParamName);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void TM03_Ctor_OneArg_CreatesExpectedInstance()
    //{
    //    // Arrange
    //    var instance = new Limit(SampleParams.MediumValueSampleMeasureUnit);

    //    // Act
    //    // Assert
    //    Assert.IsNotNull(instance);
    //    Assert.AreEqual(instance.MeasureUnit, SampleParams.MediumValueSampleMeasureUnit);
    //    Assert.AreEqual(instance.GetQuantity(), default(uint));
    //    Assert.AreEqual(instance.LimitType, default(LimitType));
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void TM04_Ctor_TwoArgs_CreatesExpectedInstance()
    //{
    //    // Arrange
    //    var instance = new Limit(SampleParams.MediumValueSampleMeasureUnit, (uint)SampleParams.PositiveQuantity, null);

    //    // Act
    //    // Assert
    //    Assert.IsNotNull(instance);
    //    Assert.AreEqual(instance.MeasureUnit, SampleParams.MediumValueSampleMeasureUnit);
    //    Assert.AreEqual(instance.GetQuantity(), SampleParams.PositiveQuantity);
    //    Assert.AreEqual(instance.LimitType, default(LimitType));
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void TM05_Ctor_ThreeArgs_CreatesExpectedInstance()
    //{
    //    // Arrange
    //    var instance = new Limit(SampleParams.MediumValueSampleMeasureUnit, (uint)SampleParams.PositiveQuantity, null, SampleParams.NonDefaultLimitType);

    //    // Act
    //    // Assert
    //    Assert.IsNotNull(instance);
    //    Assert.AreEqual(instance.MeasureUnit, SampleParams.MediumValueSampleMeasureUnit);
    //    Assert.AreEqual(instance.GetQuantity(), SampleParams.PositiveQuantity);
    //    Assert.AreEqual(instance.LimitType, SampleParams.NonDefaultLimitType);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void TM06_GetBaseMeasure_NullArgs_ThrowsArgumentNullException()
    //{
    //    // Arrange
    //    // Act
    //    // Assert
    //    //var ex1 = Assert.ThrowsException<ArgumentNullException>(() => _limit.GetBaseMeasure(null, null));
    //    //Assert.AreEqual(Names.MeasureUnit, ex1.ParamName);

    //    var ex2 = Assert.ThrowsException<ArgumentNullException>(() => _limit.GetBaseMeasure(Values.SampleMeasureUnit, null!));
    //    Assert.AreEqual(Names.Quantity, ex2.ParamName);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void TM07_GetBaseMeasure_InvalidQuantityArg_ThrowsArgumentOutOfRangeException()
    //{
    //    // Arrange
    //    var Quantity = SampleParams.NegativeQuantity;

    //    // Act
    //    // Assert
    //    var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => _limit.GetBaseMeasure(Quantity, SampleParams.MediumValueSampleMeasureUnit));
    //    Assert.AreEqual(ParamNames.Quantity, ex.ParamName);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void TM08_GetBaseMeasure_ValidArgs_CreatesExpectedInstance()
    //{
    //    // Arrange
    //    var MeasureUnit = Values.DifferentTypeSampleMeasureUnit;

    //    var Quantity = Values.PositiveQuantity;
    //    var measurement = _stubs.GetMeasurementStub(MeasureUnit);

    //    // Act
    //    var result = _limit.GetBaseMeasure(Quantity, MeasureUnit);

    //    // Assert
    //    Assert.IsInstanceOfType(result, typeof(IBaseMeasure));
    //    Assert.AreEqual(result.Measurement, measurement);
    //    Assert.AreEqual(result.Quantity, Quantity);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void TM09_Equals_NullArgs_ReturnsExpectedValue()
    //{
    //    // Arrange
    //    // Act
    //    // Assert
    //    Assert.IsTrue(_limit.Equals(null, null));
    //    Assert.IsFalse(_limit.Equals(null, _limit));
    //    Assert.IsFalse(_limit.Equals((Limit)_limit, null));
    //}

    //[DataTestMethod, TestCategory("UnitTest")]
    //[DataRow(VolumeUnit.meterCubic, default, default, false)]
    //[DataRow(WeightUnit.ton, default, default, false)]
    //[DataRow(WeightUnit.kg, (uint)2, default, false)]
    //[DataRow(WeightUnit.kg, default, LimitType.BeEqual, false)]
    //[DataRow(WeightUnit.kg, default, default, true)]
    //public void TM10_Equals_ValidArgs_ReturnsExpectedValue(Enum MeasureUnit, uint Quantity, LimitType limitType, bool expectedValue)
    //{
    //    // Arrange
    //    var otherLimitMock = _mocks.GetLimitMock(MeasureUnit)
    //        .SetupProperty(l => l.Quantity, Quantity)
    //        .SetupProperty(l => l.LimitType, limitType);

    //    // Act
    //    var result = _limit.Equals(otherLimitMock.Object, Limit);

    //    // Assert
    //    Assert.AreEqual(result, expectedValue);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void TM11_GetLimit_ReturnsExpectedRef()
    //{
    //    // Arrange
    //    // Act
    //    var result = _limit.GetLimit();

    //    // Assert
    //    Assert.AreEqual(result, _limit);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void TM12_GetLimit_ValidElementaryArgs_ReturnsExpectedInstance()
    //{
    //    // Arrange
    //    var MeasureUnit = Values.DefaultSameTypeMeasureUnit;
    //    var Quantity = (uint)Values.PositiveQuantity;
    //    var limitType = Values.NonDefaultLimitType;

    //    var controlLimitMock = _mocks.GetLimitMock(MeasureUnit)
    //        .SetupProperty(l => l.Quantity, Quantity)
    //        .SetupProperty(l => l.LimitType, limitType);

    //    // Act
    //    var result = _limit.GetLimit(MeasureUnit, Quantity, limitType);

    //    // Assert
    //    Assert.AreEqual(result, controlLimitMock.Object);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void TM13_GetLimit_NullIBaseMeasureArg_ThrowsArgumentNullException()
    //{
    //    // Arrange
    //    // Act
    //    // Assert
    //    var ex = Assert.ThrowsException<ArgumentNullException>(() => _limit.GetLimit(null!, default));
    //    Assert.AreEqual(Names.baseMeasure, ex.ParamName);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void TM14_GetLimit_ValidIBaseMeasureArg_ReturnsExpectedInstance()
    //{
    //    // Arrange
    //    var MeasureUnit = ExtentUnit.cm;
    //    var Quantity = (uint)2;
    //    var limitType = default(LimitType);

    //    var controlLimitMock = _mocks.GetLimitMock(MeasureUnit)
    //        .SetupProperty(l => l.Quantity, Quantity)
    //        .SetupProperty(l => l.LimitType, limitType);

    //    IBaseMeasure baseMeasure = new BaseMeasureChild(Quantity, MeasureUnit);

    //    // Act
    //    var result = _limit.GetLimit(baseMeasure);

    //    // Assert
    //    Assert.AreEqual(result, controlLimitMock.Object);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void TM15_GetLimit_ValidIBaseMeasureAndValidLimitTypeArgs_ReturnsExpectedInstance()
    //{
    //    // Arrange
    //    var MeasureUnit = Values.SampleMeasureUnit;
    //    var Quantity = Values.PositiveQuantity;
    //    var limitType = Values.NonDefaultLimitType;

    //    var controlLimitMock = _mocks.GetLimitMock()
    //        .SetupProperty(l => l.Quantity, Quantity)
    //        .SetupProperty(l => l.LimitType, limitType);

    //    var baseMeasure = _limit.GetBaseMeasure(Quantity, MeasureUnit);

    //    // Act
    //    var result = _limit.GetLimit(baseMeasure, limitType);

    //    // Assert
    //    Assert.AreEqual(result, controlLimitMock.Object);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void TM16_GetLimit_InvalidIBaseMeasureQuantityProperty_ThrowsArgumentOutOfRangeException()
    //{
    //    // Arrange
    //    var Quantity = Values.NegativeQuantity;
    //    IBaseMeasure baseMeasure = new BaseMeasureChild(Quantity, Values.SampleMeasureUnit);

    //    // Act
    //    // Assert
    //    var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => _limit.GetLimit(baseMeasure, default));
    //    Assert.AreEqual(Names.baseMeasure, ex.ParamName);
    //    Assert.AreEqual(Quantity, ex.ActualValue);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void TM17_GetLimit_NullArg_ThrowsArgumentNullException()
    //{
    //    // Arrange
    //    // Act
    //    // Assert
    //    var ex = Assert.ThrowsException<ArgumentNullException>(() => _limit.GetLimit(null!));
    //    Assert.AreEqual(Names.other, ex.ParamName);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void TM18_GetLimit_ValidILimitArg_ReturnsExpectedInstance()
    //{
    //    // Arrange
    //    var controlLimitMock = _mocks.GetLimitMock()
    //        .SetupProperty(l => l.Quantity, Values.PositiveQuantity)
    //        .SetupProperty(l => l.LimitType, Values.NonDefaultLimitType);

    //    ILimit paramLimit = new Limit(Values.SampleMeasureUnit, null, (uint)Values.PositiveQuantity, Values.NonDefaultLimitType);

    //    // Act
    //    var result = _limit.GetLimit(paramLimit);

    //    // Assert
    //    Assert.AreEqual(result, controlLimitMock.Object);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void TM19_Equals_NullArg_ReturnsExpectedValue()
    //{
    //    // Arrange
    //    // Act
    //    // Assert
    //    Assert.IsFalse(_limit.Equals(null));
    //}

    //[DataTestMethod, TestCategory("UnitTest")]
    //[DataRow(VolumeUnit.meterCubic, default, default, false)]
    //[DataRow(WeightUnit.ton, default, default, false)]
    //[DataRow(WeightUnit.kg, (uint)2, default, false)]
    //[DataRow(WeightUnit.kg, default, LimitType.BeEqual, false)]
    //[DataRow(WeightUnit.kg, default, default, true)]
    //public void TM20_Equals_ValidArgs_ReturnsExpectedValue(Enum MeasureUnit, uint Quantity, LimitType limitType, bool expectedValue)
    //{
    //    // Arrange
    //    var otherLimitMock = _mocks.GetLimitMock(MeasureUnit)
    //        .SetupProperty(l => l.Quantity, Quantity)
    //        .SetupProperty(l => l.LimitType, limitType);

    //    // Act
    //    var result = _limit.Equals((object?)otherLimitMock.Object);

    //    // Assert
    //    Assert.AreEqual(result, expectedValue);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void TM21_Equals_ObjectArg_ReturnsFalse()
    //{
    //    // Arrange
    //    // Act
    //    var result = _limit.Equals(new object());

    //    // Assert
    //    Assert.IsFalse(result);
    //}

    //[DataTestMethod, TestCategory("UnitTest")]
    //[DataRow(VolumeUnit.meterCubic, default, default, false)]
    //[DataRow(WeightUnit.ton, default, default, false)]
    //[DataRow(WeightUnit.kg, (uint)2, default, false)]
    //[DataRow(WeightUnit.kg, default, LimitType.BeEqual, false)]
    //[DataRow(WeightUnit.kg, default, default, true)]
    //public void TM22_Equals_ValidArgs_ReturnsExpectedValue(Enum MeasureUnit, uint Quantity, LimitType limitType, bool expectedValue)
    //{
    //    // Arrange
    //    var otherLimitMock = _mocks.GetLimitMock(MeasureUnit)
    //        .SetupProperty(l => l.Quantity, Quantity)
    //        .SetupProperty(l => l.LimitType, limitType);

    //    // Act
    //    var result = _limit.Equals(Limit, otherLimitMock.Object);

    //    // Assert
    //    Assert.AreEqual(result, expectedValue);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void TM23_GetHashCode_ReturnsExpectedValue()
    //{
    //    // Arrange
    //    var baseMeasure = new BaseMeasureChild(Values.PositiveQuantity, Values.SampleMeasureUnit);
    //    var limit = _limit.GetLimit(baseMeasure, Values.NonDefaultLimitType);
    //    var measurement = _stubs.GetSampleMeasurementStub();
    //    var controlHashCode = HashCode.Combine(measurement, Values.PositiveQuantity, Values.NonDefaultLimitType);

    //    // Act
    //    var result = limit.GetHashCode();

    //    // Assert
    //    Assert.AreEqual(result, controlHashCode);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void TM24_GetHashCode_ValidArg_ReturnsExpectedValue()
    //{
    //    // Arrange
    //    var baseMeasure = new BaseMeasureChild(Values.PositiveQuantity, Values.SampleMeasureUnit);
    //    var limit = _limit.GetLimit(baseMeasure, Values.NonDefaultLimitType);
    //    var measurement = _stubs.GetSampleMeasurementStub();
    //    var controlHashCode = HashCode.Combine(measurement, Values.PositiveQuantity, Values.NonDefaultLimitType);

    //    // Act
    //    var result = _limit.GetHashCode(limit);

    //    // Assert
    //    Assert.AreEqual(result, controlHashCode);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void TM25_StaticOperatorEqual_NullArgs_ReturnsExpectedValue()
    //{
    //    // Arrange
    //    // Act
    //    // Assert
    //    Assert.IsTrue(null == null);
    //    Assert.IsFalse(null == _limit);
    //    Assert.IsFalse(_limit == null);
    //}

    //[DataTestMethod, TestCategory("UnitTest")]
    //[DataRow(VolumeUnit.meterCubic, default, default, false)]
    //[DataRow(WeightUnit.ton, default, default, false)]
    //[DataRow(WeightUnit.kg, (uint)2, default, false)]
    //[DataRow(WeightUnit.kg, default, LimitType.BeEqual, false)]
    //[DataRow(WeightUnit.kg, default, default, true)]
    //public void TM26_StaticOperatorEqual_ValidArgs_ReturnsExpectedValue(Enum MeasureUnit, uint Quantity, LimitType limitType, bool expectedValue)
    //{
    //    // Arrange
    //    var otherLimitMock = _mocks.GetLimitMock(MeasureUnit)
    //        .SetupProperty(l => l.Quantity, Quantity)
    //        .SetupProperty(l => l.LimitType, limitType);

    //    // Act
    //    var result = (Limit)Limit == otherLimitMock.Object;

    //    // Assert
    //    Assert.AreEqual(result, expectedValue);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void TM27_StaticOperatorNotEqual_NullArgs_ReturnsExpectedValue()
    //{
    //    // Arrange
    //    // Act
    //    // Assert
    //    Assert.IsFalse(null != null);
    //    Assert.IsTrue(null != _limit);
    //    Assert.IsTrue((Limit)_limit != null!);
    //}

    //[DataTestMethod, TestCategory("UnitTest")]
    //[DataRow(VolumeUnit.meterCubic, default, default, true)]
    //[DataRow(WeightUnit.ton, default, default, true)]
    //[DataRow(WeightUnit.kg, (uint)2, default, true)]
    //[DataRow(WeightUnit.kg, default, LimitType.BeEqual, true)]
    //[DataRow(WeightUnit.kg, default, default, false)]
    //public void TM28_StaticOperatorEqual_ValidArgs_ReturnsExpectedValue(Enum MeasureUnit, uint Quantity, LimitType limitType, bool expectedValue)
    //{
    //    // Arrange
    //    var otherLimitMock = _mocks.GetLimitMock(MeasureUnit)
    //        .SetupProperty(l => l.Quantity, Quantity)
    //        .SetupProperty(l => l.LimitType, limitType);

    //    // Act
    //    var result = (Limit)Limit != otherLimitMock.Object;

    //    // Assert
    //    Assert.AreEqual(result, expectedValue);
    //}
}

#nullable enable
