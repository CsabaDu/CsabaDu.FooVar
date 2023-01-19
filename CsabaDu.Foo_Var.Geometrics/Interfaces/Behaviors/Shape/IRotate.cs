using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;

public interface IRotate<T> where T : IShape
{
    T Rotated();
    (T, T) RotatedWith(T other);
    IExtent GetComparedShapeExtent(Comparison? comparison);
    IEnumerable<IExtent> GetSortedShapeExtentList();
}
