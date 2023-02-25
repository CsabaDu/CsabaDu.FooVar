using CsabaDu.FooVar.Geometrics.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.DataTypes.Shape.ShapeTypes;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace Geometrics.DataTypes.Shape.ShapeTypes;

internal sealed class CrossSection : Section, ICrossSection
{
    public CrossSection(IPlaneShape planeSectionShape, IRectangle cornerPadding, ShapeExtentType perpendicular) : base(planeSectionShape, cornerPadding)
    {
        ValidatePerpendicular(perpendicular);

        Perpendicular = perpendicular;
    }

    public CrossSection(ISection section, ShapeExtentType perpendicular) : base(section)
    {
        ValidatePerpendicular(perpendicular);

        Perpendicular = perpendicular;
    }

    public ShapeExtentType Perpendicular { get; init; }

    public ICrossSection GetCrossSection() => this;

    public ICrossSection GetCrossSection(IPlaneShape planeSectionShape, IRectangle cornerPadding, ShapeExtentType perpendicular)
    {
        return new CrossSection(planeSectionShape, cornerPadding, perpendicular);
    }

    public ICrossSection GetCrossSection(ISection section, ShapeExtentType perpendicular)
    {
        return new CrossSection(section, perpendicular);
    }

    public ICuboid GetCrossSectionBodyDimensions(IExtent depth)
    {
        ValidateShapeExtent(depth);

        IExtent length = GetShapeExtent(ShapeExtentType.Length);
        IExtent width = GetShapeExtent(ShapeExtentType.Width);

        return Perpendicular switch
        {
            ShapeExtentType.Radius => new Cuboid(length, depth, width),
            ShapeExtentType.Length => new Cuboid(width, depth, length),
            ShapeExtentType.Width => new Cuboid(length, depth, width),
            ShapeExtentType.Height => new Cuboid(length, width, depth),
            _ => throw new NotImplementedException(),
        };
    }

    public override ISection GetSection(IPlaneShape planeSectionShape, IRectangle cornerPadding) => GetCrossSection(planeSectionShape, cornerPadding, Perpendicular);

    public override ISection GetSection(ISection section) => GetCrossSection(section, Perpendicular);

    public void ValidateCrossSection(ICuboid cuboid, ShapeExtentType perpendicular)
    {
        _ = cuboid ?? throw new ArgumentNullException(nameof(cuboid));

        if (cuboid.ShapeExtentTypeSet.Contains(perpendicular)) return;

        throw new ArgumentOutOfRangeException(nameof(perpendicular), perpendicular, null);
    }

    public void ValidatePerpendicular(ShapeExtentType perpendicular)
    {
        if (IsValidPerpendicular(perpendicular)) return;

        throw new ArgumentOutOfRangeException(nameof(perpendicular), perpendicular, null);
    }

    private bool IsValidPerpendicular(ShapeExtentType perpendicular)
    {
        return perpendicular switch
        {
            ShapeExtentType.Radius => ShapeTraits.HasFlag(ShapeTrait.Circular),
            ShapeExtentType.Length => !ShapeTraits.HasFlag(ShapeTrait.Circular),
            ShapeExtentType.Width => !ShapeTraits.HasFlag(ShapeTrait.Circular),
            ShapeExtentType.Height => !ShapeTraits.HasFlag(ShapeTrait.Plane),

            _ => false,
        };
    }
}
