using CsabaDu.Foo_Var.Geometrics.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.Foo_Var.Geometrics.DataTypes.Shape.ShapeTypes;

internal sealed class Box : SpatialShape<IRectangle>, IBox
{
    public Box(IEnumerable<IExtent> shapeExtentList) : base(shapeExtentList, ShapeTrait.None)
    {
        IExtent length = BaseShape.Length;
        IExtent width = BaseShape.Width;

        Length = length;
        Width = width;
        Volume = GetBoxVolume(length, width, Height);
    }

    public Box(IRectangle baseShape, IExtent height) : base(baseShape, height, ShapeTrait.None)
    {
        IExtent length = baseShape.Length;
        IExtent width = baseShape.Width;

        Length = length;
        Width= width;
        Volume = GetBoxVolume(length, width, height);
    }

    public Box(IExtent length, IExtent width, IExtent height) : this(new Rectangle(length, width), height) { }

    public Box(IBox other) : this(other.GetShapeExtentList()) { }

    public IExtent Length { get; init; }
    public IExtent Width { get; init; }
    public override IVolume Volume { get; init; }

    public IBox GetBox(params IExtent[] shapeExtents)
    {
        if (shapeExtents == null || shapeExtents.Length == 0) return this;

        ValidateShapeExtentCount(shapeExtents.Length);

        return ShapeFactory.GetBox(shapeExtents[0], shapeExtents[1], shapeExtents[2]); 
    }

    public IBox GetBox(IPlaneShape baseShape, IExtent height)
    {
        _ = baseShape ?? throw new ArgumentNullException(nameof(baseShape));

        if (baseShape is IRectangle rectangle)
        {
            return ShapeFactory.GetBox(rectangle, height);
        }
        
        if (baseShape is ICircle circle)
        {
            IDrum drum = new Drum(circle, height);

            return (IBox)drum.GetTangentShape();
        }

        throw new ArgumentOutOfRangeException(nameof(baseShape), baseShape.GetShapeType(), null);
    }

    public IBox GetBox(ExtentUnit extentUnit)
    {
        return (IBox)ExchangeTo(extentUnit)!;
    }

    public IBox GetBox(IGeometricBody geometricBody)
    {
        _ = geometricBody ?? throw new ArgumentNullException(nameof(geometricBody));

        return GetBox(geometricBody.GetBaseShape(), geometricBody.Height);
    }

    public override IExtent GetDiagonal(ExtentUnit extentUnit = ExtentUnit.meter)
    {
        extentUnit.ValidateShapeExtentUnit();

        return GetBoxDiagonal(Length, Width, Height, extentUnit);
    }

    public IExtent GetComparedShapeExtent(Comparison? comparison)
    {
        IEnumerable<IExtent> shapeExtentList = GetSortedShapeExtentList();

        if (comparison == null) return shapeExtentList.ElementAt(1);

        return comparison switch
        {
            Comparison.Greater => shapeExtentList.First(),
            Comparison.Less => shapeExtentList.Last(),

            _ => throw new ArgumentOutOfRangeException(nameof(comparison), comparison, null),
        };
    }

    public override IReadOnlyList<IExtent> GetShapeExtentList()
    {
        return new List<IExtent>()
        {
            Length,
            Width,
            Height,
        };
    }

    public IEnumerable<IExtent> GetSortedShapeExtentList()
    {
        return GetShapeExtentList().OrderByDescending(x => x);
    }

    public IStraightShape GetStraightShape(params IExtent[] shapeExtents) => GetBox(shapeExtents);

    public override IShape GetTangentShape(Side shapeSide = Side.Outer)
    {
        ICircle baseShape = (ICircle)BaseShape.GetTangentShape(shapeSide);

        return ShapeFactory.GetDrum(baseShape, Height);
    }

    public IStraightShape Rotated()
    {
        IEnumerable<IExtent> shapeExtentList = GetSortedShapeExtentList();

        return GetBox(shapeExtentList.ToArray());
    }

    public (IStraightShape, IStraightShape) RotatedWith(IStraightShape other)
    {
        _ = other ?? throw new ArgumentNullException(nameof(other));

        other.ValidateShapeTraits(ShapeTrait.None);

        return (Rotated(), other.Rotated());
    }
}
