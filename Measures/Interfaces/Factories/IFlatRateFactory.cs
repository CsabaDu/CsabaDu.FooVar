using CsabaDu.FooVar.Measures.Interfaces.DataTypes;

namespace CsabaDu.FooVar.Measures.Interfaces.Factories
{
    public interface IFlatRateFactory : IDenominatorFactory, IMeasureFactory
    {
        IFlatRate GetFlatRate(ValueType quantity, Enum measureUnit, IDenominator denominator, decimal? exchangeRate = null);

        IFlatRate GetFlatRate(ValueType quantity, IMeasurement measurement, IDenominator denominator);

        IFlatRate GetFlatRate(IMeasure numerator, IDenominator denominator);

        IFlatRate GetFlatRate(IRate rate);
    }
}
