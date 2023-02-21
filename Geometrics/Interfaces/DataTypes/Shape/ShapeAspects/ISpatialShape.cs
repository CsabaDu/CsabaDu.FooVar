using CsabaDu.FooVar.Geometrics.Interfaces.Behaviors.Shape;

namespace CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

public interface ISpatialShape<T> : IDryBody, IProjection<T> where T : IPlaneShape
{
    T BaseFace { get; init; }
    IExtent Height { get; init; }
}