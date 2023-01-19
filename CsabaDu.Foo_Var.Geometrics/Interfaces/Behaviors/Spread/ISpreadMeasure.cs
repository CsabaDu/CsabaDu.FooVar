namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Spread;

public interface ISpreadMeasure<T, U> where T : IMeasure where U : struct, Enum
{
    T GetSpreadMeasure(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits, U? spreadMeasureUnit = null);
    T GetSpreadMeasure(U? spreadMeasureUnit = null);
    void ValidateSpreadMeasure(T spreadMeasure);
}