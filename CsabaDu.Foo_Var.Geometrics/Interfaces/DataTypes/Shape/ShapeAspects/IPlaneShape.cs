using CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Spread;
using CsabaDu.Foo_Var.Geometrics.Interfaces.Factories;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

public interface IPlaneShape : IShape, ISurface, IEdge
{
    //ISurfaceFactory SurfaceFactory { get; init; }

    IPlaneShape GetPlaneShape(ExtentUnit extentUnit);
    IPlaneShape GetPlaneShape(params IExtent[] shapeExtents);
    IPlaneShape GetPlaneShape(IPlaneShape planeShape);
}