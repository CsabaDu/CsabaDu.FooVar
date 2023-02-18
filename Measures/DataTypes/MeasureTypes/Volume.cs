using CsabaDu.FooVar.Measures.Factories;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes.MeasureTypes;

namespace CsabaDu.FooVar.Measures.DataTypes.MeasureTypes;

internal sealed class Volume : Measure, IVolume
{
    public Volume(ValueType quantity, VolumeUnit volumeUnit) : base(new MeasureFactory(), quantity, volumeUnit)
    {
        Quantity = quantity.ToQuantity(typeof(double))!;
    }

    public Volume(ValueType quantity, IMeasurement measurement) : base(new MeasureFactory(), quantity, measurement)
    {
        measurement.ValidateMeasureUnitType(typeof(VolumeUnit));

        Quantity = quantity.ToQuantity(typeof(double))!;
    }

    public Volume(IBaseMeasure other) : base(new MeasureFactory(), other)
    {
        other.ValidateMeasureUnitType(typeof(VolumeUnit));

        Quantity = other.GetQuantity().ToQuantity(typeof(double))!;
    }

    public IVolume GetVolume(double quantity, VolumeUnit volumeUnit)
    {
        return new Volume(quantity, volumeUnit);
    }

    public IVolume GetVolume(IBaseMeasure? other = null)
    {
        return other == null ? this : new Volume(other);
    }

    public override IMeasure GetMeasure(IBaseMeasure? other = null) => GetVolume(other);
}
