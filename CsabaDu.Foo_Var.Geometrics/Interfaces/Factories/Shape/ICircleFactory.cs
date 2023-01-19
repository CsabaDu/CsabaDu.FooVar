using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Factories.Shape;

public interface ICircleFactory
{
    ICircle GetCircle(IExtent radius);
}
