using CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Spread;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

public interface IGeometricBody : IBaseShape, IShape, IBody, IProjection
{
    IExtent GetHeight();
    IGeometricBody GetGeometricBody(ExtentUnit extentUnit);
    IGeometricBody GetGeometricBody(params IExtent[] shapeExtents);
    IGeometricBody GetGeometricBody(IGeometricBody geometricBody);
}