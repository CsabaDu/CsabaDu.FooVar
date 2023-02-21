using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.FooVar.Cargoes.Interfaces
{
    public interface IDryMass : IMass/*, IDensity*/
    {
        IDryBody GetDryBody();
        IBulkMass GetBulkMass();
        IDryMass GetDryMass();
    }

    public interface IDryMass<T> : IDryMass where T : class, IDryBody
    {
        T DryBody { get; init;}

        //IDryMass<T> GetDryMass();
        IDryMass<T> GetDryMass(IWeight weight, T dryBody);
    }
}
