using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread.SpreadTypes;

namespace CsabaDu.FooVar.Cargoes.Interfaces;

public interface IBulk : IMass/*, IDensity*/
{
    IBulkBody BulkBody {get; init;}

    IBulk GetBulk();
    IBulk GetBulk(IWeight weight, IBody body);
}
