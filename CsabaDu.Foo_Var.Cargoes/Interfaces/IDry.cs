using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.Foo_Var.Cargoes.Interfaces
{
    public interface IDry : IMass/*, IDensity*/
    {
        IGeometricBody GetGeometricBody();
        IBulk GetBulk();
        IDry GetDry();
    }

    public interface IDry<T> : IDry where T : class, IGeometricBody
    {
        T GeometricBody { get; init;}

        //IDry<T> GetDry();
        IDry<T> GetDry(IWeight weight, T geometricBody);
    }
}
