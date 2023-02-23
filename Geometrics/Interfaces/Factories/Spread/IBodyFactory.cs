using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread.SpreadTypes;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Factories;

public interface IBodyFactory
{
    IBulkBody GetBulkBody(IVolume volume);
    IBulkBody GetBulkBody(ISpread<IVolume, VolumeUnit> spread);
    IBulkBody GetBulkBody(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits);
}
