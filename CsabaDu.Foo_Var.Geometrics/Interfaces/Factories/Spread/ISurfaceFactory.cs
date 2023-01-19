using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Spread;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Factories;

public interface ISurfaceFactory
{
    ISurface GetSurface(IArea area);
    ISurface GetSurface(ISpread<IArea, AreaUnit> spread);
    ISurface GetSurface(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits);
    ISurface GetSurface(IPlaneShape planeShape);
    ISurface GetSurface(IGeometricBody geometricBody);
}
