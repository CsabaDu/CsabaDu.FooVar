using CsabaDu.FooVar.Measures.Interfaces.DataTypes;

namespace CsabaDu.FooVar.Measures.Interfaces.Factories
{
    public interface ILimtedRateFactory : IDenominatorFactory, IMeasureFactory, ILimitFactory
    {
        ILimitedRate GetLimitedRate(ValueType quantity, Enum measureUnit, IDenominator denominator, decimal? exchangeRate = null, ILimit? limit = null);

        ILimitedRate GetLimitedRate(ValueType quantity, IMeasurement measurement, IDenominator denominator, ILimit? limit = null);

        ILimitedRate GetLimitedRate(IMeasure numerator, IDenominator denominator, ILimit? limit = null);

        ILimitedRate GetLimitedRate(IRate rate, ILimit? limit = null);
    }
}
