using CsabaDu.FooVar.Measures.Factories;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes.MeasureTypes;

namespace CsabaDu.FooVar.Measures.DataTypes.MeasureTypes;

internal sealed class Area : Measure, IArea
{
    public Area(ValueType quantity, AreaUnit areaUnit) : base(new MeasureFactory(), quantity, areaUnit)
    {
        Quantity = quantity.ToQuantity(typeof(double))!;
    }

    public Area(ValueType quantity, IMeasurement measurement) : base(new MeasureFactory(), quantity, measurement)
    {
        measurement.ValidateMeasureUnitType(typeof(AreaUnit));

        Quantity = quantity.ToQuantity(typeof(double))!;
    }

    public Area(IBaseMeasure other) : base(new MeasureFactory(), other)
    {
        other.ValidateMeasureUnitType(typeof(AreaUnit));

        Quantity = other.GetQuantity().ToQuantity(typeof(double))!;
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
