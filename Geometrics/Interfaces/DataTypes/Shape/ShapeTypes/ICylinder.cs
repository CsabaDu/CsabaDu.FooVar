using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

public interface ICylinder : ISpatialShape<ICircle>, ICircularShape
{
    ICylinder GetCylinder(params IExtent[] shapeExtents);
    ICylinder GetCylinder(IPlaneShape baseFace, IExtent height);
    ICylinder GetCylinder(ExtentUnit extentUnit);
    ICylinder GetCylinder(IDryBody dryBody);
}