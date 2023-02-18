using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

public interface ICircle : IPlaneShape, ICircularShape
{
    ICircle GetCircle(params IExtent[] shapeExtents);
    ICircle GetCircle(IExtent radius);
    ICircle GetCircle(ExtentUnit extentUnit);
    ICircle GetCircle(IPlaneShape planeShape);
}