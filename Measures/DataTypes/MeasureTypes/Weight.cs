using CsabaDu.FooVar.Measures.Factories;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes.MeasureTypes;

namespace CsabaDu.FooVar.Measures.DataTypes.MeasureTypes;

internal sealed class Weight : Measure, IWeight
{
    public Weight(ValueType quantity, WeightUnit weightUnit) : base(new MeasureFactory(), quantity, weightUnit)
    {
        Quantity = quantity.ToQuantity(typeof(double))!;
    }

    public Weight(ValueType quantity, IMeasurement measurement) : base(new MeasureFactory(), quantity, measurement)
    {
        measurement.ValidateMeasureUnitType(typeof(WeightUnit));

        Quantity = quantity.ToQuantity(typeof(double))!;
    }

    public Weight(IBaseMeasure other) : base(new MeasureFactory(), other)
    {
        other.ValidateMeasureUnitType(typeof(WeightUnit));

        Quantity = other.GetQuantity().ToQuantity(typeof(double))!;
    }

    public IWeight GetWeight(double quantity, WeightUnit weightUnit)
    {
        return new Weight(quantity, weightUnit);
    }

    public IWeight GetWeight(IBaseMeasure? other = null)
    {
        return other == null ? this : new Weight(other);
    }

    public override IMeasure GetMeasure(IBaseMeasure? other = null) => GetWeight(other);
}
