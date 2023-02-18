using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape;

namespace CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread.SpreadTypes;

public interface IBulkBody : IBody
{
    IBulkBody GetBulkBody(VolumeUnit? volumeUnit = null);
    IBulkBody GetBulkBody(IVolume volume);
    IBulkBody GetBulkBody(ISpread<IVolume, VolumeUnit> spread);
    IBulkBody GetBulkBody(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits);
    IBulkBody GetBulkBody(IShape shape);
}