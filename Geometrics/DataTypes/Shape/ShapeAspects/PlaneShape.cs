using CsabaDu.FooVar.Geometrics.Factories;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;

namespace CsabaDu.FooVar.Geometrics.DataTypes.Shape.ShapeAspects;

internal abstract class PlaneShape : Shape, IPlaneShape
{
    private readonly ISurface _surface;
    public abstract IArea Area { get; init; }

    private protected PlaneShape(ShapeTrait shapeTraits) : base(new ShapeFactory(), shapeTraits)
    {
       _surface = new SpreadFactory().GetSurface(this);
    }

    private protected PlaneShape(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits) : base(shapeExtentList, shapeTraits)
    {
        _surface = new SpreadFactory().GetSurface(this);
    }

    public int CompareTo(ISpread<IArea, AreaUnit>? other) => _surface.CompareTo(other);

    public bool Equals(ISpread<IArea, AreaUnit>? other) => _surface.Equals(other);

    public ISpread<IArea, AreaUnit>? ExchangeTo(AreaUnit areaUnit) => _surface.ExchangeTo(areaUnit);

    public bool? FitsIn(ISpread<IArea, AreaUnit>? other = null, LimitType? limitType = null)
    {
        return _surface.FitsIn(other, limitType);
    }

    public IExtent GetEdge(IExtent diagonal)
    {
        ValidateShapeExtent(diagonal);

        decimal sqrtOfTwo = Convert.ToDecimal(Math.Sqrt(2));
        IMeasure edge = diagonal.DividedBy(sqrtOfTwo);

        return diagonal.GetExtent(edge);
    }

    public IPlaneShape GetPlaneShape(params IExtent[] shapeExtents)
    {
        return ShapeFactory.GetPlaneShape(shapeExtents);
    }

    public IPlaneShape GetPlaneShape(IPlaneShape planeShape)
    {
        return ShapeFactory.GetPlaneShape(planeShape);
    }

    public IPlaneShape GetPlaneShape(AreaUnit? areaUnit = null)
    {
        if (areaUnit is not AreaUnit measureUnit) return this;

        return ExchangeTo(measureUnit) as IPlaneShape ?? throw new ArgumentOutOfRangeException(nameof(areaUnit), areaUnit, null);
    }

    public IPlaneShape GetPlaneShape(ExtentUnit extentUnit)
    {
        if (TryExchangeTo(extentUnit, out IShape? exchanged) && exchanged is IPlaneShape planeShape) return planeShape;
        
        throw new ArgumentOutOfRangeException(nameof(extentUnit), extentUnit, null);
    }

    public override IShape GetShape(params IExtent[] shapeExtents) => GetPlaneShape(shapeExtents);

    public ISpread<IArea, AreaUnit> GetSpread(AreaUnit? areaUnit = null) => GetSurface(areaUnit);

    public ISpread<IArea, AreaUnit> GetSpread(IShape shape) => GetSurface(shape);

    public ISpread<IArea, AreaUnit> GetSpread(ISpread<IArea, AreaUnit> spread) => GetSurface(spread);

    public ISpread<IArea, AreaUnit> GetSpread(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits) => GetSurface(shapeExtentList, shapeTraits);

    public ISpread<IArea, AreaUnit> GetSpread(IArea spreadMeasure) => GetSurface(spreadMeasure);

    public IArea GetSpreadMeasure(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits, AreaUnit? spreadMeasureUnit = null)
    {
        shapeTraits.ValidateShapeTraits(GetShapeType());

        return _surface.GetSpreadMeasure(shapeExtentList, shapeTraits, spreadMeasureUnit);
    }

    public IArea GetSpreadMeasure(AreaUnit? areaUnit = null) => _surface.GetSpreadMeasure(areaUnit);

    public ISurface GetSurface(AreaUnit? areaUnit = null) => _surface.GetSurface(areaUnit);

    public ISurface GetSurface(IShape shape) => _surface.GetSurface(shape);

    public ISurface GetSurface(IArea area) => _surface.GetSurface(area);

    public ISurface GetSurface(ISpread<IArea, AreaUnit> spread) => _surface.GetSurface(spread);

    public ISurface GetSurface(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits) => _surface.GetSurface(shapeExtentList, shapeTraits);

    public bool IsExchangeableTo(AreaUnit areaUnit) => _surface.IsExchangeableTo(areaUnit);

    public decimal ProportionalTo(ISpread<IArea, AreaUnit>? other) => _surface.ProportionalTo(other);

    public bool TryExchangeTo(AreaUnit areaUnit, [NotNullWhen(true)] out ISpread<IArea, AreaUnit>? exchanged) => _surface.TryExchangeTo(areaUnit, out exchanged);

    public void ValidateSpreadMeasure(IArea spreadMeasure) => _surface.ValidateSpreadMeasure(spreadMeasure);
}
