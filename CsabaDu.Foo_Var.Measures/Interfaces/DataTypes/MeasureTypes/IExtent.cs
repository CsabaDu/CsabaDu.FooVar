namespace CsabaDu.Foo_Var.Measures.Interfaces.DataTypes.MeasureTypes;

public interface IExtent : IMeasure
{
    IExtent GetExtent(double quantity, ExtentUnit extentUnit);

    IExtent GetExtent(IBaseMeasure? other = null);
}
