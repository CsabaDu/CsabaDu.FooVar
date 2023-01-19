using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;

public interface IProjection
{
    IPlaneShape GetProjection(ShapeExtentType shapeExtentType);
}

public interface IProjection<out T> : IProjection where T : IPlaneShape
{
    T GetHorizontalProjection();
    IRectangle GetVerticalProjection(Comparison? comparison = null);
}