namespace CsabaDu.Foo_Var.Cargoes.Interfaces;

public interface ICargoItem
{
    IEnumerable<IGoods> GoodsList { get; init; }

    IWeight GetNetWeight();
    IVolume? GetNetVolume();
    ICargoItem GetCargoItem();
}
