using CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

public interface IRoundShape : IShape, IDimensions
{
    IExtent Radius { get; init; }

    IRoundShape GetRoundShape(params IExtent[] shapeExtents);
}