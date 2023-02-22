using CsabaDu.FooVar.Cargoes.Interfaces;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;

namespace CsabaDu.FooVar.Cargoes.DataTypes
{
    internal sealed class BulkItem : BulkMass, IBulkItem
    {
        internal BulkItem(IWeight weight, IBody body) : base(weight, body) { }

        public IBulkItem GetBulkItem(IMass? mass = null)
        {
            if (mass == null) return this;

            IWeight weight = mass.Weight;
            IBody body = mass.GetBody();

            return GetBulkItem(weight, body);
        }

        public IBulkItem GetBulkItem(IWeight weight, IBody body)
        {
            return new BulkItem(weight, body);
        }

        public override IBulkMass GetBulkMass(IMass? mass = null) => GetBulkItem(mass);
    }
}
