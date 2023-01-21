using CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

public interface IRectangularShape : IShape, IRotate<IRectangularShape>
{
    IExtent Length { get; init; }
    IExtent Width { get; init; }

    IRectangularShape GetRectangularShape(params IExtent[] shapeExtents);
}