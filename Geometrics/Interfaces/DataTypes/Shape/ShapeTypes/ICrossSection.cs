using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

public interface ICrossSection : ISection
{
    ShapeExtentType Perpendicular { get; init; }

    ICuboid GetCrossSectionCuboid(IExtent depth);
    ICrossSection GetCrossSection();
    ICrossSection GetCrossSection(IPlaneShape planeSectionShape, IRectangle cornerPadding, ShapeExtentType perpendicular);
    ICrossSection GetCrossSection(ISection section, ShapeExtentType perpendicular);

    void ValidateCrossSection(ICuboid cuboid, ShapeExtentType perpendicular);
    void ValidatePerpendicular(ShapeExtentType perpendicular);
}
