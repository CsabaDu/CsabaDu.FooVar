using CsabaDu.FooVar.Measures.Interfaces.Behaviors;
using CsabaDu.FooVar.Measures.Interfaces.Factories;

namespace CsabaDu.FooVar.Measures.Interfaces.DataTypes;

public interface IMeasurable : IMeasureUnitType
{
    IMeasurementFactory MeasurementFactory { get; init; }

    Enum GetMeasureUnit();
    IMeasurable GetMeasurable(Enum? measureUnit = null);
}
