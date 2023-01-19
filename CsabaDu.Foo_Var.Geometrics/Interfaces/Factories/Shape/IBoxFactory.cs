using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Factories.Shape;

public interface IBoxFactory
{
    IBox GetBox(IExtent length, IExtent width, IExtent height);
    IBox GetBox(IRectangle baseShape, IExtent height);
}
