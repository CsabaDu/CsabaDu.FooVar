using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;

namespace CsabaDu.Foo_Var.Measures.Interfaces.Factories;

public interface IMeasureFactory
{
    IMeasure GetMeasure(ValueType quantity, Enum measureUnit, decimal? exchangeRate = null);

    IMeasure GetMeasure(ValueType quantity, IMeasurement measurement);

    IMeasure GetMeasure(IBaseMeasure baseMeasure);
}
