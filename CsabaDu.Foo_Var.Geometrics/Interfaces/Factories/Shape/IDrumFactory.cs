using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Factories.Shape;

public interface IDrumFactory
{
    IDrum GetDrum(IExtent radius, IExtent height);
    IDrum GetDrum(ICircle baseShape, IExtent height);
}
