using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Spread;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Factories;

public interface IBodyFactory
{
    IBody GetBody(IVolume volume);
    IBody GetBody(ISpread<IVolume, VolumeUnit> spread);
    IBody GetBody(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits);
    //IBody GetBody(IGeometricBody geometricBody);
}
