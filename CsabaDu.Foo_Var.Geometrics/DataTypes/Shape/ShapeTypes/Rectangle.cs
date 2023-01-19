using CsabaDu.Foo_Var.Geometrics.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.Foo_Var.Geometrics.DataTypes.Shape.ShapeTypes;

internal sealed class Rectangle : PlaneShape, IRectangle
{
    public Rectangle(IExtent length, IExtent width) : base(ShapeTrait.Plane)
    {
        ValidateShapeExtent(length);
        ValidateShapeExtent(width);

        Length = GetComparedShapeExtent(length, width, Comparison.Greater);
        Width = GetComparedShapeExtent(length, width, Comparison.Less);
        Area = GetRectangleArea(length, width);
    }

    public Rectangle(IEnumerable<IExtent> shapeExtentList) : base(shapeExtentList, ShapeTrait.Plane)
    {
        IExtent length = shapeExtentList.Max()!;
        IExtent width = shapeExtentList.Min()!;

        Length = length;
        Width = width;
        Area = GetRectangleArea(length, width);
    }

    public Rectangle(IRectangle other) : this(other.GetShapeExtentList()) { }

    public override IArea Area { get; init; }
    public IExtent Length { get; init; }
    public IExtent Width { get; init; }

    public IExtent GetComparedShapeExtent(Comparison? comparison)
    {
        _ = comparison ?? throw new ArgumentOutOfRangeException(nameof(comparison), comparison, null);

        IEnumerable<IExtent> shapeExtentList = GetSortedShapeExtentList();
        return comparison switch
        {
            Comparison.Greater => shapeExtentList.First(),
            Comparison.Less => shapeExtentList.Last(),

            _ => throw new ArgumentOutOfRangeException(nameof(comparison), comparison, null),
        };
    }

    public override IExtent GetDiagonal(ExtentUnit extentUnit = ExtentUnit.meter)
    {
        extentUnit.ValidateShapeExtentUnit();

        return GetRectangleDiagonal(Length, Width, extentUnit);
    }

    public IRectangle GetRectangle(params IExtent[] shapeExtents)
    {
        if (shapeExtents == null || shapeExtents.Length == 0) return this;

        ValidateShapeExtentCount(shapeExtents.Length);

        return GetRectangle(shapeExtents[0], shapeExtents[1]);
    }

    public IRectangle GetRectangle(IExtent length, IExtent width)
    {
        return ShapeFactory.GetRectangle(length, width);
    }

    public IRectangle GetRectangle(ExtentUnit extentUnit)
    {
        return (IRectangle)ExchangeTo(extentUnit)!;
    }

    public IRectangle GetRectangle(IPlaneShape planeShape)
    {
        _ = planeShape ?? throw new ArgumentNullException(nameof(planeShape));

        return planeShape.ShapeTraits.HasFlag(ShapeTrait.Round) ?
            (IRectangle)planeShape.GetTangentShape()
            : (IRectangle)planeShape.GetPlaneShape();
    }

    public override IReadOnlyList<IExtent> GetShapeExtentList()
    {
        return new List<IExtent>()
        {
            Length,
            Width,
        };
    }

    public IEnumerable<IExtent> GetSortedShapeExtentList()
    {
        return GetShapeExtentList().OrderByDescending(x => x);
    }

    public IStraightShape GetStraightShape(params IExtent[] shapeExtents) => ShapeFactory.GetSraightShape(shapeExtents);

    public override IShape GetTangentShape(Side shapeSide = Side.Outer)
    {
        IMeasure diagonal = shapeSide switch
        {
            Side.Outer => GetDiagonal(),
            Side.Inner => GetComparedShapeExtent(Comparison.Less),

            _ => throw new ArgumentOutOfRangeException(nameof(shapeSide), shapeSide, null),
        };

        IExtent radius = Length.GetExtent(diagonal.DividedBy(2));

        return ShapeFactory.GetCircle(radius);
    }

    public IStraightShape Rotated()
    {
        IEnumerable<IExtent> shapeExtentList = GetSortedShapeExtentList();

        return GetRectangle(shapeExtentList.ToArray());
    }

    public (IStraightShape, IStraightShape) RotatedWith(IStraightShape other)
    {
        _ = other ?? throw new ArgumentNullException(nameof(other));

        other.ValidateShapeTraits(ShapeTrait.Plane);

        return (Rotated(), other.Rotated());
    }

    private static IExtent GetComparedShapeExtent(IExtent length, IExtent width, Comparison comparison)
    {
        bool isLengthGreaterOrEqual = length.CompareTo(width) >= 0;

        return comparison switch
        {
            Comparison.Greater => isLengthGreaterOrEqual ? length : width,
            Comparison.Less => isLengthGreaterOrEqual ? width : length,

            _ => throw new ArgumentOutOfRangeException(nameof(comparison), comparison, null),
        };
    }
}
