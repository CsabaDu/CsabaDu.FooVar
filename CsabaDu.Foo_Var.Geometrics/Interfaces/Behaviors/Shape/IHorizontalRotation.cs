using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;

public interface IHorizontalRotation
{
    IRectangle GetComparedFace(Comparison? comparison);
    ICuboid RotatedHorizontally();
    (ICuboid, ICuboid) RotatedHorizontallyWith(ICuboid other);
}
