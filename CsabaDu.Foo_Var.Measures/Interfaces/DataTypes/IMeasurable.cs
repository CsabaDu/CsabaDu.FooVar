using CsabaDu.Foo_Var.Measures.Interfaces.Behaviors;

namespace CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;

public interface IMeasurable : IMeasureUnitType
{
    Enum GetMeasureUnit();

    IMeasurable GetMeasurable(Enum? measureUnit = null);
}
