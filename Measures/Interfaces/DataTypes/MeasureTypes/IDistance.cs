namespace CsabaDu.FooVar.Measures.Interfaces.DataTypes.MeasureTypes;

public interface IDistance : IMeasure
{
    IDistance GetDistance(double quantity, DistanceUnit distanceUnit);

    IDistance GetDistance(IBaseMeasure? other = null);
}
