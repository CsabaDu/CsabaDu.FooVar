using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Factories.Shape;

public interface IRectangularShapeFactory : ICuboidFactory, IRectangleFactory
{
    IRectangularShape GetSraightShape(params IExtent[] shapeExtents);
}
