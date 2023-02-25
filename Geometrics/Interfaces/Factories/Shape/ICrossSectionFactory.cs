using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Factories.Shape;

public interface ICrossSectionFactory
{
    ICrossSection GetCrossSection(IPlaneShape planeSectionShape, IRectangle cornerPadding, ShapeExtentType perpendicular);
    ICrossSection GetCrossSection(ISection section, ShapeExtentType perpendicular);
}
