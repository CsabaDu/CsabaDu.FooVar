namespace CsabaDu.Foo_Var.Cargoes.Interfaces;

public interface ICargoItem : IBulk
{
    IEnumerable<IGoods> GoodsList { get; init; }

    IMass GetNetMass();
    ICargoItem GetCargoItem();
    ICargoItem GetCargoItem(IEnumerable<IGoods> goodsList);
}
