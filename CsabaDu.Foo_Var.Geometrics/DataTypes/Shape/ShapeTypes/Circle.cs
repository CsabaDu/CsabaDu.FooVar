using CsabaDu.Foo_Var.Geometrics.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.Foo_Var.Geometrics.DataTypes.Shape.ShapeTypes;

internal sealed class Circle : PlaneShape, ICircle
{
    public Circle(IExtent radius) : base(ShapeTrait.Plane | ShapeTrait.Round)
    {
        ValidateShapeExtent(radius);

        Radius = radius;
        Area = GetCircleArea(radius);
    }

    public Circle(IEnumerable<IExtent> shapeExtentList) : base(shapeExtentList, ShapeTrait.Plane | ShapeTrait.Round)
    {
        IExtent radius = shapeExtentList.First();

        Radius = radius;
        Area = GetCircleArea(radius);
    }

    public Circle(ICircle other) : this(other.GetShapeExtentList()) { }

    public override IArea Area { get; init; }
    public IExtent Radius { get; init; }

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

        return planeShape.ShapeTraits.HasFlag(ShapeTrait.Round) ?
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

    public IStraightShape GetDimensions()
    {
        IExtent diagonal = GetDiagonal();

        return ShapeFactory.GetRectangle(diagonal, diagonal);
    }

    public IRoundShape GetRoundShape(params IExtent[] shapeExtents) => ShapeFactory.GetRoundShape(shapeExtents);

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
            Side.Inner => GetEdge(diagonal),

            _ => throw new ArgumentOutOfRangeException(nameof(shapeSide), shapeSide, null),
        };

        return ShapeFactory.GetRectangle(edge, edge);
    }
}
