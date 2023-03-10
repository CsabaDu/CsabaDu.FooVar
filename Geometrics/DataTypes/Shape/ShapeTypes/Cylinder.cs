using CsabaDu.FooVar.Geometrics.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes.MeasureTypes;

namespace CsabaDu.FooVar.Geometrics.DataTypes.Shape.ShapeTypes;

internal sealed class Cylinder : SpatialShape<ICircle>, ICylinder
{
    private ICuboid Dimensions => (ICuboid)GetDimensions();

    public Cylinder(IEnumerable<IExtent> shapeExtentList) : base(shapeExtentList, ShapeTrait.Circular)
    {
        IExtent radius = BaseFace.Radius;

        Radius = radius;
        Volume = GetCylinderVolume(radius, Height);
    }

    public Cylinder(ICircle baseFace, IExtent height) : base(baseFace, height, ShapeTrait.Circular)
    {
        IExtent radius = baseFace.Radius;

        Radius = radius;
        Volume = GetCylinderVolume(radius, height);
    }

    public Cylinder(IExtent radius, IExtent height) : this(new Circle(radius), height) { }

    public Cylinder(ICylinder other) : this(other?.GetShapeExtentList() ?? throw new ArgumentNullException(nameof(other))) { }

    public IExtent Radius { get; init; }
    public override IVolume Volume { get; init; }

    public override IEnumerable<IExtent> DimensionsShapeExtentList => Dimensions.GetShapeExtentList();

    public override IPlaneShape GetBaseFace() => BaseFace;

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

    public ICylinder GetCylinder(IDryBody dryBody)
    {
        _ = dryBody ?? throw new ArgumentNullException(nameof(dryBody));

        return GetCylinder(dryBody.GetBaseFace(), dryBody.GetHeight());
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
