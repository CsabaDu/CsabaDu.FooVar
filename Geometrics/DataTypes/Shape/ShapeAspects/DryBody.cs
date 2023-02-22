using CsabaDu.FooVar.Geometrics.Factories;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;
using CsabaDu.FooVar.Geometrics.Interfaces.Factories;

namespace CsabaDu.FooVar.Geometrics.DataTypes.Shape.ShapeAspects;

internal abstract class DryBody : Shape, IDryBody
{
    private readonly IBody _body;

    public abstract IVolume Volume { get; init; }
    public IBodyFactory BodyFactory { get; init; }

    private protected DryBody(ShapeTrait shapeTraits) : base(new ShapeFactory(), shapeTraits)
    {
        BodyFactory = new SpreadFactory();

        _body = BodyFactory.GetBody(this);
    }

    private protected DryBody(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits) : base(shapeExtentList, shapeTraits)
    {
        BodyFactory = new SpreadFactory();

        _body = BodyFactory.GetBody(this);
    }

    public int CompareTo(ISpread<IVolume, VolumeUnit>? other) => _body.CompareTo(other);

    public bool Equals(ISpread<IVolume, VolumeUnit>? other) => _body.Equals(other);

    public ISpread<IVolume, VolumeUnit>? ExchangeTo(VolumeUnit volumeUnit) => _body.ExchangeTo(volumeUnit);

    public bool? FitsIn(ISpread<IVolume, VolumeUnit>? other = null, LimitType? limitType = null) => _body.FitsIn(other, limitType);

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

    public IBody GetBody(VolumeUnit? volumeUnit = null) => _body.GetBody(volumeUnit);

    public IBody GetBody(IVolume volume) => _body.GetBody(volume);

    public IBody GetBody(ISpread<IVolume, VolumeUnit> spread) => _body.GetBody(spread);

    public IBody GetBody(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits) => _body.GetBody(shapeExtentList, shapeTraits);

    public IBody GetBody(IShape shape) => _body.GetBody(shape);

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

    public ISpread<IVolume, VolumeUnit> GetSpread(IVolume spreadMeasure) => GetBody(spreadMeasure);

    public ISpread<IVolume, VolumeUnit> GetSpread(ISpread<IVolume, VolumeUnit> spread)=> GetBody(spread);

    public ISpread<IVolume, VolumeUnit> GetSpread(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits) => GetBody(shapeExtentList, shapeTraits);

    public ISpread<IVolume, VolumeUnit> GetSpread(IShape shape) => GetBody(shape);

    public ISpread<IVolume, VolumeUnit> GetSpread(VolumeUnit volumeUnit) => _body.GetSpread(volumeUnit);

    public ISpread GetSpread() => _body.GetSpread();

    public IVolume GetSpreadMeasure(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits, VolumeUnit? spreadMeasureUnit = null) => _body.GetSpreadMeasure(shapeExtentList, shapeTraits, spreadMeasureUnit);

    public IVolume GetSpreadMeasure(VolumeUnit spreadMeasureUnit) => _body.GetSpreadMeasure(spreadMeasureUnit);

    public IMeasure GetSpreadMeasure() => _body.GetSpreadMeasure();

    public bool IsExchangeableTo(VolumeUnit volumeUnit) => _body.IsExchangeableTo(volumeUnit);

    public decimal ProportionalTo(ISpread<IVolume, VolumeUnit> other) => _body.ProportionalTo(other);

    public bool TryExchangeTo(VolumeUnit volumeUnit, [NotNullWhen(true)] out ISpread<IVolume, VolumeUnit>? exchanged) => _body.TryExchangeTo(volumeUnit, out exchanged);

    public void ValidateSpreadMeasure(IVolume volume) => _body.ValidateSpreadMeasure(volume);

    public abstract IExtent GetHeight();
    public abstract IPlaneShape GetProjection(ShapeExtentType shapeExtentType);
}
