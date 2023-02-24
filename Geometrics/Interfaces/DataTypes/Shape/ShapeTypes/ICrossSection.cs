using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

public interface ICrossSection : ISection
{
    ShapeExtentType PerpendicularShapeExtentType { get; init; }

    IDryBody GetCrossSectionBody(IDryBody dryBody);
    ICrossSection GetCrossSection();
    ICrossSection GetCrossSection(IPlaneShape planeSectionShape, IRectangle cornerPadding, ShapeExtentType perpendicularShapeExtentType);
    ICrossSection GetCrossSection(IPlaneSection planeSection, ShapeExtentType perpendicularShapeExtentType);

    void ValidateCrossSection(IDryBody dryBody);
}
