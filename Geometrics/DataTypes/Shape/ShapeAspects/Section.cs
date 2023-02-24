using CsabaDu.FooVar.Geometrics.Factories;
using CsabaDu.FooVar.Geometrics.Interfaces.Behaviors.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.FooVar.Geometrics.DataTypes.Shape.ShapeAspects;

internal abstract class Section : DryBody, ISection
{
    private protected Section(IPlaneShape planeSectionShape, IRectangle cornerPadding, ShapeExtentType perpendicularShapeExtentType) : base(planeSectionShape?.ShapeTraits - (int)ShapeTrait.Plane ?? throw new ArgumentNullException(nameof(planeSectionShape)))
    {
        ValidateSectionArgs(planeSectionShape!, cornerPadding, perpendicularShapeExtentType);

        CornerPadding = cornerPadding;
        PerpendicularShapeExtentType = perpendicularShapeExtentType;
        PlaneSectionShape = planeSectionShape!;
    }

    public IPlaneShape PlaneSectionShape { get; init; }
    public ShapeExtentType PerpendicularShapeExtentType { get; init; }
    public IRectangle CornerPadding { get; init; }

    public abstract IRectangularShape GetDimensions();

    public override sealed IExtent GetHeight() => GetShapeExtent(PerpendicularShapeExtentType);

    public override IPlaneShape GetProjection(ShapeExtentType perpendicularShapeExtentType)
    {
        if (ShapeTraits.HasFlag(ShapeTrait.Circular))
        {
            return (this as ICylinder)?.GetProjection(perpendicularShapeExtentType) ?? throw new Exception(); // TODO ?
        }
        throw new NotImplementedException();
    }

    public abstract ISection GetSection(IPlaneShape planeSectionShape, IRectangle cornerPadding, ShapeExtentType perpendicularShapeExtentType);

    public override IReadOnlyList<IExtent> GetShapeExtentList()
    {
        throw new NotImplementedException();
    }

    public override IShape GetTangentShape(Side shapeSide = Side.Outer)
    {
        throw new NotImplementedException();
    }

    public void ValidateSectionArgs(IPlaneShape planeSectionShape, IRectangle cornerPadding, ShapeExtentType perpendicularShapeExtentType)
    {
        _ = planeSectionShape ?? throw new ArgumentNullException(nameof(planeSectionShape));
        _ = cornerPadding ?? throw new ArgumentNullException(nameof(cornerPadding));

        if (!GetShapeExtentTypeSet(ShapeTraits).Contains(perpendicularShapeExtentType))
        {
            throw new ArgumentOutOfRangeException(nameof(perpendicularShapeExtentType), perpendicularShapeExtentType, null);
        }

        IPlaneShape projection = GetProjection(perpendicularShapeExtentType);

        IRectangle rectangularProjection = GetRectangularTangentPlaneShape(projection, Side.Inner);
        IRectangle planeSectionRectangle = GetRectangularTangentPlaneShape(PlaneSectionShape, Side.Outer);

        IRectangle minimumRectangle = GetMinimumRectangle(rectangularProjection, planeSectionRectangle);

        if ((minimumRectangle as IShape)?.FitsIn(projection, LimitType.BeNotGreater) != true)
        {
            throw new ArgumentOutOfRangeException(nameof(planeSectionShape), "");
        }
    }

    private static IRectangle GetRectangularTangentPlaneShape(IPlaneShape planeShape, Side side)
    {
        return planeShape.ShapeTraits.HasFlag(ShapeTrait.Circular) ?
            (IRectangle)planeShape.GetTangentShape(side)
            : (IRectangle)planeShape;
    }

    private static IExtent GetMinimumShapeExtent(IRectangle cornerPadding, IRectangle planeSectionRectangle, ShapeExtentType shapeExtentType)
    {
        IMeasure cornerPaddingExtent = cornerPadding.GetShapeExtent(shapeExtentType);
        IMeasure planeSectionShapeExtent = planeSectionRectangle.GetShapeExtent(shapeExtentType);

        return (IExtent)cornerPaddingExtent.SumWith(planeSectionShapeExtent);
    }

    private IRectangle GetMinimumRectangle(IRectangle cornerPadding, IRectangle planeSectionShape)
    {
        IExtent Length = GetMinimumShapeExtent(cornerPadding, planeSectionShape, ShapeExtentType.Length);
        IExtent Width = GetMinimumShapeExtent(cornerPadding, planeSectionShape, ShapeExtentType.Width);

        return ShapeFactory.GetRectangle(Length, Width);
    }
}
