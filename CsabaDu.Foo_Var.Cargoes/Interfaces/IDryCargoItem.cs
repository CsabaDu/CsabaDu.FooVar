using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.Foo_Var.Cargoes.Interfaces
{
    public interface IDryCargoItem<T> : IContentSize, ICargoItem/*, IDry<T>*/ where T : class, IGeometricBody
    {
        IPacking<T> Packing { get; init; }
        IPieceCount CargoItemPieceCount { get; init; }

        IDryCargoItem<T> GetDryCargoItem();
    }
}
