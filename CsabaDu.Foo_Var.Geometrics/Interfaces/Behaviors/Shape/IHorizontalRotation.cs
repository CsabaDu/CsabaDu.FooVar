using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;

public interface IHorizontalRotation<T> where T : IShape
{
    T RotatedHorizontally();
    (T, T) RotatedHorizontallyWith(T other);
    IExtent GetComparedShapeExtent(Comparison? comparison);
    IEnumerable<IExtent> GetSortedShapeExtentList();
}
