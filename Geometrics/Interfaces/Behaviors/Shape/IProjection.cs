using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Behaviors.Shape;

public interface IProjection
{
    IPlaneShape GetProjection(ShapeExtentType perpendicular);
}

public interface IProjection<out T> : IProjection where T : IPlaneShape
{
    T GetHorizontalProjection();
    IRectangle GetVerticalProjection(Comparison? comparison = null);
}
