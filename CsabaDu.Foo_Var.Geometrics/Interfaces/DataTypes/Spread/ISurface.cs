using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.Factories;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Spread;

public interface ISurface : ISpread<IArea, AreaUnit>
{
    IArea Area { get; init; }

    ISurface GetSurface(AreaUnit? areaUnit = null);
    ISurface GetSurface(IArea area);
    ISurface GetSurface(ISpread<IArea, AreaUnit> spread);
    ISurface GetSurface(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits);
    ISurface GetSurface(IShape shape);
}