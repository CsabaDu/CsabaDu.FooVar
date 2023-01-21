using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

public interface ICuboid : ISpatialShape<IRectangle>, IRectangularShape
{
    ICuboid GetCuboid(params IExtent[] shapeExtents);
    ICuboid GetCuboid(IPlaneShape baseShape, IExtent height);
    ICuboid GetCuboid(ExtentUnit extentUnit);
    ICuboid GetCuboid(IGeometricBody geometricBody);
}