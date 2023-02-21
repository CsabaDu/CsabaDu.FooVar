using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Factories.Shape;

public interface IDryBodyFactory : ICuboidFactory, ICylinderFactory, IComplexDryBodyFactory
{
    IDryBody GetDryBody(params IExtent[] shapeExtents);
    IDryBody GetDryBody(IDryBody dryBody);
}
