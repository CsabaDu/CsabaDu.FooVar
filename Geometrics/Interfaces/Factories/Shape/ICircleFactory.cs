using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Factories.Shape;

public interface ICircleFactory
{
    ICircle GetCircle(IExtent radius);
}
