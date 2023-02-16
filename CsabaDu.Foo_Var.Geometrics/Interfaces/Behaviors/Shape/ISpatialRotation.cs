using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;

public interface ISpatialRotation
{
    ICuboid RotatedSpatially();
    (ICuboid, ICuboid) RotatedSpatiallyWith(ICuboid other);
    IRectangle GetComparedCuboidFace(Comparison? comparison);
}
