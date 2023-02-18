using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Factories.Shape;

public interface IGeometricBodyFactory : ICuboidFactory, ICylinderFactory, IComplexSpatialShapeFactory
{
    IGeometricBody GetGeometricBody(params IExtent[] shapeExtents);
    IGeometricBody GetGeometricBody(IGeometricBody geometricBody);
}
