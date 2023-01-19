using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Factories.Shape;

public interface IStraightShapeFactory : IBoxFactory, IRectangleFactory
{
    IStraightShape GetSraightShape(params IExtent[] shapeExtents);
}
