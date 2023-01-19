using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;

namespace CsabaDu.Foo_Var.Measures.Interfaces.Factories;

public interface ILimitFactory
{
    ILimit GetLimit(Enum measureUnit, ValueType? quantity = default, decimal? exchangeRate = null, LimitType limitType = default);

    ILimit GetLimit(IMeasurement measurement, ValueType? quantity = default, LimitType limitType = default);

    ILimit GetLimit(IBaseMeasure baseMeasure, LimitType limitType = default);
}
