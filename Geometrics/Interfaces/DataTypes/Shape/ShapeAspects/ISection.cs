using CsabaDu.FooVar.Geometrics.Interfaces.Behaviors.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

public interface ISection : IDryBody, IDimensions
{
    IPlaneShape PlaneSectionShape { get; init; }
    ShapeExtentType PerpendicularShapeExtentType { get; init; }
    IRectangle CornerPadding { get; init; }

    ISection GetSection(IPlaneShape planeSectionShape, IRectangle cornerPadding, ShapeExtentType perpendicularShapeExtentType);

    void ValidateSectionArgs(IPlaneShape planeSectionShape, IRectangle cornerPadding, ShapeExtentType perpendicularShapeExtentType);
}
