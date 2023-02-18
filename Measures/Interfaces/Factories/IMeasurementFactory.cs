using CsabaDu.FooVar.Measures.Interfaces.DataTypes;

namespace CsabaDu.FooVar.Measures.Interfaces.Factories;

public interface IMeasurementFactory
{
    IMeasurement GetMeasurement(Enum measureUnit, decimal? exchangeRate = null);

    IMeasurement GetMeasurement(IMeasurement measurement);
}
