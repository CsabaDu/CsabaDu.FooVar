using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Factories;

public interface IBodyFactory
{
    IBody GetBody(IVolume volume);
    IBody GetBody(ISpread<IVolume, VolumeUnit> spread);
    IBody GetBody(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits);
    //IBody GetBody(IDryBody dryBody);
}
