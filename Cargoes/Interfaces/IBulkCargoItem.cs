namespace CsabaDu.FooVar.Cargoes.Interfaces;

public interface IBulkCargoItem : /*IBulk, */ICargoItem
{
    IBulkCargoItem GetBulkCargoItem();
    IBulkCargoItem GetBulkCargoItem(ICargoItem cargoItem);
}
