using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;
using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes.MeasureTypes;

namespace CsabaDu.Foo_Var.Measures.DataTypes.MeasureTypes;

internal sealed class Area : Measure, IArea
{
    public Area(ValueType quantity, AreaUnit areaUnit) : base(quantity, areaUnit)
    {
        Quantity = quantity.ToQuantity(typeof(double))!;
    }

    public Area(ValueType quantity, IMeasurement measurement) : base(quantity, measurement)
    {
        measurement.ValidateMeasureUnitType(typeof(AreaUnit));

        Quantity = quantity.ToQuantity(typeof(double))!;
    }

    public Area(IBaseMeasure other) : base(other)
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
