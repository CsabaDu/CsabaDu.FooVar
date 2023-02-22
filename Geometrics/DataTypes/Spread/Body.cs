using CsabaDu.FooVar.Geometrics.Factories;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;
using CsabaDu.FooVar.Geometrics.Interfaces.Factories;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes.MeasureTypes;
using static CsabaDu.FooVar.Measures.Statics.MeasureUnit;

namespace CsabaDu.FooVar.Geometrics.DataTypes.Spread
{
    internal abstract class Body : Spread<IVolume, VolumeUnit>, IBody
    {
        public IVolume Volume { get; init; }

        private protected Body(IVolume volume) : base(new SpreadFactory())
        {
            ValidateSpreadMeasure(volume);

            Volume = volume;
        }

        private protected Body(ISpread<IVolume, VolumeUnit> spread) : base(new SpreadFactory())
        {
            _ = spread ?? throw new ArgumentNullException(nameof(spread));

            Volume = (IVolume)spread.GetSpreadMeasure();
        }

        private protected Body(IDryBody dryBody) : base(new SpreadFactory())
        {
            _ = dryBody ?? throw new ArgumentNullException(nameof(dryBody));

            Volume = dryBody.Volume;
        }

        private protected Body(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits) : base(new SpreadFactory())
        {
            shapeTraits.ValidateShapeTraitsBySpreadType(typeof(IDryBody));
            shapeTraits.ValidateShapeExtentList(shapeExtentList);

            Volume = GetSpreadMeasure(shapeExtentList, shapeTraits);
        }

        public IBody GetBody(IVolume volume) => SpreadFactory.GetBody(volume);

        public IBody GetBody(ISpread<IVolume, VolumeUnit> spread) => SpreadFactory.GetBody(spread);

        public override sealed ISpread<IVolume, VolumeUnit> GetSpread(IVolume spreadMeasure) => GetBody(spreadMeasure);

        public override sealed ISpread<IVolume, VolumeUnit> GetSpread(ISpread<IVolume, VolumeUnit> spread) => GetBody(spread);

        public override sealed ISpread<IVolume, VolumeUnit> GetSpread(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits) => GetBody(shapeExtentList, shapeTraits);

        public override sealed ISpread<IVolume, VolumeUnit> GetSpread(IShape shape) => GetBody(shape);

        public override sealed IMeasure GetSpreadMeasure() => Volume;
        public override sealed IVolume GetSpreadMeasure(VolumeUnit volumeUnit)
        {
            if (Volume.TryExchangeTo(volumeUnit, out IBaseMeasure? exchanged)) return Volume.GetVolume(exchanged);

            throw new ArgumentOutOfRangeException(nameof(volumeUnit), volumeUnit, null);
        }

        public abstract IBody GetBody(VolumeUnit? volumeUnit = null);
        public abstract IBody GetBody(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits);
        public abstract IBody GetBody(IShape shape);
    }
}
