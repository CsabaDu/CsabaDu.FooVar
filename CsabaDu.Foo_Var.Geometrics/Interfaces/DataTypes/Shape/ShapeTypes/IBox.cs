using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

public interface IBox : ISpatialShape<IRectangle>, IStraightShape
{
    IBox GetBox(params IExtent[] shapeExtents);
    IBox GetBox(IPlaneShape baseShape, IExtent height);
    IBox GetBox(ExtentUnit extentUnit);
    IBox GetBox(IGeometricBody geometricBody);
}