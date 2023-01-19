using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;

namespace CsabaDu.Foo_Var.Measures.DataTypes;

internal sealed class LimitedRate : Rate, ILimitedRate
{
    #region Properties
    public ILimit Limit { get; init; }
    #endregion

    #region Constructors
    internal LimitedRate(ValueType quantity, Enum measureUnit, IDenominator denominator, decimal? exchangeRate, ILimit? limit = null) : base(quantity, measureUnit, denominator, exchangeRate)
    {
        Limit = denominator.GetOrCreateLimit(limit);
    }

    internal LimitedRate(ValueType quantity, IMeasurement measurement, IDenominator denominator, ILimit? limit = null) : base(quantity, measurement, denominator)
    {
        Limit = denominator.GetOrCreateLimit(limit);
    }

    internal LimitedRate(IMeasure numerator, IDenominator denominator, ILimit? limit = null) : base(numerator, denominator)
    {
        Limit = denominator.GetOrCreateLimit(limit);
    }

    internal LimitedRate(IFlatRate flatRate, ILimit? limit = null) : this((IRate)flatRate, limit) { }

    internal LimitedRate(ILimitedRate other, ILimit? limit = null) : base(other)
    {
        Limit = other.Denominator.GetOrCreateLimit(limit);
    }

    internal LimitedRate(IRate rate, ILimit? limit = null) : base(rate)
    {
        Limit = rate.GetOrCreateLimit(limit);
    }
    #endregion

    #region Public methods
    public override ILimit? GetLimit() => Limit;

    public ILimitedRate GetLimitedRate(ValueType quantity, Enum measureUnit, IDenominator? denominator = null, decimal? exchangeRate = null, ILimit? limit = null)
    {
        denominator ??= Denominator;
        limit ??= Limit;

        return RateFactory.GetLimitedRate(quantity, measureUnit, denominator, exchangeRate, limit);
    }

    public ILimitedRate GetLimitedRate(ValueType quantity, IMeasurement? measurement = null, IDenominator? denominator = null, ILimit? limit = null)
    {
        measurement ??= Measurement;
        denominator ??= Denominator;
        limit ??= Limit;

        return RateFactory.GetLimitedRate(quantity, measurement, denominator, limit);
    }

    public ILimitedRate GetLimitedRate(IMeasure numerator, IDenominator? denominator = null, ILimit? limit = null)
    {
        denominator ??= Denominator;
        limit ??= Limit;

        return RateFactory.GetLimitedRate(numerator, denominator, limit);
    }

    public ILimitedRate GetLimitedRate(IFlatRate flatRate, ILimit? limit = null)
    {
        return RateFactory.GetLimitedRate(flatRate, limit);
    }

    public ILimitedRate GetLimitedRate(ILimitedRate other, ILimit? limit = null)
    {
        return RateFactory.GetLimitedRate(other, limit);
    }

    public ILimitedRate GetLimitedRate(IRate? rate = null)
    {
        return rate == null ? this : RateFactory.GetLimitedRate(rate);
    }

    public override IRate GetRate(IRate? other = null) => GetLimitedRate(other);

    public override bool Equals(object? obj)
    {
        return obj is ILimitedRate other && Equals(this, other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Quantity, Measurement, Denominator, Limit);
    }

    public bool Equals(ILimitedRate? limitedRate, ILimitedRate? other)
    {
        if (limitedRate == null && other == null) return true;

        if (limitedRate == null || other == null) return false;

        if (!limitedRate.Equals(other.GetNumerator())) return false;

        if (!limitedRate.Denominator.Equals(other.Denominator)) return false;

        ILimit limitedRateLimit = limitedRate.Limit;

        return limitedRateLimit.Equals(limitedRateLimit, other.Limit);
    }

    public int GetHashCode([DisallowNull] ILimitedRate limitedRate)
    {
        return limitedRate.GetHashCode();
    }
    #endregion

    #region Static operators
    public static bool operator ==(LimitedRate? limitedRate, ILimitedRate? other)
    {
        return limitedRate?.Equals(limitedRate, other) == true;
    }
    public static bool operator ==(ILimitedRate? limitedRate, LimitedRate? other)
    {
        return limitedRate?.Equals(limitedRate, other) == true;
    }

    public static bool operator !=(LimitedRate? limitedRate, ILimitedRate? other)
    {
        return limitedRate?.Equals(limitedRate, other) != true;
    }
    public static bool operator !=(ILimitedRate? limitedRate, LimitedRate? other)
    {
        return limitedRate?.Equals(limitedRate, other) != true;
    }
    #endregion
}
