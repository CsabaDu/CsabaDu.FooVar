using CsabaDu.FooVar.Measures.Interfaces.Behaviors;
using CsabaDu.FooVar.Measures.Interfaces.Factories;

namespace CsabaDu.FooVar.Measures.Interfaces.DataTypes;

public interface IMeasure : IBaseMeasure, ISum<IMeasure>, IMultiply<decimal, IMeasure>, IDivide<decimal, IMeasure>, IFit<IBaseMeasure>
{
    IMeasureFactory MeasureFactory { get; }

    IMeasure GetMeasure(ValueType quantity, Enum measureUnit, decimal? exchangeRate = null);

    IMeasure GetMeasure(ValueType quantity, IMeasurement? measurement = null);

    IMeasure GetMeasure(IBaseMeasure? other = null);
}
