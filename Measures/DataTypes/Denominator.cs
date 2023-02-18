using CsabaDu.FooVar.Measures.Interfaces.DataTypes;

namespace CsabaDu.FooVar.Measures.DataTypes;

internal sealed class Denominator : BaseMeasure, IDenominator
{
    internal Denominator(IBaseMeasure baseMeasure) : base(baseMeasure)
    {
        ValueType baseMeasureQuantity = baseMeasure.GetQuantity();

        Quantity = ValidateMeasures.GetValidQuantity(baseMeasureQuantity, Common.Statics.BaseMeasureType.Denominator);
    }

    internal Denominator(IMeasurement measurement, ValueType? quantity = null) : base(quantity ?? 1m, measurement)
    {
        Quantity = ValidateMeasures.GetValidQuantity(quantity, Common.Statics.BaseMeasureType.Denominator);
    }

    internal Denominator(Enum measureUnit, ValueType? quantity = null, decimal? exchangeRate = null) : base(quantity ?? 1m, measureUnit, exchangeRate)
    {
        Quantity = ValidateMeasures.GetValidQuantity(quantity, Common.Statics.BaseMeasureType.Denominator);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit, decimal? exchangeRate = null)
    {
        return GetDenominator(measureUnit, quantity, exchangeRate);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement? measurement = null)
    {
        measurement ??= Measurement;

        return GetDenominator(measurement, quantity);
    }

    public override IBaseMeasure GetBaseMeasure(IBaseMeasure? other = null)
    {
        return GetDenominator(other);
    }

    public IDenominator GetDenominator(Enum measureUnit, ValueType? quantity = null, decimal? exchangeRate = null)
    {
        return new Denominator(measureUnit, quantity, exchangeRate);
    }

    public IDenominator GetDenominator(IMeasurement measurement, ValueType? quantity = null)
    {
        return new Denominator(measurement, quantity);
    }

    public IDenominator GetDenominator(IBaseMeasure? baseMeasure = null)
    {
        return baseMeasure == null ? this : new Denominator(baseMeasure);
    }
}
