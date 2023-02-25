using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Factories.Shape;

public interface ISectionFactory : IPlaneSectionFactory, ICrossSectionFactory
{
    ISection GetSection(IPlaneShape planeSectionShape, IRectangle cornerPadding, ShapeExtentType? perpendicular = null);
    ISection GetSection(ISection section, ShapeExtentType? perpendicular = null);
}
