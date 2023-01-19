using CsabaDu.Foo_Var.Measures.DataTypes;
using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;

namespace CsabaDu.Foo_Var.Tests.Fakes.Measures;

internal class MeasurableChild : Measurable
{
    internal MeasurableChild(Enum measureUnit) : base(measureUnit) { }

    internal MeasurableChild(IMeasurement measurement) : base(measurement) { }

    public override IMeasurable GetMeasurable(Enum? measureUnit = null)
    {
        return measureUnit == null ? this : new MeasurableChild(measureUnit);
    }
}
