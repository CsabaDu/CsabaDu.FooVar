using CsabaDu.FooVar.Cargoes.Interfaces;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;

namespace CsabaDu.FooVar.Cargoes.DataTypes
{
    internal abstract class DryMass : Mass, IDryMass
    {

        private readonly IBulkMass _bulkMass;

        private protected DryMass(IWeight weight, IDryBody dryBody) : base(weight)
        {
            _ = dryBody ?? throw new ArgumentNullException(nameof(dryBody));

            _bulkMass = new BulkItem(weight, dryBody);
        }

        public override sealed IBody GetBody() => GetDryBody();

        public IBulkMass GetBulkMass() => _bulkMass;

        public IDryMass GetDryMass() => this;

        public override IMass GetMass(IWeight? weight = null)
        {
            if (weight == null) return this;

            IBody body = _bulkMass.GetBody();

            return _bulkMass.GetBulkMass(weight, body);
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

        public abstract IDryMass<T> GetDryMass(IWeight weight, T dryBody);
        public IDryMass<T> GetDryMass(IDryMass<T> other)
        {
            _ = other ?? throw new ArgumentNullException(nameof(other));

            IWeight weight = other.Weight;
            T dryBody = other.DryBody;

            return GetDryMass(weight, dryBody);
        }
    }
}
