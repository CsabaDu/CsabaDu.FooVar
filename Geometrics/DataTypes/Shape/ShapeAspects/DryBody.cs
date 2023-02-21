using CsabaDu.FooVar.Geometrics.Factories;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;
using CsabaDu.FooVar.Geometrics.Interfaces.Factories;

namespace CsabaDu.FooVar.Geometrics.DataTypes.Shape.ShapeAspects;

internal abstract class DryBody : Shape, IDryBody
{
    private IBody Body => BodyFactory.GetBody(this);

    public abstract IVolume Volume { get; init; }
    public IBodyFactory BodyFactory { get; init; }

    private protected DryBody(ShapeTrait shapeTraits) : base(new ShapeFactory(), shapeTraits)
    {
        BodyFactory = new SpreadFactory();
    }

    private protected DryBody(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits) : base(shapeExtentList, shapeTraits)
    {
        BodyFactory = new SpreadFactory();
    }

    public int CompareTo(ISpread<IVolume, VolumeUnit>? other) => Body.CompareTo(other);

    public bool Equals(ISpread<IVolume, VolumeUnit>? other) => Body.Equals(other);

    public ISpread<IVolume, VolumeUnit>? ExchangeTo(VolumeUnit volumeUnit) => Body.ExchangeTo(volumeUnit);

    public bool? FitsIn(ISpread<IVolume, VolumeUnit>? other = null, LimitType? limitType = null) => Body.FitsIn(other, limitType);

    public IPlaneShape GetBaseFace(ExtentUnit extentUnit)
    {
        return GetBaseFace().GetPlaneShape(extentUnit);
    }

    public IPlaneShape GetBaseFace(IEnumerable<IExtent> shapeExtentList)
    {
        ValidateShapeExtentList(shapeExtentList, GetBaseFace().ShapeTraits);

        int lastIndex = ShapeExtentTypeCount - 1;
        IExtent[] baseFaceExtents = shapeExtentList.TakeWhile(x => x == shapeExtentList.ElementAt(lastIndex)).ToArray();

        return GetBaseFace(baseFaceExtents);
    }

    public IPlaneShape GetBaseFace(params IExtent[] shapeExtents)
    {
        return ShapeFactory.GetPlaneShape(shapeExtents);
    }

    public IBody GetBody(VolumeUnit? volumeUnit = null) => Body.GetBody(volumeUnit);

    public IBody GetBody(IVolume volume) => Body.GetBody(volume);

    public IBody GetBody(ISpread<IVolume, VolumeUnit> spread) => Body.GetBody(spread);

    public IBody GetBody(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits) => Body.GetBody(shapeExtentList, shapeTraits);

    public IBody GetBody(IShape shape) => Body.GetBody(shape);

    public IDryBody GetDryBody(params IExtent[] shapeExtents)
    {
        return ShapeFactory.GetDryBody(shapeExtents);
    }

    public IDryBody GetDryBody(IDryBody dryBody)
    {
        return ShapeFactory.GetDryBody(dryBody);
    }

    public IDryBody GetDryBody(VolumeUnit? volumeUnit = null)
    {
        if (volumeUnit is not VolumeUnit measureUnit) return this;

        return ExchangeTo(measureUnit) as IDryBody ?? throw new ArgumentOutOfRangeException(nameof(volumeUnit), volumeUnit, null);
    }

    public IDryBody GetDryBody(ExtentUnit extentUnit)
    {
        return ExchangeTo(extentUnit) as IDryBody ?? throw new ArgumentOutOfRangeException(nameof(extentUnit), extentUnit, null);
    }

    public override IShape GetShape(params IExtent[] shapeExtents) => GetDryBody(shapeExtents);

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
