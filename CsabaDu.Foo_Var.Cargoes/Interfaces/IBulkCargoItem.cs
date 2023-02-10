namespace CsabaDu.Foo_Var.Cargoes.Interfaces;

public interface IBulkCargoItem : /*IBulk, */ICargoItem
{
    IBulkCargoItem GetBulkCargoItem();
    IBulkCargoItem GetBulkCargoItem(ICargoItem cargoItem);
}
