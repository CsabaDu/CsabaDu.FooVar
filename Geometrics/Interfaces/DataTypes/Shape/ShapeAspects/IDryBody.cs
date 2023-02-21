using CsabaDu.FooVar.Geometrics.Interfaces.Behaviors.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;

namespace CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

public interface IDryBody : IBaseFace, IShape, IBody, IProjection
{
    IExtent GetHeight();
    IDryBody GetDryBody(ExtentUnit extentUnit);
    IDryBody GetDryBody(params IExtent[] shapeExtents);
    IDryBody GetDryBody(IDryBody dryBody);
}