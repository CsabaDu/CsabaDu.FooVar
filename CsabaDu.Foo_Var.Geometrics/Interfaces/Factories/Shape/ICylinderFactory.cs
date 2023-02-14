using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Factories.Shape;

public interface ICylinderFactory
{
    ICylinder GetCylinder(IExtent radius, IExtent height);
    ICylinder GetCylinder(ICircle baseFace, IExtent height);
}
