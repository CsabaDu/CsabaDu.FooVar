using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Factories.Shape;

public interface ICylinderFactory
{
    ICylinder GetCylinder(IExtent radius, IExtent height);
    ICylinder GetCylinder(ICircle baseFace, IExtent height);
}
