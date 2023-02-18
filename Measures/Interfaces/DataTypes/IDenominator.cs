namespace CsabaDu.FooVar.Measures.Interfaces.DataTypes;

public interface IDenominator : IBaseMeasure
{
    IDenominator GetDenominator(Enum measureUnit, ValueType? quantity = default, decimal? exchangeRate = null);

    IDenominator GetDenominator(IMeasurement measurement, ValueType? quantity = default);

    IDenominator GetDenominator(IBaseMeasure? baseMeasure = null);
}
