using CsabaDu.FooVar.Measures.Factories;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes.MeasureTypes;

namespace CsabaDu.FooVar.Measures.DataTypes.MeasureTypes;

internal sealed class Weight : Measure, IWeight
{
    internal Weight(ValueType quantity, WeightUnit weightUnit) : base(new MeasureFactory(), quantity, weightUnit)
    {
        Quantity = quantity.ToQuantity(TypeCode.Double) ?? throw new ArgumentOutOfRangeException(nameof(quantity), quantity, null);
    }

    internal Weight(ValueType quantity, IMeasurement measurement) : base(new MeasureFactory(), quantity, measurement)
    {
        measurement.ValidateMeasureUnitType(typeof(WeightUnit));

        Quantity = quantity.ToQuantity(TypeCode.Double) ?? throw new ArgumentOutOfRangeException(nameof(quantity), quantity, null);
    }

    internal Weight(IBaseMeasure other) : base(new MeasureFactory(), other)
    {
        other.ValidateMeasureUnitType(typeof(WeightUnit));
        ValueType quantity = other.GetQuantity();

        Quantity = quantity.ToQuantity(TypeCode.Double) ?? throw new ArgumentOutOfRangeException(nameof(other), quantity, null);
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
