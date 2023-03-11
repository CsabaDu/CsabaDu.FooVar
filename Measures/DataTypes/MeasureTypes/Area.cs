using CsabaDu.FooVar.Measures.Factories;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes.MeasureTypes;

namespace CsabaDu.FooVar.Measures.DataTypes.MeasureTypes;

internal sealed class Area : Measure, IArea
{
    internal Area(ValueType quantity, AreaUnit areaUnit) : base(new MeasureFactory(), quantity, areaUnit)
    {
        Quantity = quantity.ToQuantity(TypeCode.Double) ?? throw new ArgumentOutOfRangeException(nameof(quantity), quantity, null);
    }

    internal Area(ValueType quantity, IMeasurement measurement) : base(new MeasureFactory(), quantity, measurement)
    {
        measurement.ValidateMeasureUnitType(typeof(AreaUnit));

        Quantity = quantity.ToQuantity(TypeCode.Double) ?? throw new ArgumentOutOfRangeException(nameof(quantity), quantity, null);
    }

    internal Area(IBaseMeasure other) : base(new MeasureFactory(), other)
    {
        other.ValidateMeasureUnitType(typeof(AreaUnit));
        ValueType quantity = other.GetQuantity();

        Quantity = quantity.ToQuantity(TypeCode.Double) ?? throw new ArgumentOutOfRangeException(nameof(other), quantity, null);
    }

    public IArea GetArea(double quantity, AreaUnit areaUnit)
    {
        return new Area(quantity, areaUnit);
    }

    public IArea GetArea(IBaseMeasure? other = null)
    {
        return other == null ? this : new Area(other);
    }

    public override IMeasure GetMeasure(IBaseMeasure? other = null) => GetArea(other);
}
