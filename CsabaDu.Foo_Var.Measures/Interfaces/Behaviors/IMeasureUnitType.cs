namespace CsabaDu.Foo_Var.Measures.Interfaces.Behaviors;

public interface IMeasureUnitType
{
    object MeasureUnit { get; init; }

    Type GetMeasureUnitType();

    bool HasSameMeasureUnitType(Enum measureUnit);

    void ValidateMeasureUnitType(Type type);
}
