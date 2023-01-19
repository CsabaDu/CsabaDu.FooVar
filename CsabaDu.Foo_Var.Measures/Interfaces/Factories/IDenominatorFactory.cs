using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;

namespace CsabaDu.Foo_Var.Measures.Interfaces.Factories;

public interface IDenominatorFactory
{
    IDenominator GetDenominator(Enum measureUnit, ValueType? quantity = default, decimal? exchangeRate = null);

    IDenominator GetDenominator(IMeasurement measurement, ValueType? quantity = default);
}
