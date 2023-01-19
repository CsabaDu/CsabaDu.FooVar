using CsabaDu.Foo_Var.Measures.DataTypes.MeasureTypes;
using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;
using CsabaDu.Foo_Var.Measures.Interfaces.Factories;

namespace CsabaDu.Foo_Var.Measures.Factories;

public sealed class MeasureFactory : IMeasureFactory
{
    #region Public methods
    public IMeasure GetMeasure(ValueType quantity, Enum measureUnit, decimal? exchangeRate = null)
    {
        _ = measureUnit ?? throw new ArgumentNullException(nameof(measureUnit));

        measureUnit.ValidateExchangeRate(exchangeRate, true);

        return CreateMeasure(quantity, measureUnit, exchangeRate);
    }

    public IMeasure GetMeasure(ValueType quantity, IMeasurement measurement)
    {
        _ = measurement ?? throw new ArgumentNullException(nameof(measurement));

        Enum measureUnit = measurement.GetMeasureUnit();

        return CreateMeasure(quantity, measureUnit, null);
    }

    public IMeasure GetMeasure(IBaseMeasure baseMeasure)
    {
        _ = baseMeasure ?? throw new ArgumentNullException(nameof(baseMeasure));

        ValueType quantity = baseMeasure.GetQuantity();

        Enum measureUnit = baseMeasure.GetMeasureUnit();

        return CreateMeasure(quantity, measureUnit, null);
    }
    #endregion

    #region Private methods
    private static IMeasure CreateMeasure(ValueType quantity, Enum measureUnit, decimal? exchangeRate)
    {
        quantity = ValidateMeasures.GetValidQuantity(quantity);

        return measureUnit switch
        {
            AreaUnit areaUnit => new Area(quantity, areaUnit),
            Currency currency => new Cash(quantity, currency, exchangeRate),
            Pieces pieces => new PieceCount(quantity, pieces, exchangeRate),
            DistanceUnit distanceUnit => new Distance(quantity, distanceUnit),
            ExtentUnit extentUnit => new Extent(quantity, extentUnit),
            TimeUnit timeUnit => new Time(quantity, timeUnit),
            VolumeUnit volumeUnit => new Volume(quantity, volumeUnit),
            WeightUnit weightUnit => new Weight(quantity, weightUnit),

            _ => throw new ArgumentOutOfRangeException(nameof(measureUnit), measureUnit.GetType(), null),
        };
    }
    #endregion
}
