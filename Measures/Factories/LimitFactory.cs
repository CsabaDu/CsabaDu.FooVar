using CsabaDu.FooVar.Measures.DataTypes;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes;
using CsabaDu.FooVar.Measures.Interfaces.Factories;

namespace CsabaDu.FooVar.Measures.Factories;

public sealed class LimitFactory : ILimitFactory
{
    #region Public methods
    public ILimit GetLimit(Enum measureUnit, ValueType? quantity = null, decimal? exchangeRate = null, LimitType limitType = default)
    {
        return new Limit(measureUnit, quantity, exchangeRate, limitType);
    }

    public ILimit GetLimit(IMeasurement measurement, ValueType? quantity = null, LimitType limitType = default)
    {
        return new Limit(measurement, quantity, limitType);
    }

    public ILimit GetLimit(IBaseMeasure baseMeasure, LimitType limitType = default)
    {
        return GetLimit(baseMeasure.Measurement, baseMeasure.GetQuantity(), limitType);
    }
    #endregion
}
