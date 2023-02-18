namespace CsabaDu.FooVar.Commodities.Interfaces
{
    public interface ICommodity
    {
        string CommodityName { get; set; }
        Type GoodsMeasureUnitType { get; init; }
        object CommodityCustomsCode { get; init; }
        object HandlingType { get; init; }
    }
}
