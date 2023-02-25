using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Factories;

public interface ISpreadFactory : IBodyFactory, ISurfaceFactory
{
    ISpread GetSpread(ISpread spread);
    ISpread GetSpread(IMeasure spreadMeasure);
    ISpread GetSpread(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits);
    ISpread GetSpread(IShape shape);
}
