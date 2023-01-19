using CsabaDu.Foo_Var.Measures.Interfaces.Factories;

namespace CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;

public interface ILimit : IBaseMeasure, IEqualityComparer<ILimit>
{
    LimitType LimitType { get; init; }

    ILimit GetLimit(Enum measureUnit, ValueType? quantity = null, decimal? exchangeRate = null, LimitType limitType = default);

    ILimit GetLimit(IMeasurement? measurement = null, ValueType? quantity = null, LimitType limitType = default);

    ILimit GetLimit(IBaseMeasure? baseMeasure = null, LimitType limitType = default);

    ILimit GetLimit(ILimit? other = null);
}
