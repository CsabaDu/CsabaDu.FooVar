namespace CsabaDu.FooVar.Geometrics.Interfaces.Behaviors.Spread;

public interface ISpreadMeasure<T, U> where T : class, IMeasure where U : struct, Enum
{
    T GetSpreadMeasure(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits, U? spreadMeasureUnit = null);
    T GetSpreadMeasure(U spreadMeasureUnit);
    void ValidateSpreadMeasure(T spreadMeasure);
}