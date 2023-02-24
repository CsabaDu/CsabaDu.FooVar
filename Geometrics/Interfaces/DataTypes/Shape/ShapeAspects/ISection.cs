using CsabaDu.FooVar.Geometrics.Interfaces.Behaviors.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

public interface ISection : IPlaneShape
{
    IPlaneShape PlaneSectionShape { get; init; }
    IRectangle CornerPadding { get; init; }

    ISection GetSection();
    ISection GetSection(IPlaneShape planeSectionShape, IRectangle cornerPadding);

    void ValidateSection(IPlaneShape planeShape);
}