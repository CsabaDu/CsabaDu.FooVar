using CsabaDu.FooVar.Measures.Factories;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes.MeasureTypes;

namespace CsabaDu.FooVar.Measures.DataTypes.MeasureTypes;

internal sealed class Time : Measure, ITime
{
    internal Time(ValueType quantity, TimeUnit timeUnit) : base(new MeasureFactory(), quantity, timeUnit)
    {
        Quantity = quantity.ToQuantity(TypeCode.Double) ?? throw new ArgumentOutOfRangeException(nameof(quantity), quantity, null);
    }

    internal Time(ValueType quantity, IMeasurement measurement) : base(new MeasureFactory(), quantity, measurement)
    {
        measurement.ValidateMeasureUnitType(typeof(TimeUnit));

        Quantity = quantity.ToQuantity(TypeCode.Double) ?? throw new ArgumentOutOfRangeException(nameof(quantity), quantity, null);
    }

    internal Time(IBaseMeasure other) : base(new MeasureFactory(), other)
    {
        other.ValidateMeasureUnitType(typeof(TimeUnit));
        ValueType quantity = other.GetQuantity();

        Quantity = quantity.ToQuantity(TypeCode.Double) ?? throw new ArgumentOutOfRangeException(nameof(other), quantity, null);
    }

    public ITime GetTime(double quantity, TimeUnit timeUnit)
    {
        return new Time(quantity, timeUnit);
    }

    public ITime GetTime(IBaseMeasure? other = null)
    {
        return other == null ? this : new Time(other);
    }

    public override IMeasure GetMeasure(IBaseMeasure? other = null) => GetTime(other);
}
