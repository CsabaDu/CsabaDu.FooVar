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

        if (minimumFace.FitsIn(planeShape, LimitType.BeLess) != true) throw new ArgumentOutOfRangeException(nameof(planeShape), "");
    }

    public abstract ISection GetSection(IPlaneShape planeSectionShape, IRectangle cornerPadding);
    public abstract ISection GetSection(ISection section);
}

//internal abstract class Section : DryBody, ISection
//{
//    private protected Section(IPlaneShape planeSectionShape, IRectangle cornerPadding, ShapeExtentType perpendicular) : base(planeSectionShape?.ShapeTraits - (int)ShapeTrait.Plane ?? throw new ArgumentNullException(nameof(planeSectionShape)))
//    {
//        ValidateSectionArgs(planeSectionShape!, cornerPadding, perpendicular);

//        CornerPadding = cornerPadding;
//        Perpendicular = perpendicular;
//        PlaneSectionShape = planeSectionShape!;
//    }

//    public IPlaneShape PlaneSectionShape { get; init; }
//    public ShapeExtentType Perpendicular { get; init; }
//    public IRectangle CornerPadding { get; init; }

//    public abstract IRectangularShape GetDimensions();

//    public override sealed IExtent GetHeight() => GetShapeExtent(Perpendicular);

//    public override IPlaneShape GetProjection(ShapeExtentType perpendicular)
//    {
//        if (ShapeTraits.HasFlag(ShapeTrait.Circular))
//        {
//            return (this as ICylinder)?.GetProjection(perpendicular) ?? throw new Exception(); // TODO ?
//        }
//        throw new NotImplementedException();
//    }

//    public abstract ISection GetSection(IPlaneShape planeSectionShape, IRectangle cornerPadding, ShapeExtentType perpendicular);

//    public override IReadOnlyList<IExtent> GetShapeExtentList()
//    {
//        throw new NotImplementedException();
//    }

//    public override IShape GetTangentShape(Side shapeSide = Side.Outer)
//    {
//        throw new NotImplementedException();
//    }

//    public void ValidateSectionArgs(IPlaneShape planeSectionShape, IRectangle cornerPadding, ShapeExtentType perpendicular)
//    {
//        _ = planeSectionShape ?? throw new ArgumentNullException(nameof(planeSectionShape));
//        _ = cornerPadding ?? throw new ArgumentNullException(nameof(cornerPadding));

//        if (!GetShapeExtentTypeSet(ShapeTraits).Contains(perpendicular))
//        {
//            throw new ArgumentOutOfRangeException(nameof(perpendicular), perpendicular, null);
//        }

//        IPlaneShape projection = GetProjection(perpendicular);

//        IRectangle rectangularProjection = GetRectangularTangentPlaneShape(projection, Side.Inner);
//        IRectangle planeSectionRectangle = GetRectangularTangentPlaneShape(PlaneSectionShape, Side.Outer);

//        IRectangle minimumRectangle = GetMinimumRectangle(rectangularProjection, planeSectionRectangle);

//        if ((minimumRectangle as IShape)?.FitsIn(projection, LimitType.BeNotGreater) != true)
//        {
//            throw new ArgumentOutOfRangeException(nameof(planeSectionShape), "");
//        }
//    }

//    private static IRectangle GetRectangularTangentPlaneShape(IPlaneShape planeShape, Side side)
//    {
//        return planeShape.ShapeTraits.HasFlag(ShapeTrait.Circular) ?
//            (IRectangle)planeShape.GetTangentShape(side)
//            : (IRectangle)planeShape;
//    }

//    private static IExtent GetMinimumShapeExtent(IRectangle cornerPadding, IRectangle planeSectionRectangle, ShapeExtentType shapeExtentType)
//    {
//        IMeasure cornerPaddingExtent = cornerPadding.GetShapeExtent(shapeExtentType);
//        IMeasure planeSectionShapeExtent = planeSectionRectangle.GetShapeExtent(shapeExtentType);

//        return (IExtent)cornerPaddingExtent.SumWith(planeSectionShapeExtent);
//    }

//    private IRectangle GetMinimumRectangle(IRectangle cornerPadding, IRectangle planeSectionShape)
//    {
//        IExtent Length = GetMinimumShapeExtent(cornerPadding, planeSectionShape, ShapeExtentType.Length);
//        IExtent Width = GetMinimumShapeExtent(cornerPadding, planeSectionShape, ShapeExtentType.Width);

//        return ShapeFactory.GetRectangle(Length, Width);
//    }
//}
