using CsabaDu.FooVar.Geometrics.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.FooVar.Geometrics.DataTypes.Shape.ShapeTypes;

internal sealed class Rectangle : PlaneShape, IRectangle
{
    public Rectangle(IExtent length, IExtent width) : base(ShapeTrait.Plane)
    {
        ValidateShapeExtent(length);
        ValidateShapeExtent(width);

        Length = length;
        Width = width;
        Area = GetRectangleArea(length, width);
    }

    public Rectangle(IEnumerable<IExtent> shapeExtentList) : base(shapeExtentList, ShapeTrait.Plane)
    {
        IExtent length = shapeExtentList.First()!;
        IExtent width = shapeExtentList.Last()!;

        Length = length;
        Width = width;
        Area = GetRectangleArea(length, width);
    }

    public Rectangle(IRectangle other) : this(other.GetShapeExtentList()) { }

    public override IArea Area { get; init; }
    public IExtent Length { get; init; }
    public IExtent Width { get; init; }

    public override IEnumerable<IExtent> DimensionsShapeExtentList => GetShapeExtentList();

    public IExtent GetComparedShapeExtent(Comparison? comparison)
    {
        return CalculateGeometrics.GetComparedShapeExtent(Length, Width, comparison);
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

        return planeShape.ShapeTraits.HasFlag(ShapeTrait.Circular) ?
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

    public IRectangularShape GetRectangularShape(params IExtent[] shapeExtents) => ShapeFactory.GetRectangularShape(shapeExtents);

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

    public IRectangularShape RotatedHorizontally()
    {
        IEnumerable<IExtent> shapeExtentList = GetSortedShapeExtentList();

        return GetRectangle(shapeExtentList.ToArray());
    }

    public (IRectangularShape, IRectangularShape) RotatedHorizontallyWith(IRectangularShape other)
    {
        _ = other ?? throw new ArgumentNullException(nameof(other));

        other.ValidateShapeTraits(ShapeTrait.Plane);

        return (RotatedHorizontally(), other.RotatedHorizontally());
    }
}
