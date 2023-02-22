using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread.SpreadTypes;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Factories;

public interface IBodyFactory
{
    IBulkBody GetBody(IVolume volume);
    IBulkBody GetBody(ISpread<IVolume, VolumeUnit> spread);
    IBulkBody GetBody(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits);
}
