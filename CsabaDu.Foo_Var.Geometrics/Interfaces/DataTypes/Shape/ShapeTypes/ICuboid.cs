using CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

public interface ICuboid : ISpatialShape<IRectangle>, IRectangularShape, ISpatialRotation
{
    ICuboid GetCuboid(params IExtent[] shapeExtents);
    ICuboid GetCuboid(IPlaneShape bases, IExtent height);
    ICuboid GetCuboid(ExtentUnit extentUnit);
    ICuboid GetCuboid(IGeometricBody geometricBody);
}