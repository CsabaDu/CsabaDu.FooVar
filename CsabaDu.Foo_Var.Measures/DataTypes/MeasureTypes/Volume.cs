using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;
using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes.MeasureTypes;

namespace CsabaDu.Foo_Var.Measures.DataTypes.MeasureTypes;

internal sealed class Volume : Measure, IVolume
{
    public Volume(ValueType quantity, VolumeUnit volumeUnit) : base(quantity, volumeUnit)
    {
        Quantity = quantity.ToQuantity(typeof(double))!;
    }

    public Volume(ValueType quantity, IMeasurement measurement) : base(quantity, measurement)
    {
        measurement.ValidateMeasureUnitType(typeof(VolumeUnit));

        Quantity = quantity.ToQuantity(typeof(double))!;
    }

    public Volume(IBaseMeasure other) : base(other)
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
