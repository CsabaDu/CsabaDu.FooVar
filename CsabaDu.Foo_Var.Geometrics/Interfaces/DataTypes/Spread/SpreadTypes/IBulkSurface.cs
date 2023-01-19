using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Spread.SpreadTypes;

public interface IBulkSurface : ISurface
{
    IBulkSurface GetBulkSurface(AreaUnit? areaUnit = null);
    IBulkSurface GetBulkSurface(IArea area);
    IBulkSurface GetBulkSurface(ISpread<IArea, AreaUnit> spread);
    IBulkSurface GetBulkSurface(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits);
    IBulkSurface GetBulkSurface(IShape shape);
}