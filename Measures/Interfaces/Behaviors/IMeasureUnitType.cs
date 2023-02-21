namespace CsabaDu.FooVar.Measures.Interfaces.Behaviors;

public interface IMeasureUnitType
{
    Type GetMeasureUnitType();

    bool HasSameMeasureUnitType(Enum measureUnit);

    void ValidateMeasureUnitType(Type type);
}
