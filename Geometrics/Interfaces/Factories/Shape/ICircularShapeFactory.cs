using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Factories.Shape;

public interface ICircularShapeFactory : ICylinderFactory, ICircleFactory
{
    ICircularShape GetCircularShape(params IExtent[] shapeExtents);
}
