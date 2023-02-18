using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Behaviors.Shape;

public interface ITangentShape
{
    IShape GetTangentShape(Side shapeSide = Side.Outer);
}