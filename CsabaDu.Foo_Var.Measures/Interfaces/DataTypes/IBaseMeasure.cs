using CsabaDu.Foo_Var.Measures.Interfaces.Behaviors;

namespace CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;

public interface IBaseMeasure : IMeasurable, IExchangeRate<IBaseMeasure>, IQuantity<IBaseMeasure>, IProportional<IBaseMeasure, Enum>
{
    IMeasurement Measurement { get; init; }

    object Quantity { get; init; }

    IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit, decimal? exchangeRate = null);

    IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement? measurement = null);

    IBaseMeasure GetBaseMeasure(IBaseMeasure? other = null);
}
