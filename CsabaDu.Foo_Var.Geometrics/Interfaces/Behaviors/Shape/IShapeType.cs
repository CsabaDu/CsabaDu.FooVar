namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;

public interface IShapeType
{
    Type GetShapeType();
    Type GetShapeType(ShapeTrait shapeTraits);

    void ValidateShapeType(Type shapeType);
}