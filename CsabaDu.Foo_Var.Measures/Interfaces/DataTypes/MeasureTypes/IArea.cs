namespace CsabaDu.Foo_Var.Measures.Interfaces.DataTypes.MeasureTypes;

public interface IArea : IMeasure
{
    IArea GetArea(double quantity, AreaUnit areaUnit);

    IArea GetArea(IBaseMeasure? other = null);
}
