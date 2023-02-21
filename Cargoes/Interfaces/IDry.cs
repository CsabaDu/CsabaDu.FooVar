using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.FooVar.Cargoes.Interfaces
{
    public interface IDry : IMass/*, IDensity*/
    {
        IDryBody GetDryBody();
        IBulk GetBulk();
        IDry GetDry();
    }

    public interface IDry<T> : IDry where T : class, IDryBody
    {
        T DryBody { get; init;}

        //IDry<T> GetDry();
        IDry<T> GetDry(IWeight weight, T dryBody);
    }
}
