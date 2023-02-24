using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

public interface IPlaneSection : ISection
{
    IPlaneSection GetPlaneSection();
    IPlaneSection GetPlaneSection(IPlaneShape planeSectionShape, IRectangle cornerPadding);

    void ValidatePlaneSection(IPlaneShape planeShape);
}
