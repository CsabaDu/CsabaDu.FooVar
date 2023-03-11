using CsabaDu.FooVar.Measures.Factories;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes.MeasureTypes;

namespace CsabaDu.FooVar.Measures.DataTypes.MeasureTypes;

internal sealed class Volume : Measure, IVolume
{
    internal Volume(ValueType quantity, VolumeUnit volumeUnit) : base(new MeasureFactory(), quantity, volumeUnit)
    {
        Quantity = quantity.ToQuantity(TypeCode.Double) ?? throw new ArgumentOutOfRangeException(nameof(quantity), quantity, null);
    }

    internal Volume(ValueType quantity, IMeasurement measurement) : base(new MeasureFactory(), quantity, measurement)
    {
        measurement.ValidateMeasureUnitType(typeof(VolumeUnit));

        Quantity = quantity.ToQuantity(TypeCode.Double) ?? throw new ArgumentOutOfRangeException(nameof(quantity), quantity, null);
    }

    internal Volume(IBaseMeasure other) : base(new MeasureFactory(), other)
    {
        other.ValidateMeasureUnitType(typeof(VolumeUnit));
        ValueType quantity = other.GetQuantity();

        Quantity = quantity.ToQuantity(TypeCode.Double) ?? throw new ArgumentOutOfRangeException(nameof(other), quantity, null);
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
