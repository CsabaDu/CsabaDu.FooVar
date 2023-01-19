using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Factories.Shape;

public interface IPlaneShapeFactory : IRectangleFactory, ICircleFactory
{
    IPlaneShape GetPlaneShape(params IExtent[] shapeExtents);
    IPlaneShape GetPlaneShape(IPlaneShape planeShape);
}
