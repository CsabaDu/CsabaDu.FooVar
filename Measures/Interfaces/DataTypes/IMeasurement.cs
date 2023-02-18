namespace CsabaDu.FooVar.Measures.Interfaces.DataTypes;

public interface IMeasurement : IMeasurable, IProportional<IMeasurement, Enum>
{
    decimal ExchangeRate { get; init; }
    Type MeasureUnitType { get; init; }

    IMeasurement GetMeasurement(Enum measureUnit, decimal? exchangeRate = null);
    IMeasurement GetMeasurement(IMeasurement? other = null);
}
