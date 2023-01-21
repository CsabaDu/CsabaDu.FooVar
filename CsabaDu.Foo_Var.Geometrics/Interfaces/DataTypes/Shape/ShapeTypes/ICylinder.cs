using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

public interface ICylinder : ISpatialShape<ICircle>, ICircularShape
{
    ICylinder GetCylinder(params IExtent[] shapeExtents);
    ICylinder GetCylinder(IPlaneShape baseShape, IExtent height);
    ICylinder GetCylinder(ExtentUnit extentUnit);
    ICylinder GetCylinder(IGeometricBody geometricBody);
}