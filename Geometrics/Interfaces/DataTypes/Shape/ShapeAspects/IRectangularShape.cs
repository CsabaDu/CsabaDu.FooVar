using CsabaDu.FooVar.Geometrics.Interfaces.Behaviors.Shape;

namespace CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

public interface IRectangularShape : IShape, IHorizontalRotation<IRectangularShape>
{
    IExtent Length { get; init; }
    IExtent Width { get; init; }

    IRectangularShape GetRectangularShape(params IExtent[] shapeExtents);
}