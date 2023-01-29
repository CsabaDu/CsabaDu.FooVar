using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Spread.SpreadTypes;

namespace CsabaDu.Foo_Var.Cargoes.Interfaces;

public interface IBulk : IMass
{
    IBulkBody BulkBody {get; init;}

    IBulk GetBulk();
    IBulk GetBulk(IWeight weight, IVolume volume);
}
