using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Factories.Shape;

public interface ICuboidFactory
{
    ICuboid GetCuboid(IExtent length, IExtent width, IExtent height);
    ICuboid GetCuboid(IRectangle baseFace, IExtent height);
}
