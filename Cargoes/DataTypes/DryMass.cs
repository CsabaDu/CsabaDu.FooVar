using CsabaDu.FooVar.Cargoes.Interfaces;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;

namespace CsabaDu.FooVar.Cargoes.DataTypes;

internal abstract class DryMass : Mass, IDryMass
{
    private readonly IBulkMass _mass;

    private protected DryMass(IWeight weight, IDryBody dryBody) : base(weight)
    {
        _ = dryBody ?? throw new ArgumentNullException(nameof(dryBody));

        _mass = new BulkItem(weight, dryBody);
    }

    public override sealed int CompareTo(IMass? other)
    {
        if (other == null) return 1;

        if (other is IDryMass dryMass)
        {
            var (weight, body) = GetWeightAndBodyToCompareMass(dryMass);

            return CompareTo(weight, (IDryBody)body) ?? throw new ArgumentOutOfRangeException(nameof(other), "");
        }

        return base.CompareTo(other);
    }

    public override sealed bool? FitsIn(IMass? other = null, LimitType? limitType = null)
    {
        limitType ??= LimitType.BeNotGreater;

        if (other == null) return null;

        if (other is IDryMass dryMass)
        {
            var (weight, body) = GetWeightAndBodyToCompareMass(dryMass);
            int? nullableComparison = CompareTo(weight, (IDryBody)body);

            if (nullableComparison is not int comparison) return null;

            return comparison.FitsIn(limitType);
        }

        return base.FitsIn(other, limitType);
    }

    public override sealed IBody GetBody() => GetDryBody();

    public IBulkMass GetBulkMass() => _mass;

    public IDryMass GetDryMass() => this;

    public override sealed IMass GetMass(IWeight? weight = null)
    {
        if (weight == null) return this;

        IBody body = _mass.GetBody();

        return _mass.GetBulkMass(weight, body);
    }

    public override sealed bool Equals(IMass? other)
    {
        if (other is IDryMass dryMass)
        {
            return Weight.Equals(dryMass.Weight) && (GetDryBody() as IShape)!.Equals(dryMass.GetDryBody());
        }

        return base.Equals(other);
    }

    private int? CompareTo(IWeight weight, IDryBody dryBody)
    {
        int weightComparison = Weight.CompareTo(weight);
        int bodyComparison = (GetDryBody() as IShape)!.CompareTo(dryBody);

        return Compare(weightComparison, bodyComparison);
    }

    public abstract IDryBody GetDryBody();
}

internal abstract class DryMass<T> : DryMass, IDryMass<T> where T : class, IDryBody
{
    private protected DryMass(IWeight weight, T dryBody) : base(weight, dryBody)
    {
        DryBody = dryBody;
    }

    public T DryBody { get; init; }

    public override sealed IDryBody GetDryBody() => DryBody;

    public IDryMass<T> GetDryMass(IDryMass<T> other)
    {
        _ = other ?? throw new ArgumentNullException(nameof(other));

        IWeight weight = other.Weight;
        T dryBody = other.DryBody;

        return GetDryMass(weight, dryBody);
    }

    public abstract IDryMass<T> GetDryMass(IWeight weight, T dryBody);
}
