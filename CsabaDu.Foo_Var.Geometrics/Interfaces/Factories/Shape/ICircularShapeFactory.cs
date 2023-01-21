using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Factories.Shape;

public interface ICircularShapeFactory : ICylinderFactory, ICircleFactory
{
    ICircularShape GetCircularShape(params IExtent[] shapeExtents);
}
