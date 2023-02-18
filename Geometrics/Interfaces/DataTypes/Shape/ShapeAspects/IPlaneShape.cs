using CsabaDu.FooVar.Geometrics.Interfaces.Behaviors.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;
using CsabaDu.FooVar.Geometrics.Interfaces.Factories;

namespace CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

public interface IPlaneShape : IShape, ISurface, IEdge
{
    //ISurfaceFactory SurfaceFactory { get; init; }

    IPlaneShape GetPlaneShape(ExtentUnit extentUnit);
    IPlaneShape GetPlaneShape(params IExtent[] shapeExtents);
    IPlaneShape GetPlaneShape(IPlaneShape planeShape);
}