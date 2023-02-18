using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape;

namespace CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;

public interface ISurface : ISpread<IArea, AreaUnit>
{
    IArea Area { get; init; }

    ISurface GetSurface(AreaUnit? areaUnit = null);
    ISurface GetSurface(IArea area);
    ISurface GetSurface(ISpread<IArea, AreaUnit> spread);
    ISurface GetSurface(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits);
    ISurface GetSurface(IShape shape);
}