using CsabaDu.FooVar.Measures.Interfaces.DataTypes;

namespace CsabaDu.FooVar.Tests.Fakes.Measures;

internal class BaseMeasureChild : BaseMeasure
{
    internal BaseMeasureChild(ValueType quantity, Enum measureUnit, decimal? exchangeRate = null) : base(quantity, measureUnit, exchangeRate) { }

    internal BaseMeasureChild(ValueType quantity, IMeasurement measurement) : base(quantity, measurement) { }

    internal BaseMeasureChild(IBaseMeasure other) : base(other) { }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit, decimal? exchangeRate = null)
    {
        return new BaseMeasureChild(quantity, measureUnit, exchangeRate);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement? measurement = null)
    {
        return new BaseMeasureChild(quantity, measurement ?? Measurement);
    }

    public override IBaseMeasure GetBaseMeasure(IBaseMeasure? other = null)
    {
        return other == null ? this : new BaseMeasureChild(other);
    }
}
