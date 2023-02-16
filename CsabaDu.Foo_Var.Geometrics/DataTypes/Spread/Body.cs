using CsabaDu.Foo_Var.Geometrics.Factories;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Spread;
using CsabaDu.Foo_Var.Geometrics.Interfaces.Factories;

namespace CsabaDu.Foo_Var.Geometrics.DataTypes.Spread
{
    internal abstract class Body : Spread<IVolume, VolumeUnit>, IBody
    {
        public IVolume Volume { get; init; }
        //public IBodyFactory BodyFactory { get; init; }

        private protected Body(IVolume volume) : base(new SpreadFactory())
        {
            ValidateSpreadMeasure(volume);

            Volume = volume;
        }

        private protected Body(ISpread<IVolume, VolumeUnit> spread) : base(new SpreadFactory())
        {
            _ = spread ?? throw new ArgumentNullException(nameof(spread));

            Volume = spread.GetSpreadMeasure();
        }

        private protected Body(IGeometricBody geometricBody) : base(new SpreadFactory())
        {
            _ = geometricBody ?? throw new ArgumentNullException(nameof(geometricBody));

            Volume = geometricBody.Volume;
        }

        private protected Body(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits) : base(new SpreadFactory())
        {
            shapeTraits.ValidateShapeTraitsBySpreadType(typeof(IGeometricBody));
            shapeTraits.ValidateShapeExtentList(shapeExtentList);

            Volume = GetSpreadMeasure(shapeExtentList, shapeTraits);
        }
        public IBody GetBody(IVolume volume) => SpreadFactory.GetBody(volume);
        public IBody GetBody(ISpread<IVolume, VolumeUnit> spread) => SpreadFactory.GetBody(spread);

        public override sealed ISpread<IVolume, VolumeUnit> GetSpread(IVolume spreadMeasure) => GetBody(spreadMeasure);

        public override sealed ISpread<IVolume, VolumeUnit> GetSpread(ISpread<IVolume, VolumeUnit> spread) => GetBody(spread);

        public override ISpread<IVolume, VolumeUnit> GetSpread(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits) => GetBody(shapeExtentList, shapeTraits);

        public override sealed ISpread<IVolume, VolumeUnit> GetSpread(IShape shape) => GetBody(shape);

        public override sealed IVolume GetSpreadMeasure(VolumeUnit? volumeUnit = null)
        {
            if (volumeUnit == null) return Volume;

            IBaseMeasure exchanged = Volume.ExchangeTo(volumeUnit)!;

            return Volume.GetVolume(exchanged);
        }

        public abstract IBody GetBody(VolumeUnit? volumeUnit = null);
        public abstract IBody GetBody(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits);
        public abstract IBody GetBody(IShape shape);
    }
}
