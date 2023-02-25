using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Factories.Shape;

public interface IPlaneSectionFactory
{
    IPlaneSection GetPlaneSection(IPlaneShape planeSectionShape, IRectangle cornerPadding);
    IPlaneSection GetPlaneSection(ISection section);
}
