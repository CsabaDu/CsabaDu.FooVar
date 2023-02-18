namespace CsabaDu.FooVar.Measures.Interfaces.DataTypes.MeasureTypes;

public interface IWeight : IMeasure
{
    IWeight GetWeight(double quantity, WeightUnit weightUnit);

    IWeight GetWeight(IBaseMeasure? other = null);
}
