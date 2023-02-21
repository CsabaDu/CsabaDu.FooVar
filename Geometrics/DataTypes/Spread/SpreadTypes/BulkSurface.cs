using CsabaDu.FooVar.Geometrics.Factories;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread.SpreadTypes;
using CsabaDu.FooVar.Geometrics.Interfaces.Factories;

namespace CsabaDu.FooVar.Geometrics.DataTypes.Spread.SpreadTypes;

internal sealed class BulkSurface : Spread<IArea, AreaUnit>, IBulkSurface
{
    public IArea Area { get; init; }

    public BulkSurface(IArea area) : base(new SpreadFactory())
    {
        ValidateSpreadMeasure(area);

        Area = area;
    }

    public BulkSurface(ISpread<IArea, AreaUnit> surface) : base(new SpreadFactory())
    {
        _ = surface ?? throw new ArgumentNullException(nameof(surface));

        Area = surface.GetSpreadMeasure();
    }

    public BulkSurface(IPlaneShape planeShape) : base(new SpreadFactory())
    {
        _ = planeShape ?? throw new ArgumentNullException(nameof(planeShape));

        Area = planeShape.Area;
    }

    public BulkSurface(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits) : base(new SpreadFactory())
    {
        shapeTraits.ValidateShapeTraitsBySpreadType(typeof(IPlaneShape));
        shapeTraits.ValidateShapeExtentList(shapeExtentList);

        Area = GetSpreadMeasure(shapeExtentList, shapeTraits);
    }

    public IBulkSurface GetBulkSurface(AreaUnit? areaUnit = null)
    {
        if (areaUnit is not AreaUnit measureUnit) return this;

        if (TryExchangeTo(measureUnit, out ISpread<IArea, AreaUnit>? exchanged)) return GetBulkSurface(exchanged);

        throw new ArgumentOutOfRangeException(nameof(areaUnit), areaUnit, null);
    }

    public IBulkSurface GetBulkSurface(IShape shape)
    {
        _ = shape ?? throw new ArgumentNullException(nameof(shape));

        if (shape is IPlaneShape planeShape) return new BulkSurface(planeShape);

        if (shape is IDryBody dryBody) return GetBulkSurface(dryBody);

        throw new ArgumentOutOfRangeException(nameof(shape), shape.GetType(), null);
    }

    private static IBulkSurface GetBulkSurface(IDryBody dryBody)
    {

        
        IArea baseArea = dryBody.GetBaseFace().Area;
        IExtent height = dryBody.GetHeight();
        IMeasure basePerimeter = height;

        if (dryBody is ICuboid cuboid)
        {
            basePerimeter = cuboid.Length.SumWith(cuboid.Width).MultipliedBy(2);
        }

        if (dryBody is ICylinder cylinder)
        {
            basePerimeter = cylinder.BaseFace.GetDiagonal().MultipliedBy(Convert.ToDecimal(Math.PI));
        }

        IExtent mantleBaseExtent = height.GetExtent(basePerimeter);
        IArea mantleArea = GetRectangleArea(mantleBaseExtent, height);

        IArea fullSurfaceArea = baseArea.GetArea(baseArea.MultipliedBy(2).SumWith(mantleArea));

        return new BulkSurface(fullSurfaceArea);
    }

    public IBulkSurface GetBulkSurface(ISpread<IArea, AreaUnit> spread)
    {
        return new BulkSurface(spread);
    }

    public IBulkSurface GetBulkSurface(IArea area)
    {
        return new BulkSurface(area);
    }

    public IBulkSurface GetBulkSurface(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits)
    {
        return new BulkSurface(shapeExtentList, shapeTraits);
    }

    public override ISpread<IArea, AreaUnit> GetSpread(IArea spreadMeasure) => GetSurface(spreadMeasure);

    public override ISpread<IArea, AreaUnit> GetSpread(ISpread<IArea, AreaUnit> spread) => GetSurface(spread);

    public override ISpread<IArea, AreaUnit> GetSpread(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits) => GetSurface(shapeExtentList, shapeTraits);

    public override ISpread<IArea, AreaUnit> GetSpread(IShape shape) => GetBulkSurface(shape);

    public override IArea GetSpreadMeasure(AreaUnit? areaUnit = null)
    {
        if (areaUnit == null) return Area;

        if (Area.TryExchangeTo(areaUnit, out IBaseMeasure? exchanged)) return Area.GetArea(exchanged);

        throw new ArgumentOutOfRangeException(nameof(areaUnit), areaUnit, null);
    }

    public ISurface GetSurface(AreaUnit? areaUnit = null) => GetBulkSurface(areaUnit);

    public ISurface GetSurface(IShape shape) => GetBulkSurface(shape);

    public ISurface GetSurface(IArea area) => GetBulkSurface(area);

    public ISurface GetSurface(ISpread<IArea, AreaUnit> spread) => GetBulkSurface(spread);

    public ISurface GetSurface(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits) => GetBulkSurface(shapeExtentList, shapeTraits);
}
