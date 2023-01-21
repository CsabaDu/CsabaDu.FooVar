using CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

public interface ICircularShape : IShape, IDimensions
{
    IExtent Radius { get; init; }

    ICircularShape GetCircularShape(params IExtent[] shapeExtents);
}