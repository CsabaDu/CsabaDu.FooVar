using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;

namespace CsabaDu.Foo_Var.Measures.Interfaces.Factories
{
    public interface IFlatRateFactory : IDenominatorFactory, IMeasureFactory
    {
        IFlatRate GetFlatRate(ValueType quantity, Enum measureUnit, IDenominator denominator, decimal? exchangeRate = null);

        IFlatRate GetFlatRate(ValueType quantity, IMeasurement measurement, IDenominator denominator);

        IFlatRate GetFlatRate(IMeasure numerator, IDenominator denominator);

        IFlatRate GetFlatRate(IRate rate);
    }
}
