using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Factories.Shape;

public interface IComplexDryBodyFactory
{
    IComplexDryBody GetComplexDryBody(IEnumerable<ICuboid> innerTangentCuboidList, ICuboid? dimensions = null);
    IComplexDryBody GetComplexDryBody(IEnumerable<IExtent> innerShapeExtentList, IEnumerable<IExtent>? outerShapeExtentList = null);
}
