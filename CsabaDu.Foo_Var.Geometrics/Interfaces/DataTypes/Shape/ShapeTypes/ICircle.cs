using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

public interface ICircle : IPlaneShape, IRoundShape
{
    ICircle GetCircle(params IExtent[] shapeExtents);
    ICircle GetCircle(IExtent radius);
    ICircle GetCircle(ExtentUnit extentUnit);
    ICircle GetCircle(IPlaneShape planeShape);
}