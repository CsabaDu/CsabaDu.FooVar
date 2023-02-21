using CsabaDu.FooVar.Geometrics.Interfaces.Behaviors.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

public interface IComplexDryBody : IDryBody, IDimensions
{
    IEnumerable<ICuboid> InnerTangentCuboidList { get; init; }
    int InnerTangentCuboidCount { get; init; }
    ICuboid Dimensions { get; init; }

    int GetInnerTangentCuboidListCount();
    IEnumerable<IExtent> GetInnerShapeExtentList();
    IComplexDryBody GetComplexDryBody();
    IComplexDryBody GetComplexDryBody(ExtentUnit extentUnit);
    IComplexDryBody GetComplexDryBody(IEnumerable<ICuboid> innerTangentCuboidList, ICuboid? dimensions = null);
    IComplexDryBody GetComplexDryBody(IEnumerable<IExtent> innerShapeExtentList, IEnumerable<IExtent>? outerShapeExtentList = null);

    void ValidateInnerShapeExtentList(IEnumerable<IExtent> innerShapeExtentList);
    void ValidateShapeExtentLists(IEnumerable<IExtent> innerShapeExtentList, IEnumerable<IExtent>? outerShapeExtentList = null);
    void ValidateCuboids(IEnumerable<ICuboid> innerTangentCuboidList, ICuboid? dimensions = null);
}
