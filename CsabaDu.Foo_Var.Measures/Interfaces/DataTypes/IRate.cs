using CsabaDu.Foo_Var.Measures.Interfaces.Behaviors;
using CsabaDu.Foo_Var.Measures.Interfaces.Factories;

namespace CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;

public interface IRate : IProportional<IRate, IRate>, IExchange<IRate, IDenominator>
{
    IDenominator Denominator { get; init; }

    IRateFactory RateFactory { get; }

    IMeasure GetNumerator(IBaseMeasure? other = null);

    ILimit? GetLimit();

    IRate GetRate(IMeasure numerator, IDenominator? denominator = null, ILimit? limit = null);

    IRate GetRate(IRate other, ILimit? limit = null);

    IRate GetRate(IRate? other = null);
}
