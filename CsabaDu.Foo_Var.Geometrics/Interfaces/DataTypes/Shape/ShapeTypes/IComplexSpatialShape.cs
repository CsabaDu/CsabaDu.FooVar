using CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

public interface IComplexSpatialShape : IGeometricBody, IDimensions
{
    IEnumerable<ICuboid> InnerTangentCuboids { get; init; }
    ICuboid Dimensions { get; init; }

    int GetInnerTangentCuboidsCount();
    IEnumerable<IExtent> GetInnerShapeExtentList();
    IComplexSpatialShape GetComplexSpatialShape(params IExtent[] shapeExtents);
    IComplexSpatialShape GetComplexSpatialShape(ExtentUnit extentUnit);
    IComplexSpatialShape GetComplexSpatialShape(IEnumerable<ICuboid> innerTangentCuboids, ICuboid? dimensions = null);
    IComplexSpatialShape GetComplexSpatialShape(IEnumerable<IExtent> innerShapeExtentList, IEnumerable<IExtent>? shapeExtentList = null);

    void ValidateInnerShapeExtentList(IEnumerable<IExtent> innerShapeExtentList);
    void ValidateShapeExtentLists(IEnumerable<IExtent> innerShapeExtentList, IEnumerable<IExtent>? outerShapeExtentList = null);
    void ValidateCuboids(IEnumerable<ICuboid> innerTangentCuboids, ICuboid? dimensions = null);
}
