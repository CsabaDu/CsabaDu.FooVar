using CsabaDu.FooVar.Cargoes.Interfaces;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;
using CsabaDu.FooVar.Measures.Factories;
using CsabaDu.FooVar.Measures.Interfaces.Factories;

namespace CsabaDu.FooVar.Cargoes.DataTypes;

internal abstract class Mass : IMass
{
    private protected Mass(IWeight weight)
    {
        Weight = GetValidWeight(weight);
    }

    public IWeight Weight { get; init; }

    public virtual int CompareTo(IMass? other)
    {
        if (other == null) return 1;

        var (weight, body) = GetWeightAndBodyToCompareMass(other);

        return CompareTo(weight, body) ?? throw new ArgumentOutOfRangeException(nameof(other), "");
    }

    public virtual bool Equals(IMass? other)
    {
        return other is IMass mass && Weight.Equals(mass.Weight) && GetBody().Equals(mass.GetBody());
    }

    public virtual bool? FitsIn(IMass? other = null, LimitType? limitType = null)
    {
        if (other == null) return null;

        limitType ??= LimitType.BeNotGreater;
        var (weight, body) = GetWeightAndBodyToCompareMass(other);
        int? nullableComparison = CompareTo(weight, body);

        if (nullableComparison is not int comparison) return null;

        return comparison.FitsIn(limitType);
    }

    public IFlatRate GetDensity()
    {
        IFlatRateFactory factory = new RateFactory(Weight.MeasureFactory);
        IVolume volume = GetBody().Volume;
        IDenominator volumeDenominator = factory.GetDenominator(volume);

        return factory.GetFlatRate(Weight, volumeDenominator);
    }

    private int? CompareTo(IWeight weight, IBody body)
    {
        int weightComparison = Weight.CompareTo(weight);
        int bodyComparison = GetBody().CompareTo(body);

        return Compare(weightComparison, bodyComparison);
    }

    protected static (IWeight, IBody) GetWeightAndBodyToCompareMass(IMass other)
    {
        IWeight weight = other.Weight;
        IBody body = other.GetBody();

        return (weight, body);
    }

    protected static int? Compare(int weightComparison, int bodyComparison)
    {
        return weightComparison == bodyComparison ? weightComparison : null;
    }

    private static IWeight GetValidWeight(IWeight? weight)
    {
        _ = weight ?? throw new ArgumentNullException(nameof(weight));

        decimal quantity = weight.GetDecimalQuantity();

        if (quantity < 0) throw new ArgumentOutOfRangeException(nameof(weight), quantity, null);

        return weight;
    }

    public abstract IBody GetBody();
    public abstract IMass GetMass(IWeight? weight = null);
}
