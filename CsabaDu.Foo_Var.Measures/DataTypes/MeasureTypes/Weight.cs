using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;
using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes.MeasureTypes;

namespace CsabaDu.Foo_Var.Measures.DataTypes.MeasureTypes;

internal sealed class Weight : Measure, IWeight
{
    public Weight(ValueType quantity, WeightUnit weightUnit) : base(quantity, weightUnit)
    {
        Quantity = quantity.ToQuantity(typeof(double))!;
    }

    public Weight(ValueType quantity, IMeasurement measurement) : base(quantity, measurement)
    {
        measurement.ValidateMeasureUnitType(typeof(WeightUnit));

        Quantity = quantity.ToQuantity(typeof(double))!;
    }

    public Weight(IBaseMeasure other) : base(other)
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
