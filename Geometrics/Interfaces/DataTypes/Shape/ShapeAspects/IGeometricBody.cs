using CsabaDu.FooVar.Geometrics.Interfaces.Behaviors.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;

namespace CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

public interface IGeometricBody : IBaseFace, IShape, IBody, IProjection
{
    IExtent GetHeight();
    IGeometricBody GetGeometricBody(ExtentUnit extentUnit);
    IGeometricBody GetGeometricBody(params IExtent[] shapeExtents);
    IGeometricBody GetGeometricBody(IGeometricBody geometricBody);
}