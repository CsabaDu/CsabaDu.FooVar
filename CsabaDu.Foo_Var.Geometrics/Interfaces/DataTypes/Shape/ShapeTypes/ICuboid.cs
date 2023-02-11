using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

public interface ICuboid : ISpatialShape<IRectangle>, IRectangularShape
{
    IRectangle GetComparedSide(Comparison? comparison);

    ICuboid GetCuboid(params IExtent[] shapeExtents);
    ICuboid GetCuboid(IPlaneShape bases, IExtent height);
    ICuboid GetCuboid(ExtentUnit extentUnit);
    ICuboid GetCuboid(IGeometricBody geometricBody);
}