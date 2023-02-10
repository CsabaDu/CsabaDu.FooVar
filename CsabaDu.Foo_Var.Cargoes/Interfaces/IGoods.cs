namespace CsabaDu.Foo_Var.Cargoes.Interfaces
{
    public interface ICommodity
    {
        string CommodityName { get; set; }
        Type GoodsMeasureUnitType { get; init; }
        object CommodityCustomsCode { get; init; }
        object HandlingType { get; init; }
    }


    public interface IGoods : IMass, ICommodity
    {
        IMeasure GoodsMeasure { get; init; }

        IGoods GetGoods();
        IGoods GetGoods(ICommodity commodity, IMeasure goodsMeasure, IMass mass);
    }
}
