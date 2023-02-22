using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread.SpreadTypes;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Factories;

public interface ISurfaceFactory
{
    IBulkSurface GetSurface(IArea area);
    IBulkSurface GetSurface(ISpread<IArea, AreaUnit> spread);
    IBulkSurface GetSurface(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits);
    IBulkSurface GetSurface(IPlaneShape planeShape);
    IBulkSurface GetSurface(IDryBody dryBody);
}
