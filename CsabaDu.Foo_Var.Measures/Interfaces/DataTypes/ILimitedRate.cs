using CsabaDu.Foo_Var.Measures.Interfaces.Factories;

namespace CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;

public interface ILimitedRate : IMeasure, IRate, IEqualityComparer<ILimitedRate>
{
    ILimit Limit { get; }

    ILimitedRate GetLimitedRate(ValueType quantity, Enum measureUnit, IDenominator? denominator = null, decimal? exchangeRate = null, ILimit? limit = null);

    ILimitedRate GetLimitedRate(ValueType quantity, IMeasurement? measurement = null, IDenominator? denominator = null, ILimit? limit = null);

    ILimitedRate GetLimitedRate(IMeasure numerator, IDenominator? denominator = null, ILimit? limit = null);

    ILimitedRate GetLimitedRate(IFlatRate flatRate, ILimit? limit = null);

    ILimitedRate GetLimitedRate(ILimitedRate other, ILimit? limit = null);

    ILimitedRate GetLimitedRate(IRate? rate = null);
}
