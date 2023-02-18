using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Factories.Shape;

public interface IPlaneShapeFactory : IRectangleFactory, ICircleFactory
{
    IPlaneShape GetPlaneShape(params IExtent[] shapeExtents);
    IPlaneShape GetPlaneShape(IPlaneShape planeShape);
}
