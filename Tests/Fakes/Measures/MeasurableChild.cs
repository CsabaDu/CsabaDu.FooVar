using CsabaDu.FooVar.Measures.Factories;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes;
using CsabaDu.FooVar.Measures.Interfaces.Factories;

namespace CsabaDu.FooVar.Tests.Fakes.Measures;

internal class MeasurableChild : Measurable
{
    internal MeasurableChild(IMeasurementFactory measurementFactory, Enum measureUnit) : base(measurementFactory, measureUnit) { }

    internal MeasurableChild(IMeasurement measurement) : base(measurement) { }

    public override IMeasurable GetMeasurable(Enum? measureUnit = null)
    {
        return measureUnit == null ? this : new MeasurableChild(MeasurementFactory, measureUnit);
    }
}
