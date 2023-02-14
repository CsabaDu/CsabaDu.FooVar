using CsabaDu.Foo_Var.Measures.Interfaces.Behaviors;
using CsabaDu.Foo_Var.Measures.Interfaces.Factories;

namespace CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;

public interface IMeasure : IBaseMeasure, ISum<IMeasure>, IMultiply<decimal, IMeasure>, IDivide<decimal, IMeasure>, IFit<IBaseMeasure>
{
    IMeasureFactory MeasureFactory { get; init; }

    IMeasure GetMeasure(ValueType quantity, Enum measureUnit, decimal? exchangeRate = null);

    IMeasure GetMeasure(ValueType quantity, IMeasurement? measurement = null);

    IMeasure GetMeasure(IBaseMeasure? other = null);
}
