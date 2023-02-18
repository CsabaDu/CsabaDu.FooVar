using CsabaDu.FooVar.Geometrics.Interfaces.Behaviors.Shape;

namespace CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

public interface ICircularShape : IShape, IDimensions
{
    IExtent Radius { get; init; }

    ICircularShape GetCircularShape(params IExtent[] shapeExtents);
}