using CsabaDu.FooVar.Measures.Interfaces.Behaviors;
using CsabaDu.FooVar.Measures.Interfaces.Factories;

namespace CsabaDu.FooVar.Measures.Interfaces.DataTypes;

public interface IRate : IProportional<IRate, IRate>, IExchange<IRate, IDenominator>
{
    IDenominator Denominator { get; init; }

    IRateFactory RateFactory { get; init; }

    IMeasure GetNumerator(IBaseMeasure? other = null);

    ILimit? GetLimit();

    IRate GetRate(IMeasure numerator, IDenominator? denominator = null, ILimit? limit = null);

    IRate GetRate(IRate other, ILimit? limit = null);

    IRate GetRate(IRate? other = null);
}
