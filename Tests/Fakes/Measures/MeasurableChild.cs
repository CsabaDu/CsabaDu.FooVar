using CsabaDu.FooVar.Measures.Factories;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes;

namespace CsabaDu.FooVar.Tests.Fakes.Measures;

internal class MeasurableChild : Measurable
{
    internal MeasurableChild(Enum measureUnit) : base(new MeasurementFactory(), measureUnit) { }

    internal MeasurableChild(IMeasurement measurement) : base(measurement) { }

    public override IMeasurable GetMeasurable(Enum? measureUnit = null)
    {
        return measureUnit == null ? this : new MeasurableChild(measureUnit);
    }
}
