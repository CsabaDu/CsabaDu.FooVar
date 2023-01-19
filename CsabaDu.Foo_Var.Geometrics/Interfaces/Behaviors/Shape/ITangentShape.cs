using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;

public interface ITangentShape
{
    IShape GetTangentShape(Side shapeSide = Side.Outer);
}