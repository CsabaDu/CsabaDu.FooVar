using CsabaDu.FooVar.Measures.Interfaces.Factories;

namespace CsabaDu.FooVar.Measures.Interfaces.DataTypes;

public interface IMeasurable : IMeasureUnit, IMeasureUnitType
{
    object MeasureUnit { get; init; }
    IMeasurementFactory MeasurementFactory { get; init; }


    IMeasurable GetMeasurable(Enum? measureUnit = null);
}
