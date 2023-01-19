using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;

public interface IBaseShape
{
    IPlaneShape GetBaseShape(params IExtent[] shapeExtents);
    IPlaneShape GetBaseShape(ExtentUnit extentUnit);
    IPlaneShape GetBaseShape(IEnumerable<IExtent> shapeExtentList);
}