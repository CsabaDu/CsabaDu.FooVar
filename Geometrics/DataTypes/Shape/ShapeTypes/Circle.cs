using CsabaDu.FooVar.Geometrics.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.FooVar.Geometrics.DataTypes.Shape.ShapeTypes;

internal sealed class Circle : PlaneShape, ICircle
{
    private IRectangle Dimensions => (IRectangle)GetDimensions();

    public Circle(IExtent radius) : base(ShapeTrait.Plane | ShapeTrait.Circular)
    {
        ValidateShapeExtent(radius);

        Radius = radius;
        Area = GetCircleArea(radius);
    }

    public Circle(IEnumerable<IExtent> shapeExtentList) : base(shapeExtentList, ShapeTrait.Plane | ShapeTrait.Circular)
    {
        IExtent radius = shapeExtentList.First();

        Radius = radius;
        Area = GetCircleArea(radius);
    }

    public Circle(ICircle other) : this(other?.GetShapeExtentList() ?? throw new ArgumentNullException(nameof(other))) { }

    public override IArea Area { get; init; }
    public IExtent Radius { get; init; }

    public override IEnumerable<IExtent> DimensionsShapeExtentList => Dimensions.GetShapeExtentList();

    public ICircle GetCircle(params IExtent[] shapeExtents)
    {
        if (shapeExtents == null || shapeExtents.Length == 0) return this;

        ValidateShapeExtentCount(shapeExtents.Length);

        return GetCircle(shapeExtents[0]);
    }

    public ICircle GetCircle(ExtentUnit extentUnit)
    {
        return (ICircle)ExchangeTo(extentUnit)!;
    }

    public ICircle GetCircle(IPlaneShape planeShape)
    {
        _ = planeShape ?? throw new ArgumentNullException(nameof(planeShape));

        return planeShape.ShapeTraits.HasFlag(ShapeTrait.Circular) ?
            (ICircle)planeShape.GetPlaneShape()
            : (ICircle)planeShape.GetTangentShape();
    }

    public ICircle GetCircle(IExtent radius)
    {
        return ShapeFactory.GetCircle(radius);
    }

    public override IExtent GetDiagonal(ExtentUnit extentUnit = ExtentUnit.meter)
    {
        extentUnit.ValidateShapeExtentUnit();

        return GetCircleDiagonal(Radius, extentUnit);
    }

    public IRectangularShape GetDimensions()
    {
        IExtent diagonal = GetDiagonal();

        return ShapeFactory.GetRectangle(diagonal, diagonal);
    }

    public ICircularShape GetCircularShape(params IExtent[] shapeExtents) => ShapeFactory.GetCircularShape(shapeExtents);

    public override IReadOnlyList<IExtent> GetShapeExtentList()
    {
        return new List<IExtent>()
        {
            Radius,
        };
    }

    public override IShape GetTangentShape(Side shapeSide = Side.Outer)
    {
        IExtent diagonal = GetDiagonal();

        IExtent edge = shapeSide switch
        {
            Side.Outer => diagonal,
            Side.Inner => GetTangentEdge(diagonal),

            _ => throw new ArgumentOutOfRangeException(nameof(shapeSide), shapeSide, null),
        };

        return ShapeFactory.GetRectangle(edge, edge);
    }
}
