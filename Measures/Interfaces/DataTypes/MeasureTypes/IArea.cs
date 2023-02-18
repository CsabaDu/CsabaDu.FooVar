namespace CsabaDu.FooVar.Measures.Interfaces.DataTypes.MeasureTypes;

public interface IArea : IMeasure
{
    IArea GetArea(double quantity, AreaUnit areaUnit);

    IArea GetArea(IBaseMeasure? other = null);
}
