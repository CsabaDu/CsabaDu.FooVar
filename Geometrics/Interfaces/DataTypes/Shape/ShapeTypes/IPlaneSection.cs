using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

public interface IPlaneSection : IPlaneShape
{
    IPlaneShape PlaneSectionShape { get; init; }
    ShapeExtentType PerpendicularShapeExtentType { get; init; }
    IRectangle CornerPadding { get; init; }

    IPlaneSection GetPlaneSection();

    void ValidateSectionArgs(IPlaneShape planeSectionShape, IRectangle cornerPadding, ShapeExtentType perpendicularShapeExtentType);
}
