namespace CsabaDu.FooVar.Geometrics.Interfaces.Behaviors.Shape;

public interface IShapeType
{
    Type GetShapeType();
    Type GetShapeType(ShapeTrait shapeTraits);

    void ValidateShapeType(Type shapeType);
}