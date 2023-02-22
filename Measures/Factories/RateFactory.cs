using CsabaDu.FooVar.Measures.DataTypes;
using CsabaDu.FooVar.Measures.Interfaces.Behaviors;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes;
using CsabaDu.FooVar.Measures.Interfaces.Factories;
using System.Diagnostics.Metrics;

namespace CsabaDu.FooVar.Measures.Factories;

public sealed class RateFactory : IRateFactory
{
    #region Fields
    private readonly IMeasureFactory _measureFactory;
    #endregion

    #region Constructors
    public RateFactory(IMeasureFactory measureFactory)
    {
        _measureFactory = measureFactory ?? throw new ArgumentNullException(nameof(measureFactory));
    }

    public RateFactory(IRate rate)
    {
        _ = rate ?? throw new ArgumentNullException(nameof(rate));

        _measureFactory = rate.GetNumerator().MeasureFactory;
    }
    #endregion

    #region Public methods
    public IMeasure GetMeasure(ValueType quantity, Enum measureUnit, decimal? exchangeRate = null)
    {
        return _measureFactory.GetMeasure(quantity, measureUnit, exchangeRate);
    }

    public IMeasure GetMeasure(ValueType quantity, IMeasurement measurement)
    {
        return _measureFactory.GetMeasure(quantity, measurement);
    }

    public IMeasure GetMeasure(IBaseMeasure baseMeasure)
    {
        return _measureFactory.GetMeasure(baseMeasure);
    }

    public IDenominator GetDenominator(Enum measureUnit, ValueType? quantity = null, decimal? exchangeRate = null)
    {
        return new Denominator(measureUnit, quantity, exchangeRate);
    }

    public IDenominator GetDenominator(IMeasurement measurement, ValueType? quantity = null)
    {
        return new Denominator(measurement, quantity);
    }

    public IDenominator GetDenominator(IBaseMeasure baseMeasure)
    {
        return new Denominator(baseMeasure);
    }

    public ILimit GetLimit(IMeasurement measurement, ValueType? quantity = null, LimitType limitType = default)
    {
        return  new Limit(measurement, quantity, limitType);
    }

    public ILimit GetLimit(IBaseMeasure baseMeasure, LimitType limitType = default)
    {
        return new Limit(baseMeasure, limitType);
    }

    public ILimit GetLimit(Enum measureUnit, ValueType? quantity = null, decimal? exchangeRate = null, LimitType limitType = default)
    {
        return new Limit(measureUnit, quantity, exchangeRate, limitType);
    }

    public IFlatRate GetFlatRate(IRate rate)
    {
        return new FlatRate(rate);
    }

    public IFlatRate GetFlatRate(IMeasure numerator, IDenominator denominator)
    {
        return new FlatRate(numerator, denominator);
    }

    public IFlatRate GetFlatRate(ValueType quantity, Enum measureUnit, IDenominator denominator, decimal? exchangeRate = null)
    {
        return new FlatRate(quantity, measureUnit, denominator, exchangeRate);
    }

    public IFlatRate GetFlatRate(ValueType quantity, IMeasurement measurement, IDenominator denominator)
    {
        return new FlatRate(quantity, measurement, denominator);
    }

    public ILimitedRate GetLimitedRate(IRate rate, ILimit? limit = null)
    {
        return new LimitedRate(rate, limit);
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, IDenominator denominator, ILimit? limit = null)
    {
        return new LimitedRate(numerator, denominator, limit);
    }

    public ILimitedRate GetLimitedRate(ValueType quantity, Enum measureUnit, IDenominator denominator, decimal? exchangeRate = null, ILimit? limit = null)
    {
        return new LimitedRate(quantity, measureUnit, denominator, exchangeRate, limit);
    }

    public ILimitedRate GetLimitedRate(ValueType quantity, IMeasurement measurement, IDenominator denominator, ILimit? limit = null)
    {
        return new LimitedRate(quantity, measurement, denominator, limit);
    }

    public IRate GetRate(IMeasure numerator, IDenominator denominator, ILimit? limit = null)
    {
        return limit == null ?
            GetFlatRate(numerator, denominator)
            : GetLimitedRate(numerator, denominator, limit);
    }

    public IRate GetRate(IRate other, ILimit? limit = null)
    {
        return limit == null ?
            GetFlatRate(other)
            : GetLimitedRate(other, limit);
    }

    public IRate GetRate(ValueType quantity, Enum measureUnit, IDenominator denominator, decimal? exchangeRate = null, ILimit? limit = null)
    {
        return limit == null ?
            GetFlatRate(quantity, measureUnit, denominator, exchangeRate)
            : GetLimitedRate(quantity, measureUnit, denominator, exchangeRate, limit);
    }

    public IRate GetRate(ValueType quantity, IMeasurement measurement, IDenominator denominator, ILimit? limit = null)
    {
        return limit == null ?
            GetFlatRate(quantity, measurement, denominator)
            : GetLimitedRate(quantity, measurement, denominator, limit);
    }
    #endregion
}
