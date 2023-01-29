using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.Foo_Var.Cargoes.Interfaces
{
    public interface IDry<T> : IMass where T : class, IGeometricBody
    {
        T GeometricBody { get; init;}

        IBulk GetBulk();
        IDry<T> GetDry();
        IDry<T> GetDry(IWeight weight, IEnumerable<IExtent> shapeExtentList);
    }
}
