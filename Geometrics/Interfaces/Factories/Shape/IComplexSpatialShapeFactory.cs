using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Factories.Shape;

public interface IComplexSpatialShapeFactory
{
    IComplexSpatialShape GetComplexSpatialShape(IEnumerable<ICuboid> innerTangentCuboidList, ICuboid? dimensions = null);
    IComplexSpatialShape GetComplexSpatialShape(IEnumerable<IExtent> innerShapeExtentList, IEnumerable<IExtent>? outerShapeExtentList = null);
}
