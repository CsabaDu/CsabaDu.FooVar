namespace CsabaDu.FooVar.Tests.UnitTests.Measures.Statics;

#nullable disable
[TestClass]
public class ExchangeTests
{
    static ExchangeTests()
    {
        RestoreDefaultMeasureUnits();
    }

    #region TestInitialize
    [TestInitialize]
    public void InitializeExchangeTests()
    {
        //RestoreDefaultMeasureUnits();
    }
    #endregion

    #region ShouldHaveAdHocExchangeRate
    [DataTestMethod, TestCategory("UnitTest")]
    [DataRow(default(LimitType), false)]
    [DataRow(default(Currency), false)]
    [DataRow(default(DistanceUnit), false)]
    [DataRow((DistanceUnit)1, false)]
    [DataRow((WeightUnit)3, false)]
    [DataRow((Pieces)1, true)]
    [DataRow((Currency)2, true)]
    public void ShouldHaveAdHocExchangeRate_ReturnsExpected(Enum measureUnit, bool expected)
    {
        // Arrange
        // Act
        var actual = measureUnit.ShouldHaveAdHocExchangeRate();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
}
#nullable enable
