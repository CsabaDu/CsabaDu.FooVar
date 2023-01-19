using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Factories.Shape;

public interface IGeometricBodyFactory : IBoxFactory, IDrumFactory
{
    IGeometricBody GetGeometricBody(params IExtent[] shapeExtents);
    IGeometricBody GetGeometricBody(IGeometricBody geometricBody);
}
