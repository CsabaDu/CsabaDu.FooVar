using CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

public interface ISpatialShape<T> : IGeometricBody, IProjection<T> where T : IPlaneShape
{
<<<<<<< HEAD
    T BaseFace { get; init; }
=======
    T BaseShape { get; init; }
>>>>>>> main
    IExtent Height { get; init; }
}