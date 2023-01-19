using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

public interface IRectangle : IPlaneShape, IStraightShape
{
    IRectangle GetRectangle(params IExtent[] shapeExtents);
    IRectangle GetRectangle(IExtent length, IExtent width);
    IRectangle GetRectangle(ExtentUnit extentUnit);
    IRectangle GetRectangle(IPlaneShape planeShape);
}