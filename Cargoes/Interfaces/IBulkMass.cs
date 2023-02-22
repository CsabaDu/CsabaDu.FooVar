using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread.SpreadTypes;

namespace CsabaDu.FooVar.Cargoes.Interfaces;

public interface IBulkMass : IMass/*, IDensity*/
{
    IBulkBody BulkBody {get; init;}

    IBulkMass GetBulkMass(IMass? mass = null);
    IBulkMass GetBulkMass(IWeight weight, IBody body);
}
