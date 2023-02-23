using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread.SpreadTypes;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Factories;

public interface ISurfaceFactory
{
    IBulkSurface GetBulkSurface(IArea area);
    IBulkSurface GetBulkSurface(ISpread<IArea, AreaUnit> spread);
    IBulkSurface GetBulkSurface(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits);
    IBulkSurface GetBulkSurface(IPlaneShape planeShape);
    IBulkSurface GetBulkSurface(IDryBody dryBody);
}
