using CsabaDu.Foo_Var.Geometrics.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.Foo_Var.Geometrics.DataTypes.Shape.ShapeTypes;

internal sealed class Drum : SpatialShape<ICircle>, IDrum
{
    public Drum(IEnumerable<IExtent> shapeExtentList) : base(shapeExtentList, ShapeTrait.Round)
    {
        IExtent radius = BaseShape.Radius;

        Radius = radius;
        Volume = GetDrumVolume(radius, Height);
    }

    public Drum(ICircle baseShape, IExtent height) : base(baseShape, height, ShapeTrait.Round)
    {
        IExtent radius = baseShape.Radius;

        Radius = radius;
        Volume = GetDrumVolume(radius, height);
    }

    public Drum(IExtent radius, IExtent height) : this(new Circle(radius), height) { }

    public Drum(IDrum other) : this(other.GetShapeExtentList()) { }

    public IExtent Radius { get; init; }
    public override IVolume Volume { get; init; }

    public override IExtent GetDiagonal(ExtentUnit extentUnit = ExtentUnit.meter)
    {
        extentUnit.ValidateShapeExtentUnit();

        return GetDrumDiagonal(Radius, Height, extentUnit);
    }

    public IStraightShape GetDimensions()
    {
        IRectangle baseShape = (IRectangle)BaseShape.GetDimensions();

        return ShapeFactory.GetBox(baseShape, Height);
    }

    public IDrum GetDrum(params IExtent[] shapeExtents)
    {
        if (shapeExtents == null || shapeExtents.Length == 0) return this;

        ValidateShapeExtentCount(shapeExtents.Length);

        return ShapeFactory.GetDrum(shapeExtents[0], shapeExtents[1]);
    }

    public IDrum GetDrum(IPlaneShape baseShape, IExtent height)
    {
        _ = baseShape ?? throw new ArgumentNullException(nameof(baseShape));

        if (baseShape is ICircle circle)
        {
            return GetDrum(circle, height);
        }

        if (baseShape is IRectangle rectangle)
        {
            IBox box = ShapeFactory.GetBox(rectangle, height);

            return (IDrum)box.GetTangentShape();
        }

        throw new ArgumentOutOfRangeException(nameof(baseShape), baseShape.GetShapeType(), null);
    }

    public IDrum GetDrum(ExtentUnit extentUnit)
    {
        return (IDrum)ExchangeTo(extentUnit)!;
    }

    public IDrum GetDrum(IGeometricBody geometricBody)
    {
        _ = geometricBody ?? throw new ArgumentNullException(nameof(geometricBody));

        return GetDrum(geometricBody.GetBaseShape(), geometricBody.Height);
    }

    public IRoundShape GetRoundShape(params IExtent[] shapeExtents) => ShapeFactory.GetRoundShape(shapeExtents);

    public override IReadOnlyList<IExtent> GetShapeExtentList()
    {
        return new List<IExtent>()
        {
            Radius,
            Height,
        };
    }

    public override IShape GetTangentShape(Side shapeSide = Side.Outer)
    {
        IRectangle baseShape = (IRectangle)BaseShape.GetTangentShape(shapeSide);

        return ShapeFactory.GetBox(baseShape, Height);
    }
}
