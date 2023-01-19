namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;

public interface IShapeTraits : IShapeType
{
    ShapeTrait ShapeTraits { get; init; }

    ShapeTrait GetShapeTraits(Type shapeType);

    void ValidateShapeTraits(ShapeTrait shapeTraits);
}