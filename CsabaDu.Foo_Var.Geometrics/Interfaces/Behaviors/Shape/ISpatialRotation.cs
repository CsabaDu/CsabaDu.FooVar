using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;

public interface ISpatialRotation
{
    IRectangle GetComparedFace(Comparison? comparison);
    ICuboid RotatedSpatially();
    (ICuboid, ICuboid) RotatedSpatiallyWith(ICuboid other);
}
