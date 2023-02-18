using CsabaDu.FooVar.Geometrics.Interfaces.Behaviors.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

public interface ICuboid : ISpatialShape<IRectangle>, IRectangularShape, ISpatialRotation
{
    ICuboid GetCuboid(params IExtent[] shapeExtents);
    ICuboid GetCuboid(IPlaneShape baseFace, IExtent height);
    ICuboid GetCuboid(ExtentUnit extentUnit);
    ICuboid GetCuboid(IGeometricBody geometricBody);
}