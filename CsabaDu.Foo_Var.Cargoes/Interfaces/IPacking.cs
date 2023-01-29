using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.Foo_Var.Cargoes.Interfaces
{
    public interface IPacking<T> : IDry<T> where T : class, IGeometricBody
    {
        object PackingType { get; set; }
        IWeight? TareWeight { get; set; }
        IVolume? InnerVolume { get; set; }

        IPacking<T> GetPacking();
    }
}
