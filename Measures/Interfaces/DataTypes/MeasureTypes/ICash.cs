namespace CsabaDu.FooVar.Measures.Interfaces.DataTypes.MeasureTypes;

public interface ICash : IMeasure
{
    ICash GetCash(decimal quantity, Currency currency);

    ICash GetCash(IBaseMeasure? other = null);
}
