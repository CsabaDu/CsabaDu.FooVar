using CsabaDu.FooVar.Measures.Interfaces.DataTypes;

namespace CsabaDu.FooVar.Measures.Interfaces.Factories;

public interface IRateFactory : ILimtedRateFactory, IFlatRateFactory
{
    IRate GetRate(ValueType quantity, Enum measureUnit, IDenominator denominator, decimal? exchangeRate = null, ILimit? limit = null);

    IRate GetRate(ValueType quantity, IMeasurement measurement, IDenominator denominator, ILimit? limit = null);

    IRate GetRate(IMeasure numerator, IDenominator denominator, ILimit? limit = null);

    IRate GetRate(IRate other, ILimit? limit = null);
}
