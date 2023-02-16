using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;

public interface IBaseFace
{
    IPlaneShape GetBaseFace(params IExtent[] shapeExtents);
    IPlaneShape GetBaseFace(ExtentUnit extentUnit);
    IPlaneShape GetBaseFace(IEnumerable<IExtent> shapeExtentList);
}