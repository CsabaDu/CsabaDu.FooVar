using CsabaDu.FooVar.Measures.Interfaces.DataTypes;

namespace CsabaDu.FooVar.Measures.Interfaces.Factories;

public interface ILimitFactory
{
    ILimit GetLimit(Enum measureUnit, ValueType? quantity = default, decimal? exchangeRate = null, LimitType limitType = default);

    ILimit GetLimit(IMeasurement measurement, ValueType? quantity = default, LimitType limitType = default);

    ILimit GetLimit(IBaseMeasure baseMeasure, LimitType limitType = default);
}
