using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;
using CsabaDu.Foo_Var.Measures.Interfaces.Factories;

namespace CsabaDu.Foo_Var.Measures.DataTypes;

internal abstract class Measurable : IMeasurable
{
    #region Properties
    public object MeasureUnit { get; init; }

    public IMeasurementFactory MeasurementFactory { get; init; }
    #endregion

    #region Constructors
    private protected Measurable(IMeasurementFactory measurementFactory, Enum measureUnit)
    {
        MeasurementFactory = measurementFactory ?? throw new ArgumentNullException(nameof(measurementFactory));

        _ = measureUnit ?? throw new ArgumentNullException(nameof(measureUnit));
        if (!measureUnit.IsDefinedMeasureUnit()) throw new ArgumentOutOfRangeException(nameof(measureUnit));

        MeasureUnit = measureUnit;
    }

    private protected Measurable(IMeasurement measurement)
    {
        MeasurementFactory = measurement?.MeasurementFactory ?? throw new ArgumentNullException(nameof(measurement));
        MeasureUnit = measurement.MeasureUnit;
    }
    #endregion

    #region Public methods
    public Type GetMeasureUnitType() => MeasureUnit.GetType();

    public Enum GetMeasureUnit() => (Enum)MeasureUnit;

    public bool HasSameMeasureUnitType(Enum measureUnit)
    {
        return measureUnit?.GetType() == GetMeasureUnitType();
    }

    public void ValidateMeasureUnitType(Type type)
    {
        _ = type ?? throw new ArgumentNullException(nameof(type));

        if (type != GetMeasureUnitType()) throw new ArgumentOutOfRangeException(nameof(type), type.FullName, null);
    }
    #endregion

    #region Abstract methods
    public abstract IMeasurable GetMeasurable(Enum? measureUnit = null);
    #endregion
}
