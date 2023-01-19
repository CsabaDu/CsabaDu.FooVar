using CsabaDu.Foo_Var.Geometrics.Factories;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Spread;

namespace CsabaDu.Foo_Var.Geometrics.DataTypes.Shape.ShapeAspects;

internal abstract class GeometricBody : Shape, IGeometricBody
{
    private readonly IBody _body;

    public IExtent Height { get; init; }
    public abstract IVolume Volume { get; init; }

    private protected GeometricBody(IExtent height, ShapeTrait shapeTraits) : base(shapeTraits)
    {
        ValidateShapeExtent(height);

        Height = height;

        _body = new SpreadFactory().GetBody(this);
    }

    private protected GeometricBody(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits) : base(shapeExtentList, shapeTraits)
    {
        Height = shapeExtentList.Last();

        _body = new SpreadFactory().GetBody(this);
    }

    public int CompareTo(ISpread<IVolume, VolumeUnit>? other) => _body.CompareTo(other);

    public bool Equals(ISpread<IVolume, VolumeUnit>? other) => _body.Equals(other);

    public ISpread<IVolume, VolumeUnit>? ExchangeTo(VolumeUnit volumeUnit) => _body.ExchangeTo(volumeUnit);

    public bool? FitsIn(ISpread<IVolume, VolumeUnit>? other = null, LimitType? limitType = null) => _body.FitsIn(other, limitType);

    public IPlaneShape GetBaseShape(ExtentUnit extentUnit)
    {
        return GetBaseShape().GetPlaneShape(extentUnit);
    }

    public IPlaneShape GetBaseShape(IEnumerable<IExtent> shapeExtentList)
    {
        ValidateShapeExtentList(shapeExtentList, GetBaseShape().ShapeTraits);

        int lastIndex = ShapeExtentTypeCount - 1;
        IExtent[] baseShapeExtents = shapeExtentList.TakeWhile(x => x == shapeExtentList.ElementAt(lastIndex)).ToArray();

        return GetBaseShape(baseShapeExtents);
    }

    public IPlaneShape GetBaseShape(params IExtent[] shapeExtents)
    {
        return ShapeFactory.GetPlaneShape(shapeExtents);
    }

    public IBody GetBody(VolumeUnit? volumeUnit = null) => _body.GetBody(volumeUnit);

    public IBody GetBody(IVolume volume) => _body.GetBody(volume);

    public IBody GetBody(ISpread<IVolume, VolumeUnit> spread) => _body.GetBody(spread);

    public IBody GetBody(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits) => _body.GetBody(shapeExtentList, shapeTraits);

    public IBody GetBody(IShape shape) => _body.GetBody(shape);

    public IGeometricBody GetGeometricBody(params IExtent[] shapeExtents)
    {
        return ShapeFactory.GetGeometricBody(shapeExtents);
    }

    public IGeometricBody GetGeometricBody(IGeometricBody geometricBody)
    {
        return ShapeFactory.GetGeometricBody(geometricBody);
    }

    public IGeometricBody GetGeometricBody(VolumeUnit? volumeUnit = null)
    {
        if (volumeUnit is not VolumeUnit measureUnit) return this;

        return ExchangeTo(measureUnit) as IGeometricBody ?? throw new ArgumentOutOfRangeException(nameof(volumeUnit), volumeUnit, null);
    }

    public IGeometricBody GetGeometricBody(ExtentUnit extentUnit)
    {
        return ExchangeTo(extentUnit) as IGeometricBody ?? throw new ArgumentOutOfRangeException(nameof(extentUnit), extentUnit, null);
    }

    public override IShape GetShape(params IExtent[] shapeExtents) => GetGeometricBody(shapeExtents);

    public ISpread<IVolume, VolumeUnit> GetSpread(VolumeUnit? volumeUnit = null) => GetBody(volumeUnit);

    public ISpread<IVolume, VolumeUnit> GetSpread(IVolume spreadMeasure) => GetBody(spreadMeasure);

    public ISpread<IVolume, VolumeUnit> GetSpread(ISpread<IVolume, VolumeUnit> spread)=> GetBody(spread);

    public ISpread<IVolume, VolumeUnit> GetSpread(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits) => GetBody(shapeExtentList, shapeTraits);

    public ISpread<IVolume, VolumeUnit> GetSpread(IShape shape) => GetBody(shape);

    public IVolume GetSpreadMeasure(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits, VolumeUnit? spreadMeasureUnit = null) => _body.GetSpreadMeasure(shapeExtentList, shapeTraits, spreadMeasureUnit);

    public IVolume GetSpreadMeasure(VolumeUnit? spreadMeasureUnit = null) => _body.GetSpreadMeasure(spreadMeasureUnit);

    public bool IsExchangeableTo(VolumeUnit volumeUnit) => _body.IsExchangeableTo(volumeUnit);

    public decimal ProportionalTo(ISpread<IVolume, VolumeUnit>? other) => _body.ProportionalTo(other);

    public bool TryExchangeTo(VolumeUnit volumeUnit, [NotNullWhen(true)] out ISpread<IVolume, VolumeUnit>? exchanged) => _body.TryExchangeTo(volumeUnit, out exchanged);

    public void ValidateSpreadMeasure(IVolume volume) => _body.ValidateSpreadMeasure(volume);

    public abstract IPlaneShape GetProjection(ShapeExtentType shapeExtentType);
}
