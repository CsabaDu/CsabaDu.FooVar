using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Factories.Shape;

public interface IRectangularShapeFactory : ICuboidFactory, IRectangleFactory
{
    IRectangularShape GetRectangularShape(params IExtent[] shapeExtents);
}
