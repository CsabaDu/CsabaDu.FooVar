using CsabaDu.Foo_Var.Geometrics.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.Foo_Var.Geometrics.DataTypes.Shape.ShapeTypes;

internal sealed class Cuboid : SpatialShape<IRectangle>, ICuboid
{
    public Cuboid(IEnumerable<IExtent> shapeExtentList) : base(shapeExtentList, ShapeTrait.None)
    {
        IExtent length = Bases.Length;
        IExtent width = Bases.Width;

        Length = length;
        Width = width;
        Volume = GetCuboidVolume(length, width, Height);
    }

    public Cuboid(IRectangle bases, IExtent height) : base(bases, height, ShapeTrait.None)
    {
        IExtent length = bases.Length;
        IExtent width = bases.Width;

        Length = length;
        Width= width;
        Volume = GetCuboidVolume(length, width, height);
    }

    public Cuboid(IExtent length, IExtent width, IExtent height) : this(new Rectangle(length, width), height) { }

    public Cuboid(ICuboid other) : this(other.GetShapeExtentList()) { }

    public IExtent Length { get; init; }
    public IExtent Width { get; init; }
    public override IVolume Volume { get; init; }

    public IRectangle GetComparedFace(Comparison? comparison)
    {
        if (comparison == null) return Bases;

        IExtent horizontalExtent = Bases.GetComparedShapeExtent(comparison);

        return ShapeFactory.GetRectangle(horizontalExtent, Height);
    }

    public ICuboid GetCuboid(params IExtent[] shapeExtents)
    {
        if (shapeExtents == null || shapeExtents.Length == 0) return this;

        ValidateShapeExtentCount(shapeExtents.Length);

        return ShapeFactory.GetCuboid(shapeExtents[0], shapeExtents[1], shapeExtents[2]); 
    }

    public ICuboid GetCuboid(IPlaneShape bases, IExtent height)
    {
        _ = bases ?? throw new ArgumentNullException(nameof(bases));

        if (bases is IRectangle rectangle)
        {
            return ShapeFactory.GetCuboid(rectangle, height);
        }
        
        if (bases is ICircle circle)
        {
            ICylinder cylinder = new Cylinder(circle, height);

            return (ICuboid)cylinder.GetTangentShape();
        }

        throw new ArgumentOutOfRangeException(nameof(bases), bases.GetShapeType(), null);
    }

    public ICuboid GetCuboid(ExtentUnit extentUnit)
    {
        return (ICuboid)ExchangeTo(extentUnit)!;
    }

    public ICuboid GetCuboid(IGeometricBody geometricBody)
    {
        _ = geometricBody ?? throw new ArgumentNullException(nameof(geometricBody));

        return GetCuboid(geometricBody.GetBases(), geometricBody.GetHeight());
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
        ICircle bases = (ICircle)Bases.GetTangentShape(shapeSide);

        return ShapeFactory.GetCylinder(bases, Height);
    }

    public ICuboid RotatedHorizontally()
    {
        IExtent length = Bases.GetComparedShapeExtent(Comparison.Greater);
        IExtent width = Bases.GetComparedShapeExtent(Comparison.Less);

        return GetCuboid(length, width, Height);
    }

    public (ICuboid, ICuboid) RotatedHorizontallyWith(ICuboid other)
    {
        _ = other ?? throw new ArgumentNullException(nameof(other));

        return (RotatedHorizontally(), other.RotatedHorizontally());
    }

    public IRectangularShape Rotated()
    {
        IEnumerable<IExtent> shapeExtentList = GetSortedShapeExtentList();

        return GetCuboid(shapeExtentList.ToArray());
    }

    public (IRectangularShape, IRectangularShape) RotatedWith(IRectangularShape other)
    {
        _ = other ?? throw new ArgumentNullException(nameof(other));

        other.ValidateShapeTraits(ShapeTrait.None);

        return (Rotated(), other.Rotated());
    }
}
