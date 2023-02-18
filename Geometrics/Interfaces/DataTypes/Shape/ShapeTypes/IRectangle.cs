using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

public interface IRectangle : IPlaneShape, IRectangularShape
{
    IRectangle GetRectangle(params IExtent[] shapeExtents);
    IRectangle GetRectangle(IExtent length, IExtent width);
    IRectangle GetRectangle(ExtentUnit extentUnit);
    IRectangle GetRectangle(IPlaneShape planeShape);
}