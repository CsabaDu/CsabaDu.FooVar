using CsabaDu.FooVar.Cargoes.Interfaces;
using CsabaDu.FooVar.Geometrics.Factories;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread.SpreadTypes;

namespace CsabaDu.FooVar.Cargoes.DataTypes;

internal abstract class BulkMass : Mass, IBulkMass
{
    protected BulkMass(IWeight weight, IBody body) : base(weight)
    {
        _ = body ?? throw new ArgumentNullException(nameof(body));

        BulkBody = new SpreadFactory().GetBody(body);
    }
    public IBulkBody BulkBody { get; init; }

    public override sealed int CompareTo(IMass? other)
    {
        return base.CompareTo(other);
    }

    public override sealed bool? FitsIn(IMass? other = null, LimitType? limitType = null)
    {
        return base.FitsIn(other, limitType);
    }

    public override sealed IBody GetBody() => BulkBody;

    public IBulkMass GetBulkMass() => this;

    public override sealed IMass GetMass(IWeight? weight = null)
    {
        if (weight == null) return GetBulkMass();

        return GetBulkMass(weight, BulkBody);
    }

    public override sealed bool Equals(IMass? other)
    {
        return base.Equals(other);
    }
    public IBulkMass GetBulkMass(IWeight weight, IBody body)
    {
        return new BulkItem(weight, body);
    }
    public abstract IBulkMass GetBulkMass(IMass? mass = null);
}
