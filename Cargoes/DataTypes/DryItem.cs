using CsabaDu.FooVar.Cargoes.Interfaces;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.FooVar.Cargoes.DataTypes;

internal sealed class DryItem<T> : DryMass<T>, IDryItem<T> where T : class, IDryBody
{
    internal DryItem(IWeight weight, T dryBody) : base(weight, dryBody) { }

    public IDryItem<T> GetDryItem(IDryMass<T>? dryMass = null)
    {
        if (dryMass == null) return this;

        IWeight weight = dryMass.Weight;
        T dryBody = dryMass.DryBody;

        return GetDryItem(weight, dryBody);
    }

    public IDryItem<T> GetDryItem(IWeight weight, T dryBody)
    {
        return new DryItem<T>(weight, dryBody);
    }

    public override IDryMass<T> GetDryMass(IWeight weight, T dryBody) => GetDryItem(weight, dryBody);
}
