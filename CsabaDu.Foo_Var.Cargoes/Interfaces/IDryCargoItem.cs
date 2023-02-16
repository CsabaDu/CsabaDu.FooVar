using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.Foo_Var.Cargoes.Interfaces
{
    public interface IDryCargoItem<T> : /*IContentSize, */ICargoItem/*, IDry<T>*/ where T : class, IGeometricBody
    {
        IPacking<T> Packing { get; init; }
        IPieceCount PackageCount { get; init; }

        IDryCargoItem<T> GetDryCargoItem();
        IDryCargoItem<T> GetDryCargoItem(ICargoItem cargoItem, IPacking<T> packing, IPieceCount pieceCount);
    }
}
