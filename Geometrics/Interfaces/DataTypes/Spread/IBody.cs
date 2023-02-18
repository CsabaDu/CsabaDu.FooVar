using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.Factories;

namespace CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;

public interface IBody : ISpread<IVolume, VolumeUnit>
{
    IVolume Volume { get; init; }
    //IBodyFactory BodyFactory { get; init; }

    IBody GetBody(VolumeUnit? volumeUnit = null);
    IBody GetBody(IVolume volume);
    IBody GetBody(ISpread<IVolume, VolumeUnit> spread);
    IBody GetBody(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits);
    IBody GetBody(IShape shape);
}