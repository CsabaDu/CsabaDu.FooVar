using CsabaDu.Foo_Var.Measures.Interfaces.Behaviors;
using CsabaDu.Foo_Var.Measures.Interfaces.Factories;

namespace CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;

public interface IMeasurable : IMeasureUnitType
{
    IMeasurementFactory MeasurementFactory { get; init; }

    Enum GetMeasureUnit();
    IMeasurable GetMeasurable(Enum? measureUnit = null);
}
