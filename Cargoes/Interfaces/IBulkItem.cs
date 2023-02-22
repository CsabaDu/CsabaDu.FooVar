using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;

namespace CsabaDu.FooVar.Cargoes.Interfaces
{
    public interface IBulkItem : IBulkMass
    {
        IBulkItem GetBulkItem(IMass? mass = null);
        IBulkItem GetBulkItem(IWeight weight, IBody body);
    }

    public interface IDryItem<T> : IDryMass<T> where T : class, IDryBody
    {
        IDryItem<T> GetDryItem(IDryMass<T>? dryMass = null);
        IDryItem<T> GetDryItem(IWeight weight, T dryBody);
    }
}
