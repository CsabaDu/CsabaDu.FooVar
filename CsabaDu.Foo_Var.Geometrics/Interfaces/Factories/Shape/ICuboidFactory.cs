using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Factories.Shape;

public interface ICuboidFactory
{
    ICuboid GetCuboid(IExtent length, IExtent width, IExtent height);
    ICuboid GetCuboid(IRectangle baseShape, IExtent height);
}
