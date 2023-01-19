using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;

namespace CsabaDu.Foo_Var.Measures.Interfaces.Factories;

public interface IMeasurementFactory
{
    IMeasurement GetMeasurement(Enum measureUnit, decimal? exchangeRate = null);

    IMeasurement GetMeasurement(IMeasurement measurement);
}
