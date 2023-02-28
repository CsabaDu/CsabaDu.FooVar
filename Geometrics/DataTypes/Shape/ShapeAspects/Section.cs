using CsabaDu.FooVar.Geometrics.DataTypes.Shape.ShapeTypes;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.FooVar.Geometrics.DataTypes.Shape.ShapeAspects;

internal abstract class Section : PlaneShape, ISection
{
    private protected Section(IPlaneShape planeSectionShape, IRectangle cornerPadding) : base(planeSectionShape?.ShapeTraits ?? throw new ArgumentNullException(nameof(planeSectionShape)))
    {
        CornerPadding = cornerPadding ?? throw new ArgumentNullException(nameof(cornerPadding));
        PlaneSectionShape = planeSectionShape;
        Area = planeSectionShape.Area;
    }

    private protected Section(ISection section) : base(section?.ShapeTraits ?? throw new ArgumentNullException(nameof(section)))
    {
        CornerPadding = section.CornerPadding;
        PlaneSectionShape = section.PlaneSectionShape;
        Area = section.Area;
    }

    public IPlaneShape PlaneSectionShape { get; init; }
    public IRectangle CornerPadding { get; init; }
    public override IArea Area { get; init; }
    public override IEnumerable<IExtent> DimensionsShapeExtentList => PlaneSectionShape.DimensionsShapeExtentList;

    public override IExtent GetDiagonal(ExtentUnit extentUnit = ExtentUnit.meter) => PlaneSectionShape.GetDiagonal(extentUnit);

    public IRectangle GetMinimumFace()
    {
        IMeasure length = CornerPadding.GetShapeExtent(ShapeExtentType.Length);
        length = length.SumWith(GetShapeExtent(ShapeExtentType.Length), SummingMode.Add);

        IMeasure width = CornerPadding.GetShapeExtent(ShapeExtentType.Width);
        width = width.SumWith(GetShapeExtent(ShapeExtentType.Width), SummingMode.Add);

        return new Rectangle((IExtent)length, (IExtent)width);
    }

    public ISection GetSection() => this;

    public override IReadOnlyList<IExtent> GetShapeExtentList() => PlaneSectionShape.GetShapeExtentList();

    public override IShape GetTangentShape(Side shapeSide = Side.Outer) => PlaneSectionShape.GetTangentShape();

    public void ValidateSection(IPlaneShape planeShape)
    {
        _ = planeShape ?? throw new ArgumentNullException(nameof(planeShape));

        IShape minimumFace = GetMinimumFace();

        if (minimumFace.FitsIn(planeShape, LimitType.BeNotGreater) != true) throw new ArgumentOutOfRangeException(nameof(planeShape), "");
    }

    public IDryBody GetCrossSectionBody(IExtent depth)
    {
        return ShapeFactory.GetDryBody(PlaneSectionShape, depth);
    }

    public abstract ISection GetSection(IPlaneShape planeSectionShape, IRectangle cornerPadding);
    public abstract ISection GetSection(ISection section);
}
