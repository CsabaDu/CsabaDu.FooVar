﻿using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread.SpreadTypes;

namespace CsabaDu.FooVar.Geometrics.DataTypes.Spread.SpreadTypes;

internal sealed class BulkBody : Body, IBulkBody
{
    //public IVolume Volume { get; init; }

    public BulkBody(IVolume volume) : base(volume) { }

    public BulkBody(ISpread<IVolume, VolumeUnit> spread) : base(spread) { }

    //public BulkBody(IDryBody dryBody)
    //{
    //    _ = dryBody ?? throw new ArgumentNullException(nameof(dryBody));

    //    Volume = dryBody.Volume;
    //}

    public BulkBody(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits) : base(shapeExtentList, shapeTraits) { }

    public override IBody GetBody(VolumeUnit? volumeUnit = null) => GetBulkBody(volumeUnit);


    public override IBody GetBody(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits) => GetBulkBody(shapeExtentList, shapeTraits);

    public override IBody GetBody(IShape shape) => GetBulkBody(shape);

    public IBulkBody GetBulkBody(IVolume volume)
    {
        return new BulkBody(volume);
    }

    public IBulkBody GetBulkBody(ISpread<IVolume, VolumeUnit> spread)
    {
        return new BulkBody(spread);
    }

    public IBulkBody GetBulkBody(IShape shape)
    {
        _ = shape ?? throw new ArgumentNullException(nameof(shape));

        if (shape is IDryBody dryBody) return new BulkBody(dryBody);

        throw new ArgumentOutOfRangeException(nameof(shape), shape.GetType(), null);
    }

    public IBulkBody GetBulkBody(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits)
    {
        return new BulkBody(shapeExtentList, shapeTraits);
    }

    public IBulkBody GetBulkBody(VolumeUnit? volumeUnit = null)
    {
        if (volumeUnit is not VolumeUnit measureUnit) return this;

        if (TryExchangeTo(measureUnit, out ISpread<IVolume, VolumeUnit>? exchanged)) return GetBulkBody(exchanged);

        throw new ArgumentOutOfRangeException(nameof(volumeUnit), volumeUnit, null);
    }

    //public override ISpread<IVolume, VolumeUnit> GetSpread(IVolume spreadMeasure) => GetBody(spreadMeasure);

    //public override ISpread<IVolume, VolumeUnit> GetSpread(ISpread<IVolume, VolumeUnit> spread) => GetBody(spread);

    //public override ISpread<IVolume, VolumeUnit> GetSpread(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits) => GetBody(shapeExtentList, shapeTraits);

    //public override ISpread<IVolume, VolumeUnit> GetSpread(IShape shape) => GetBody(shape);

    //public override IVolume GetSpreadMeasure(VolumeUnit? volumeUnit = null)
    //{
    //    if (volumeUnit == null) return Volume;

    //    IBaseMeasure exchanged = Volume.ExchangeTo(volumeUnit)!;

    //    return Volume.GetVolume(exchanged);
    //}
}
