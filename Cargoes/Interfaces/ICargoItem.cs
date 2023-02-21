using CsabaDu.FooVar.Commodities.Interfaces;

namespace CsabaDu.FooVar.Cargoes.Interfaces;

public interface ICargoItem : IBulkMass
{
    IEnumerable<IGoods> GoodsList { get; init; }

    IMass GetNetMass();
    ICargoItem GetCargoItem();
    ICargoItem GetCargoItem(IEnumerable<IGoods> goodsList);
}
