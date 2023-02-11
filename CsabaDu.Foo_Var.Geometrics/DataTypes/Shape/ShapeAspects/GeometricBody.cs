using CsabaDu.Foo_Var.Geometrics.Factories;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Spread;
using CsabaDu.Foo_Var.Geometrics.Interfaces.Factories;

namespace CsabaDu.Foo_Var.Geometrics.DataTypes.Shape.ShapeAspects;

internal abstract class GeometricBody : Shape, IGeometricBody
{
    private IBody Body => BodyFactory.GetBody(this);

    public abstract IVolume Volume { get; init; }
    public IBodyFactory BodyFactory { get; init; }

    private protected GeometricBody(ShapeTrait shapeTraits) : base(shapeTraits)
    {
        BodyFactory = new SpreadFactory();
    }

    private protected GeometricBody(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits) : base(shapeExtentList, shapeTraits)
    {
        BodyFactory = new SpreadFactory();
    }

    public int CompareTo(ISpread<IVolume, VolumeUnit>? other) => Body.CompareTo(other);

    public bool Equals(ISpread<IVolume, VolumeUnit>? other) => Body.Equals(other);

    public ISpread<IVolume, VolumeUnit>? ExchangeTo(VolumeUnit volumeUnit) => Body.ExchangeTo(volumeUnit);

    public bool? FitsIn(ISpread<IVolume, VolumeUnit>? other = null, LimitType? limitType = null) => Body.FitsIn(other, limitType);

    public IPlaneShape GetBases(ExtentUnit extentUnit)
    {
        return GetBases().GetPlaneShape(extentUnit);
    }

    public IPlaneShape GetBases(IEnumerable<IExtent> shapeExtentList)
    {
        ValidateShapeExtentList(shapeExtentList, GetBases().ShapeTraits);

        int lastIndex = ShapeExtentTypeCount - 1;
        IExtent[] basesExtents = shapeExtentList.TakeWhile(x => x == shapeExtentList.ElementAt(lastIndex)).ToArray();

        return GetBases(basesExtents);
    }

    public IPlaneShape GetBases(params IExtent[] shapeExtents)
    {
        return ShapeFactory.GetPlaneShape(shapeExtents);
    }

    public IBody GetBody(VolumeUnit? volumeUnit = null) => Body.GetBody(volumeUnit);

    public IBody GetBody(IVolume volume) => Body.GetBody(volume);

    public IBody GetBody(ISpread<IVolume, VolumeUnit> spread) => Body.GetBody(spread);

    public IBody GetBody(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits) => Body.GetBody(shapeExtentList, shapeTraits);

    public IBody GetBody(IShape shape) => Body.GetBody(shape);

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

    public IVolume GetSpreadMeasure(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits, VolumeUnit? spreadMeasureUnit = null) => Body.GetSpreadMeasure(shapeExtentList, shapeTraits, spreadMeasureUnit);

    public IVolume GetSpreadMeasure(VolumeUnit? spreadMeasureUnit = null) => Body.GetSpreadMeasure(spreadMeasureUnit);

    public bool IsExchangeableTo(VolumeUnit volumeUnit) => Body.IsExchangeableTo(volumeUnit);

    public decimal ProportionalTo(ISpread<IVolume, VolumeUnit>? other) => Body.ProportionalTo(other);

    public bool TryExchangeTo(VolumeUnit volumeUnit, [NotNullWhen(true)] out ISpread<IVolume, VolumeUnit>? exchanged) => Body.TryExchangeTo(volumeUnit, out exchanged);

    public void ValidateSpreadMeasure(IVolume volume) => Body.ValidateSpreadMeasure(volume);

    public abstract IExtent GetHeight();
    public abstract IPlaneShape GetProjection(ShapeExtentType shapeExtentType);
}
