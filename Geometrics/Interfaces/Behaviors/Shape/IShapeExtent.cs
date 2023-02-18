namespace CsabaDu.FooVar.Geometrics.Interfaces.Behaviors.Shape;

public interface IShapeExtent
{
    IExtent GetShapeExtent(ShapeExtentType shapeExtentType);

    void ValidateShapeExtent(IExtent shapeExtent);
}