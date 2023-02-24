using CsabaDu.FooVar.Geometrics.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace Geometrics.DataTypes.Shape.ShapeTypes
{
    //internal sealed class PlaneSection : PlaneShape, IPlaneSection
    //{
    //    public PlaneSection(IPlaneShape planeSectionShape, IRectangle cornerPadding) : base(planeSectionShape?.ShapeTraits ?? throw new ArgumentNullException(nameof(planeSectionShape)))
    //    {
    //        CornerPadding = cornerPadding ?? throw new ArgumentNullException(nameof(cornerPadding));
    //        PlaneSectionShape = planeSectionShape;
    //        Area = planeSectionShape.Area;
    //    }

    //    public override IEnumerable<IExtent> DimensionsShapeExtentList => throw new NotImplementedException();

    //    public override IArea Area { get; init; }
    //    public IPlaneShape PlaneSectionShape { get; init; }
    //    public IRectangle CornerPadding { get; init; }

    //    public override IExtent GetDiagonal(ExtentUnit extentUnit = ExtentUnit.meter) => PlaneSectionShape.GetDiagonal();

    //    public IPlaneSection GetPlaneSection() => this;

    //    public override IReadOnlyList<IExtent> GetShapeExtentList() => PlaneSectionShape.GetShapeExtentList();

    //    public override IShape GetTangentShape(Side shapeSide = Side.Outer) => PlaneSectionShape.GetTangentShape(shapeSide);

    //    public void ValidatePlaneSection(IPlaneShape planeShape)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //internal sealed class CrossSection : DryBody, ICrossSection
    //{
    //    private readonly DryBody _crossSectionBody;

    //    public CrossSection(IPlaneSection planeSection, ShapeExtentType perpendicularShapeExtentType) : base((planeSection?.ShapeTraits ?? throw new ArgumentNullException(nameof(planeSection))) - (int)ShapeTrait.Plane)
    //    {
    //        ValidateShapeExtentType(perpendicularShapeExtentType);

    //        IPlaneShape planeSectionShape = planeSection.PlaneSectionShape;

    //    }

    //    public override IVolume Volume { get; init; }
    //    public IPlaneSection PlaneSection { get; init; }
    //    public ShapeExtentType PerpendicularShapeExtentType { get; init; }
    //    public override IEnumerable<IExtent> DimensionsShapeExtentList => GetDimensionsShapeExtentList();

    //    public override IPlaneShape GetBaseFace() => _crossSectionBody.GetBaseFace();

    //    public ICrossSection GetCrossSection() => this;

    //    public IDryBody GetCrossSectionBody() => _crossSectionBody;

    //    public override IExtent GetDiagonal(ExtentUnit extentUnit = ExtentUnit.meter) => _crossSectionBody.GetDiagonal(extentUnit);

    //    public override IExtent GetHeight() => _crossSectionBody.GetHeight();

    //    public override IPlaneShape GetProjection(ShapeExtentType perpendicularShapeExtentType) => _crossSectionBody.GetProjection(perpendicularShapeExtentType);

    //    public override IReadOnlyList<IExtent> GetShapeExtentList() => _crossSectionBody.GetShapeExtentList();

    //    public override IShape GetTangentShape(Side shapeSide = Side.Outer) => _crossSectionBody.GetTangentShape(shapeSide);

    //    public void ValidateCrossSection(IDryBody dryBody)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    private IEnumerable<IExtent> GetDimensionsShapeExtentList()
    //    {
    //        return ShapeTraits.HasFlag(ShapeTrait.Circular) ?
    //            (_crossSectionBody as ICylinder)!.GetDimensions().GetShapeExtentList()
    //            : GetShapeExtentList();
    //    }
    //}
}
