using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Behaviors.Shape
{
    public interface IProjection
    {
        IPlaneShape GetProjection(ShapeExtentType perpendicularShapeExtentType);
    }

    public interface IProjection<out T> : IProjection where T : IPlaneShape
    {
        T GetHorizontalProjection();
        IRectangle GetVerticalProjection(Comparison? comparison = null);
    }

    public interface ISection : IDryBody
    {
        IPlaneShape PlaneSectionShape { get; init; }
        ShapeExtentType PerpendicularShapeExtentType { get; init; }
        IRectangle CornerPadding { get; init; }

        ISection GetSection(IPlaneShape planeSectionShape, IRectangle cornerPadding, ShapeExtentType perpendicularShapeExtentType);
    }

    public interface IPlaneSection : ISection
    {
        IPlaneSection GetPlaneSection();
    }

    public interface ICrossSection : ISection
    {
        IDryBody GetCrossSectionBody();
        ICrossSection GetCrossSection();
    }
}
