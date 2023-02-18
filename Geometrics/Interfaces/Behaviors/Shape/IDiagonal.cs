using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Behaviors.Shape;

public interface IDiagonal
{
    IExtent GetDiagonal(IShape shape, ExtentUnit extentUnit);
    IExtent GetDiagonal(ExtentUnit extentUnit = ExtentUnit.meter);
}