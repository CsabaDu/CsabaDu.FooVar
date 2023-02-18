using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Behaviors.Shape;

public interface IBaseFace
{
    IPlaneShape GetBaseFace(params IExtent[] shapeExtents);
    IPlaneShape GetBaseFace(ExtentUnit extentUnit);
    IPlaneShape GetBaseFace(IEnumerable<IExtent> shapeExtentList);
}