namespace CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;

public interface IFlatRate : IMeasure, IRate, IMultiply<IMeasure, IMeasure>, ISum<IFlatRate>
{
    IFlatRate GetFlatRate(ValueType quantity, Enum measureUnit, IDenominator? denominator = null, decimal? exchangeRate = null);

    IFlatRate GetFlatRate(ValueType quantity, IMeasurement? measurement = null, IDenominator? denominator = null);

    IFlatRate GetFlatRate(IMeasure numerator, IDenominator? denominator = null);

    IFlatRate GetFlatRate(IRate? rate = null);
}
