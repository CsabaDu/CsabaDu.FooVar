using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;

namespace CsabaDu.Foo_Var.Measures.Interfaces.Builders;

public interface IRateBuilder
{
    IRateBuilder SetMeasurement(Enum measureUnit, BaseMeasure baseMeasureType = default, decimal? exchangeRate = null);

    IRateBuilder SetMeasurement(IMeasurement measurement, BaseMeasure baseMeasureType = default);

    IRateBuilder SetQuantity(ValueType quantity, BaseMeasure baseMeasureType = default);

    IRateBuilder SetLimitType(LimitType limitType);

    IRateBuilder SetMeasurements(IMeasurement numeratorMeasurement, IMeasurement denominatorMeasurement, IMeasurement? limitMeasurement = null);

    IRateBuilder SetQuantities(ValueType numeratorQuantity, ValueType? denominatorQuantity = null, ValueType? limitQuantity = null);

    IFlatRate BuildFlatRate();

    ILimitedRate BuildLimitedRate();

    IRate BuildRate();
}
