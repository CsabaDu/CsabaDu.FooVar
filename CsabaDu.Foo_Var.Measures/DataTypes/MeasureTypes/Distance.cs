using CsabaDu.Foo_Var.Measures.Factories;
using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;
using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes.MeasureTypes;

namespace CsabaDu.Foo_Var.Measures.DataTypes.MeasureTypes;

internal sealed class Distance : Measure, IDistance
{
    public Distance(ValueType quantity, DistanceUnit distanceUnit) : base(new MeasureFactory(), quantity, distanceUnit)
    {
        Quantity = quantity.ToQuantity(typeof(double))!;
    }

    public Distance(ValueType quantity, IMeasurement measurement) : base(new MeasureFactory(), quantity, measurement)
    {
        measurement.ValidateMeasureUnitType(typeof(DistanceUnit));

        Quantity = quantity.ToQuantity(typeof(double))!;
    }

    public Distance(IBaseMeasure other) : base(new MeasureFactory(), other)
    {
        other.ValidateMeasureUnitType(typeof(DistanceUnit));

        Quantity = other.GetQuantity().ToQuantity(typeof(double))!;
    }

    public IDistance GetDistance(double quantity, DistanceUnit distanceUnit)
    {
        return new Distance(quantity, distanceUnit);
    }

    public IDistance GetDistance(IBaseMeasure? other = null)
    {
        return other == null ? this : new Distance(other);
    }

    public override IMeasure GetMeasure(IBaseMeasure? other = null) => GetDistance(other);
}
