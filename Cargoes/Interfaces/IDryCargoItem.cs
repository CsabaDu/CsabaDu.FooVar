using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.FooVar.Cargoes.Interfaces
{
    public interface IDryCargoItem<T> : /*IContentSize, */ICargoItem/*, IDry<T>*/ where T : class, IDryBody
    {
        IPacking<T> Packing { get; init; }
        IPieceCount PackageCount { get; init; }

        IDryCargoItem<T> GetDryCargoItem();
        IDryCargoItem<T> GetDryCargoItem(ICargoItem cargoItem, IPacking<T> packing, IPieceCount pieceCount);
    }
}
