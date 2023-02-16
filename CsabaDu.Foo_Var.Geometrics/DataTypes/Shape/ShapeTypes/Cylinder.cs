using CsabaDu.Foo_Var.Geometrics.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.Foo_Var.Geometrics.DataTypes.Shape.ShapeTypes;

internal sealed class Cylinder : SpatialShape<ICircle>, ICylinder
{
    public Cylinder(IEnumerable<IExtent> shapeExtentList) : base(shapeExtentList, ShapeTrait.Circular)
    {
        IExtent radius = BaseFace.Radius;

        Radius = radius;
        Volume = GetCylinderVolume(radius, Height);
    }

<<<<<<< HEAD
    public Cylinder(ICircle baseFace, IExtent height) : base(baseFace, height, ShapeTrait.Circular)
=======
    public Cylinder(ICircle baseShape, IExtent height) : base(baseShape, height, ShapeTrait.Circular)
>>>>>>> main
    {
        IExtent radius = baseFace.Radius;

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
        IRectangle baseFace = (IRectangle)BaseFace.GetDimensions();

        return ShapeFactory.GetCuboid(baseFace, Height);
    }

    public ICylinder GetCylinder(params IExtent[] shapeExtents)
    {
        if (shapeExtents == null || shapeExtents.Length == 0) return this;

        ValidateShapeExtentCount(shapeExtents.Length);

        return ShapeFactory.GetCylinder(shapeExtents[0], shapeExtents[1]);
    }

    public ICylinder GetCylinder(IPlaneShape baseFace, IExtent height)
    {
        _ = baseFace ?? throw new ArgumentNullException(nameof(baseFace));

        if (baseFace is ICircle circle)
        {
            return GetCylinder(circle, height);
        }

        if (baseFace is IRectangle rectangle)
        {
            ICuboid cuboid = ShapeFactory.GetCuboid(rectangle, height);

            return (ICylinder)cuboid.GetTangentShape();
        }

        throw new ArgumentOutOfRangeException(nameof(baseFace), baseFace.GetShapeType(), null);
    }

    public ICylinder GetCylinder(ExtentUnit extentUnit)
    {
        return (ICylinder)ExchangeTo(extentUnit)!;
    }

    public ICylinder GetCylinder(IGeometricBody geometricBody)
    {
        _ = geometricBody ?? throw new ArgumentNullException(nameof(geometricBody));

<<<<<<< HEAD
        return GetCylinder(geometricBody.GetBaseFace(), geometricBody.GetHeight());
=======
        return GetCylinder(geometricBody.GetBaseShape(), geometricBody.GetHeight());
>>>>>>> main
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
        IRectangle baseFace = (IRectangle)BaseFace.GetTangentShape(shapeSide);

        return ShapeFactory.GetCuboid(baseFace, Height);
    }
}
