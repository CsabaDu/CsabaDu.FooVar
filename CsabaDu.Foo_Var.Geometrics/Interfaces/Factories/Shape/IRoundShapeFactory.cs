using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Factories.Shape;

public interface IRoundShapeFactory : IDrumFactory, ICircleFactory
{
    IRoundShape GetRoundShape(params IExtent[] shapeExtents);
}
