using CsabaDu.Foo_Var.Measures.Factories;
using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;

namespace CsabaDu.Foo_Var.Tests.Fakes.Measures;

internal class MeasurableChild : Measurable
{
    internal MeasurableChild(Enum measureUnit) : base(new MeasurementFactory(), measureUnit) { }

    internal MeasurableChild(IMeasurement measurement) : base(new MeasurementFactory(), measurement) { }

    public override IMeasurable GetMeasurable(Enum? measureUnit = null)
    {
        return measureUnit == null ? this : new MeasurableChild(measureUnit);
    }
}
