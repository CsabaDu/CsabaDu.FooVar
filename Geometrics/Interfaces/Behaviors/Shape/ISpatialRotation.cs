using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Behaviors.Shape;

public interface ISpatialRotation
{
    ICuboid RotatedSpatially();
    (ICuboid, ICuboid) RotatedSpatiallyWith(ICuboid other);
    IRectangle GetComparedCuboidFace(Comparison? comparison);
}
