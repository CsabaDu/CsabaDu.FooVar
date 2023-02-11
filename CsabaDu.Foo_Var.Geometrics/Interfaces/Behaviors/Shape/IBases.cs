using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;

public interface IBases
{
    IPlaneShape GetBases(params IExtent[] shapeExtents);
    IPlaneShape GetBases(ExtentUnit extentUnit);
    IPlaneShape GetBases(IEnumerable<IExtent> shapeExtentList);
}