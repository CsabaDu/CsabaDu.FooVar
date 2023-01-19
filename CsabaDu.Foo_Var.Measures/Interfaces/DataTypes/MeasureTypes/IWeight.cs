namespace CsabaDu.Foo_Var.Measures.Interfaces.DataTypes.MeasureTypes;

public interface IWeight : IMeasure
{
    IWeight GetWeight(double quantity, WeightUnit weightUnit);

    IWeight GetWeight(IBaseMeasure? other = null);
}
