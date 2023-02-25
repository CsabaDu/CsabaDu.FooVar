using CsabaDu.FooVar.Geometrics.Interfaces.Behaviors.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

public interface ISection : IPlaneShape
{
    IPlaneShape PlaneSectionShape { get; init; }
    IRectangle CornerPadding { get; init; }

    IRectangle GetMinimumFace();
    IDryBody GetCrossSectionBody(IExtent depth);
    ISection GetSection();
    ISection GetSection(IPlaneShape planeSectionShape, IRectangle cornerPadding);
    ISection GetSection(ISection section);

    void ValidateSection(IPlaneShape planeShape);
}