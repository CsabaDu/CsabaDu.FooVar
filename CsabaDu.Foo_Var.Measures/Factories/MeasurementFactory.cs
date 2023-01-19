using CsabaDu.Foo_Var.Measures.DataTypes;
using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;
using CsabaDu.Foo_Var.Measures.Interfaces.Factories;

namespace CsabaDu.Foo_Var.Measures.Factories;

public sealed class MeasurementFactory : IMeasurementFactory
{
    #region Properties
    private static IDictionary<Enum, IMeasurement> ValidMeasurements
    {
        get => ValidateMeasures.ValidMeasureUnits.ToDictionary(x => x, x => new Measurement(x) as IMeasurement);
    }
    #endregion

    #region Public methods
    public IMeasurement GetMeasurement(Enum measureUnit, decimal? exchangeRate = null)
    {
        _ = measureUnit ?? throw new ArgumentNullException(nameof(measureUnit));

        if (!measureUnit.IsValidMeasureUnit()) return new Measurement(measureUnit, exchangeRate);

        if (exchangeRate == null || exchangeRate == measureUnit.GetExchangeRate()) return ValidMeasurements[measureUnit];

        throw new ArgumentOutOfRangeException(nameof(exchangeRate), exchangeRate, null);
    }

    public IMeasurement GetMeasurement(IMeasurement measurement)
    {
        _ = measurement ?? throw new ArgumentNullException(nameof(measurement));

        Enum measureUnit = measurement.GetMeasureUnit();

        return ValidMeasurements[measureUnit];
    }
    #endregion
}
