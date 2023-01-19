using CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

public interface IStraightShape : IShape, IRotate<IStraightShape>
{
    IExtent Length { get; init; }
    IExtent Width { get; init; }

    IStraightShape GetStraightShape(params IExtent[] shapeExtents);
}