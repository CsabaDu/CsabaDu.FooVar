using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Factories.Shape;

public interface IPlaneShapeFactory : IRectangleFactory, ICircleFactory, ISectionFactory
{
    IPlaneShape GetPlaneShape(params IExtent[] shapeExtents);
    IPlaneShape GetPlaneShape(IPlaneShape planeShape, IRectangle? cornerPadding = null, ShapeExtentType? perpendicular = null);
}
