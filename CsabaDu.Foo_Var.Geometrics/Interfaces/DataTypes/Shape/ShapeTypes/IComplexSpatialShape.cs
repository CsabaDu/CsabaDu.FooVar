using CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

public interface IComplexSpatialShape : IGeometricBody, IDimensions
{
    IEnumerable<ICuboid> InnerTangentCuboidList { get; init; }
    ICuboid Dimensions { get; init; }

    int GetInnerTangentCuboidListCount();
    IEnumerable<IExtent> GetInnerShapeExtentList();
    IComplexSpatialShape GetComplexSpatialShape();
    IComplexSpatialShape GetComplexSpatialShape(ExtentUnit extentUnit);
    IComplexSpatialShape GetComplexSpatialShape(IEnumerable<ICuboid> innerTangentCuboidList, ICuboid? dimensions = null);
    IComplexSpatialShape GetComplexSpatialShape(IEnumerable<IExtent> innerShapeExtentList, IEnumerable<IExtent>? outerShapeExtentList = null);

    void ValidateInnerShapeExtentList(IEnumerable<IExtent> innerShapeExtentList);
    void ValidateShapeExtentLists(IEnumerable<IExtent> innerShapeExtentList, IEnumerable<IExtent>? outerShapeExtentList = null);
    void ValidateCuboids(IEnumerable<ICuboid> innerTangentCuboidList, ICuboid? dimensions = null);
}
