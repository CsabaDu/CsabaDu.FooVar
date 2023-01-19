using CsabaDu.Foo_Var.Measures.Interfaces.Builders;
using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;
using CsabaDu.Foo_Var.Measures.Interfaces.Factories;

namespace CsabaDu.Foo_Var.Measures.Builders;

public sealed class RateBuilder : IRateBuilder
{
    #region Fields
    private IMeasurement? _numeratorMeasurement;
    private object? _numeratorQuantity;

    private IMeasurement? _denominatorMeasurement;
    private object? _denominatorQuantity;

    private IMeasurement? _limitMeasurement;
    private object? _limitQuantity;
    private LimitType _limitType;

    private readonly IMeasurementFactory _measurementFactory;
    private readonly IRateFactory _rateFactory;
    #endregion

    #region Constructors
    public RateBuilder(IMeasurementFactory measurementFactory, IRateFactory rateFactory)
    {
        _measurementFactory = measurementFactory ?? throw new ArgumentNullException(nameof(measurementFactory));
        _rateFactory = rateFactory ?? throw new ArgumentNullException(nameof(rateFactory));
    }

    public RateBuilder(IRate rate)
    {
        _ = rate ?? throw new ArgumentNullException(nameof(rate));

        _measurementFactory = rate.Denominator.Measurement.MeasurementFactory;
        _rateFactory = rate.RateFactory;

        PrepareRateElements(rate);
    }
    #endregion

    #region Public methods
    public IRateBuilder SetMeasurement(Enum measureUnit, BaseMeasureType baseMeasureType = default, decimal? exchangeRate = null)
    {
        _ = measureUnit ?? throw new ArgumentNullException(nameof(measureUnit));

        IMeasurement measurement = _measurementFactory.GetMeasurement(measureUnit, exchangeRate);

        return SetMeasurement(measurement, baseMeasureType);
    }

    public IRateBuilder SetMeasurement(IMeasurement measurement, BaseMeasureType baseMeasureType = default)
    {
        _ = measurement ?? throw new ArgumentNullException(nameof(measurement));

        switch (baseMeasureType)
        {
            case BaseMeasureType.Measure:
                _numeratorMeasurement = measurement;
                return this;
            case BaseMeasureType.Denominator:
                _denominatorMeasurement = measurement;
                return this;
            case BaseMeasureType.Limit:
                _limitMeasurement = measurement;
                return this;

            default: throw new ArgumentOutOfRangeException(nameof(baseMeasureType));
        }
    }

    public IRateBuilder SetQuantity(ValueType quantity, BaseMeasureType baseMeasureType = default)
    {
        ValueType validQuantity = ValidateMeasures.GetValidQuantity(quantity, baseMeasureType);

        switch (baseMeasureType)
        {
            case BaseMeasureType.Measure:
                _numeratorQuantity = validQuantity;
                break;
            case BaseMeasureType.Denominator:
                _denominatorQuantity = validQuantity;
                break;
            case BaseMeasureType.Limit:
                _limitQuantity = (ulong)validQuantity;
                break;
            default: throw new ArgumentOutOfRangeException(nameof(baseMeasureType));
        }

        return this;
    }

    public IRateBuilder SetLimitType(LimitType limitType)
    {
        _limitType = limitType;

        return this;
    }

    public IRateBuilder SetMeasurements(IMeasurement numeratorMeasurement, IMeasurement denominatorMeasurement, IMeasurement? limitMeasurement = null)
    {
        _numeratorMeasurement = numeratorMeasurement ?? throw new ArgumentNullException(nameof(numeratorMeasurement));

        _denominatorMeasurement = denominatorMeasurement ?? throw new ArgumentNullException(nameof(denominatorMeasurement));

        _limitMeasurement = limitMeasurement ?? denominatorMeasurement;

        return this;
    }

    public IRateBuilder SetQuantities(ValueType numeratorQuantity, ValueType? denominatorQuantity = null, ValueType? limitQuantity = null)
    {
        _numeratorQuantity = numeratorQuantity ?? throw new ArgumentNullException(nameof(numeratorQuantity));

        _denominatorQuantity = denominatorQuantity ?? decimal.One;

        _limitQuantity = limitQuantity ?? uint.MinValue;

        return this;
    }

    public IFlatRate BuildFlatRate()
    {
        IMeasure numerator = CreateNumerator();
        IDenominator denominator = CreateDenominator();

        return _rateFactory.GetFlatRate(numerator, denominator);
    }

    public ILimitedRate BuildLimitedRate()
    {
        IMeasure numerator = CreateNumerator();
        IDenominator denominator = CreateDenominator();
        ILimit limit = CreateLimit();

        return _rateFactory.GetLimitedRate(numerator, denominator, limit);
    }

    public IRate BuildRate()
    {
        return _limitMeasurement == null && _limitQuantity == null ?
            BuildFlatRate()
            : BuildLimitedRate();
    }
    #endregion

    #region Private methods
    private void PrepareRateElements(IRate rate)
    {
        if (rate == null) return;

        IMeasure numerator = rate.GetNumerator();

        _numeratorQuantity = numerator.Quantity;
        _numeratorMeasurement = numerator.Measurement;

        IDenominator denominator = rate.Denominator;

        _denominatorQuantity = denominator.Quantity;
        _denominatorMeasurement = denominator.Measurement;

        if (rate.GetLimit() is not ILimit limit) return;

        _limitMeasurement = limit.Measurement;
        _limitQuantity = limit.Quantity;
        _limitType = limit.LimitType;
    }

    private IMeasure CreateNumerator()
    {
        return _rateFactory.GetMeasure((ValueType)_numeratorQuantity!, _numeratorMeasurement!);
    }

    private IDenominator CreateDenominator()
    {
        return _rateFactory.GetDenominator(_denominatorMeasurement!, (ValueType?)_denominatorQuantity!);
    }

    private ILimit CreateLimit()
    {
        return _rateFactory.GetLimit(_limitMeasurement!, (ValueType?)_limitQuantity, _limitType);
    }
    #endregion
}
