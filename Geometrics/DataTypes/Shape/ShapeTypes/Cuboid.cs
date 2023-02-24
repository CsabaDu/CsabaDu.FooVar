using CsabaDu.FooVar.Geometrics.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes.MeasureTypes;

namespace CsabaDu.FooVar.Geometrics.DataTypes.Shape.ShapeTypes;

internal sealed class Cuboid : SpatialShape<IRectangle>, ICuboid
{
    public Cuboid(IEnumerable<IExtent> shapeExtentList) : base(shapeExtentList, ShapeTrait.None)
    {
        IExtent length = BaseFace.Length;
        IExtent width = BaseFace.Width;

        Length = length;
        Width = width;
        Volume = GetCuboidVolume(length, width, Height);
    }

    public Cuboid(IRectangle baseFace, IExtent height) : base(baseFace, height, ShapeTrait.None)
    {
        IExtent length = baseFace.Length;
        IExtent width = baseFace.Width;

        Length = length;
        Width= width;
        Volume = GetCuboidVolume(length, width, height);
    }

    public Cuboid(IExtent length, IExtent width, IExtent height) : this(new Rectangle(length, width), height) { }

    public Cuboid(ICuboid other) : this(other?.GetShapeExtentList() ?? throw new ArgumentNullException(nameof(other))) { }

    public IExtent Length { get; init; }
    public IExtent Width { get; init; }
    public override IVolume Volume { get; init; }
    public override IEnumerable<IExtent> DimensionsShapeExtentList => GetShapeExtentList();

    public override IPlaneShape GetBaseFace() => BaseFace;

    public IRectangle GetComparedVerticalFace(Comparison comparison)
    {
        IExtent horizontalExtent = BaseFace.GetComparedShapeExtent(comparison);

        return ShapeFactory.GetRectangle(horizontalExtent, Height);
    }

    public ICuboid GetCuboid(params IExtent[] shapeExtents)
    {
        if (shapeExtents == null || shapeExtents.Length == 0) return this;

        ValidateShapeExtentCount(shapeExtents.Length);

        return ShapeFactory.GetCuboid(shapeExtents[0], shapeExtents[1], shapeExtents[2]); 
    }

    public ICuboid GetCuboid(IPlaneShape baseFace, IExtent height)
    {
        _ = baseFace ?? throw new ArgumentNullException(nameof(baseFace));

        if (baseFace is IRectangle rectangle)
        {
            return ShapeFactory.GetCuboid(rectangle, height);
        }
        
        if (baseFace is ICircle circle)
        {
            ICylinder cylinder = new Cylinder(circle, height);

            return (ICuboid)cylinder.GetTangentShape();
        }

        throw new ArgumentOutOfRangeException(nameof(baseFace), baseFace.GetShapeType(), null);
    }

    public ICuboid GetCuboid(ExtentUnit extentUnit)
    {
        return (ICuboid)ExchangeTo(extentUnit)!;
    }

    public ICuboid GetCuboid(IDryBody dryBody)
    {
        _ = dryBody ?? throw new ArgumentNullException(nameof(dryBody));

        return GetCuboid(dryBody.GetBaseFace(), dryBody.GetHeight());
    }

    public override IExtent GetDiagonal(ExtentUnit extentUnit = ExtentUnit.meter)
    {
        extentUnit.ValidateShapeExtentUnit();

        return GetCuboidDiagonal(Length, Width, Height, extentUnit);
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

    public IRectangularShape GetRectangularShape(params IExtent[] shapeExtents) => GetCuboid(shapeExtents);

    public override IShape GetTangentShape(Side shapeSide = Side.Outer)
    {
        ICircle baseFace = (ICircle)BaseFace.GetTangentShape(shapeSide);

        return ShapeFactory.GetCylinder(baseFace, Height);
    }

    public ICuboid RotatedSpatially()
    {
        IEnumerable<IExtent> shapeExtentList = GetSortedShapeExtentList();

        return GetCuboid(shapeExtentList.ToArray());
    }

    public (ICuboid, ICuboid) RotatedSpatiallyWith(ICuboid other)
    {
        _ = other ?? throw new ArgumentNullException(nameof(other));

        return (RotatedSpatially(), other.RotatedSpatially());
    }

    public IRectangularShape RotatedHorizontally()
    {
        IExtent length = BaseFace.GetComparedShapeExtent(Comparison.Greater);
        IExtent width = BaseFace.GetComparedShapeExtent(Comparison.Less);

        return GetCuboid(length, width, Height);
    }

    public (IRectangularShape, IRectangularShape) RotatedHorizontallyWith(IRectangularShape other)
    {
        _ = other ?? throw new ArgumentNullException(nameof(other));

        other.ValidateShapeTraits(ShapeTrait.None);

        return (RotatedHorizontally(), other.RotatedHorizontally());
    }
}
