using CsabaDu.FooVar.Measures.Interfaces.Factories;

namespace CsabaDu.FooVar.Measures.Interfaces.DataTypes;

public interface ILimit : IBaseMeasure, IEqualityComparer<ILimit>
{
    LimitType LimitType { get; }

    ILimit GetLimit(Enum measureUnit, ValueType? quantity = null, decimal? exchangeRate = null, LimitType limitType = default);

    ILimit GetLimit(IMeasurement? measurement = null, ValueType? quantity = null, LimitType limitType = default);

    ILimit GetLimit(IBaseMeasure? baseMeasure = null, LimitType limitType = default);

    ILimit GetLimit(ILimit? other = null);
}
