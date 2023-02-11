using CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

public interface ISpatialShape<T> : IGeometricBody, IProjection<T> where T : IPlaneShape
{
    T Bases { get; init; }
    IExtent Height { get; init; }
}