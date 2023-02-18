using CsabaDu.FooVar.Measures.Interfaces.DataTypes;

namespace CsabaDu.FooVar.Measures.Interfaces.Builders;

public interface IRateBuilder
{
    IRateBuilder SetMeasurement(Enum measureUnit, BaseMeasureType baseMeasureType = default, decimal? exchangeRate = null);

    IRateBuilder SetMeasurement(IMeasurement measurement, BaseMeasureType baseMeasureType = default);

    IRateBuilder SetQuantity(ValueType quantity, BaseMeasureType baseMeasureType = default);

    IRateBuilder SetLimitType(LimitType limitType);

    IRateBuilder SetMeasurements(IMeasurement numeratorMeasurement, IMeasurement denominatorMeasurement, IMeasurement? limitMeasurement = null);

    IRateBuilder SetQuantities(ValueType numeratorQuantity, ValueType? denominatorQuantity = null, ValueType? limitQuantity = null);

    IFlatRate BuildFlatRate();

    ILimitedRate BuildLimitedRate();

    IRate BuildRate();
}
