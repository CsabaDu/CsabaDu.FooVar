using CsabaDu.Foo_Var.Geometrics.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.Foo_Var.Geometrics.DataTypes.Shape.ShapeTypes;

internal sealed class Cylinder : SpatialShape<ICircle>, ICylinder
{
    public Cylinder(IEnumerable<IExtent> shapeExtentList) : base(shapeExtentList, ShapeTrait.Circular)
    {
        IExtent radius = Bases.Radius;

        Radius = radius;
        Volume = GetCylinderVolume(radius, Height);
    }

    public Cylinder(ICircle bases, IExtent height) : base(bases, height, ShapeTrait.Circular)
    {
        IExtent radius = bases.Radius;

        Radius = radius;
        Volume = GetCylinderVolume(radius, height);
    }

    public Cylinder(IExtent radius, IExtent height) : this(new Circle(radius), height) { }

    public Cylinder(ICylinder other) : this(other.GetShapeExtentList()) { }

    public IExtent Radius { get; init; }
    public override IVolume Volume { get; init; }

    public override IExtent GetDiagonal(ExtentUnit extentUnit = ExtentUnit.meter)
    {
        extentUnit.ValidateShapeExtentUnit();

        return GetCylinderDiagonal(Radius, Height, extentUnit);
    }

    public IRectangularShape GetDimensions()
    {
        IRectangle bases = (IRectangle)Bases.GetDimensions();

        return ShapeFactory.GetCuboid(bases, Height);
    }

    public ICylinder GetCylinder(params IExtent[] shapeExtents)
    {
        if (shapeExtents == null || shapeExtents.Length == 0) return this;

        ValidateShapeExtentCount(shapeExtents.Length);

        return ShapeFactory.GetCylinder(shapeExtents[0], shapeExtents[1]);
    }

    public ICylinder GetCylinder(IPlaneShape bases, IExtent height)
    {
        _ = bases ?? throw new ArgumentNullException(nameof(bases));

        if (bases is ICircle circle)
        {
            return GetCylinder(circle, height);
        }

        if (bases is IRectangle rectangle)
        {
            ICuboid cuboid = ShapeFactory.GetCuboid(rectangle, height);

            return (ICylinder)cuboid.GetTangentShape();
        }

        throw new ArgumentOutOfRangeException(nameof(bases), bases.GetShapeType(), null);
    }

    public ICylinder GetCylinder(ExtentUnit extentUnit)
    {
        return (ICylinder)ExchangeTo(extentUnit)!;
    }

    public ICylinder GetCylinder(IGeometricBody geometricBody)
    {
        _ = geometricBody ?? throw new ArgumentNullException(nameof(geometricBody));

        return GetCylinder(geometricBody.GetBases(), geometricBody.GetHeight());
    }

    public ICircularShape GetCircularShape(params IExtent[] shapeExtents) => ShapeFactory.GetCircularShape(shapeExtents);

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
        IRectangle bases = (IRectangle)Bases.GetTangentShape(shapeSide);

        return ShapeFactory.GetCuboid(bases, Height);
    }
}
