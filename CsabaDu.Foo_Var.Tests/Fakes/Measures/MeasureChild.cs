using CsabaDu.Foo_Var.Measures.DataTypes;
using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;

namespace CsabaDu.Foo_Var.Tests.Fakes.Measures;

internal class MeasureChild : Measure
{
    public MeasureChild(Enum measureUnit, ValueType quantity) : base(quantity, measureUnit) { }

    public MeasureChild(IMeasurement measurement, ValueType quantity) : base(quantity, measurement) { }

    public MeasureChild(IBaseMeasure other) : base(other) { }

    public override IMeasure GetMeasure(IBaseMeasure? other = null)
    {
        return other == null ? this : new MeasureChild(other);
    }
}
