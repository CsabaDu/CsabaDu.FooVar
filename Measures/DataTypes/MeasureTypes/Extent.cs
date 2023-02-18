using CsabaDu.FooVar.Measures.Factories;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes.MeasureTypes;

namespace CsabaDu.FooVar.Measures.DataTypes.MeasureTypes;

internal sealed class Extent : Measure, IExtent
{
    public Extent(ValueType quantity, ExtentUnit extentUnit) : base(new MeasureFactory(), quantity, extentUnit)
    {
        Quantity = quantity.ToQuantity(typeof(double))!;
    }

    public Extent(ValueType quantity, IMeasurement measurement) : base(new MeasureFactory(), quantity, measurement)
    {
        measurement.ValidateMeasureUnitType(typeof(ExtentUnit));

        Quantity = quantity.ToQuantity(typeof(double))!;
    }

    public Extent(IBaseMeasure other) : base(new MeasureFactory(), other)
    {
        other.ValidateMeasureUnitType(typeof(ExtentUnit));

        Quantity = other.GetQuantity().ToQuantity(typeof(double))!;
    }

    public IExtent GetExtent(double quantity, ExtentUnit extentUnit)
    {
        return new Extent(quantity, extentUnit);
    }

    public IExtent GetExtent(IBaseMeasure? other = null)
    {
        return other == null ? this : new Extent(other);
    }

    public override IMeasure GetMeasure(IBaseMeasure? other = null) => GetExtent(other);
}
