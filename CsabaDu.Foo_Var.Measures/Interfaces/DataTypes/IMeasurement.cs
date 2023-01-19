using CsabaDu.Foo_Var.Measures.Interfaces.Behaviors;
using CsabaDu.Foo_Var.Measures.Interfaces.Factories;

namespace CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;

public interface IMeasurement : IMeasurable, IProportional<IMeasurement, Enum>
{
    decimal ExchangeRate { get; init; }

    Type MeasureUnitType { get; init; }

    IMeasurementFactory MeasurementFactory { get; init; }

    IMeasurement GetMeasurement(Enum measureUnit, decimal? exchangeRate = null);

    IMeasurement GetMeasurement(IMeasurement? other = null);
}
