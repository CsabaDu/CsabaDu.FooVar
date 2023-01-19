using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;

public interface IDiagonal
{
    IExtent GetDiagonal(IShape shape, ExtentUnit extentUnit);
    IExtent GetDiagonal(ExtentUnit extentUnit = ExtentUnit.meter);
}