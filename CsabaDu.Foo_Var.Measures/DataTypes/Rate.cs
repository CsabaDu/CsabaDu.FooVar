using CsabaDu.Foo_Var.Measures.Factories;
using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;
using CsabaDu.Foo_Var.Measures.Interfaces.Factories;

namespace CsabaDu.Foo_Var.Measures.DataTypes;

internal abstract class Rate : Measure, IRate
{
    #region Properties
    public IDenominator Denominator { get; init; }

    public IRateFactory RateFactory { get; init; }
    #endregion

    #region Constructors
    private protected Rate(IRateFactory rateFactory, ValueType quantity, Enum measureUnit, IDenominator denominator, decimal? exchangeRate = null) : base(new MeasureFactory(), quantity, measureUnit, exchangeRate)
    {
        RateFactory = rateFactory ?? throw new ArgumentNullException(nameof(rateFactory));
        Denominator = denominator ?? throw new ArgumentNullException(nameof(denominator));
    }

    private protected Rate(IRateFactory rateFactory, ValueType quantity, IMeasurement measurement, IDenominator denominator) : base(new MeasureFactory(), quantity, measurement)
    {
        RateFactory = rateFactory ?? throw new ArgumentNullException(nameof(rateFactory));
        Denominator = denominator ?? throw new ArgumentNullException(nameof(denominator));
    }

    private protected Rate(IRateFactory rateFactory, IMeasure numerator, IDenominator denominator) : base(numerator.MeasureFactory, numerator)
    {
        RateFactory = rateFactory ?? throw new ArgumentNullException(nameof(rateFactory));
        Denominator = denominator ?? throw new ArgumentNullException(nameof(denominator));
    }

    private protected Rate(IRateFactory rateFactory, IRate other) : this(rateFactory, other?.GetNumerator() ?? throw new ArgumentNullException(nameof(other)), other.Denominator) { }
    #endregion

    #region Public methods
    public int CompareTo(IRate? other)
    {
        if (other == null) return 1;

        if (!IsExchangeableTo(other)) throw new ArgumentOutOfRangeException(nameof(other));

        IDenominator otherDenominator = other.Denominator;

        IMeasure otherNumerator = other.GetNumerator();

        if (Denominator.Equals(otherDenominator)) return CompareTo(otherNumerator);

        decimal denominatorRatio = Denominator.ProportionalTo(otherDenominator);

        return CompareTo(otherNumerator.DividedBy(denominatorRatio));
    }

    public bool Equals(IRate? other)
    {
        return other != null && CompareTo(other) == 0 && GetLimit()?.Equals(GetLimit(), other.GetLimit()) == true;
    }

    public override bool Equals(object? obj)
    {
        return obj is IRate other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Quantity, Measurement, Denominator);
    }

    public IRate? ExchangeTo(IDenominator denominator)
    {
        if (denominator is null) return null;

        if (!Denominator.Measurement.IsExchangeableTo(denominator.GetMeasureUnit())) return null;

        var denominatorsRatio = denominator.ProportionalTo(Denominator);

        return GetRate(MultipliedBy(denominatorsRatio), denominator, GetLimit());
    }

    public override sealed IMeasure GetMeasure(IBaseMeasure? other = null) => GetNumerator(other);

    public IMeasure GetNumerator(IBaseMeasure? other = null)
    {
        if (other == null) return this;

        ValueType quantity = other.GetQuantity();
        IMeasurement measurement = other.Measurement;

        return GetMeasure(quantity, measurement);
    }

    public IRate GetRate(IMeasure numerator, IDenominator? denominator = null, ILimit? limit = null)
    {
        denominator ??= Denominator;
        limit ??= GetLimit();

        return RateFactory.GetRate(numerator, denominator, limit);
    }

    public IRate GetRate(IRate other, ILimit? limit = null)
    {
        if (other == null) return this;

        return RateFactory.GetRate(other.GetNumerator(), other.Denominator, other.GetLimit());
    }

    public bool IsExchangeableTo(IRate other)
    {
        Enum otherMeasureUnit = other.GetNumerator().GetMeasureUnit();

        if (!HasSameMeasureUnitType(otherMeasureUnit)) return false;

        Enum otherDenominatorMeasureUnit = other.Denominator.GetMeasureUnit();

        return Denominator.HasSameMeasureUnitType(otherDenominatorMeasureUnit);
    }

    public decimal ProportionalTo(IRate? other)
    {
        _ = other ?? throw new ArgumentNullException(nameof(other));

        if (!IsExchangeableTo(other)) throw new ArgumentOutOfRangeException(nameof(other));

        IMeasure otherNumerator = other.GetNumerator();

        decimal numeratorRatio = ProportionalTo(otherNumerator);

        IDenominator otherDenominator = other.Denominator;

        if (otherDenominator.GetExchangeRate() == Denominator.GetExchangeRate()) return numeratorRatio;

        decimal otherDenominatorRatio = otherDenominator.ProportionalTo(Denominator);

        if (Equals(otherNumerator)) return otherDenominatorRatio;

        return numeratorRatio * otherDenominatorRatio;
    }

    public bool TryExchangeTo(IDenominator denominator, [NotNullWhen(true)] out IRate? exchanged)
    {
        exchanged = ExchangeTo(denominator);
        return exchanged != null;
    }
    #endregion

    #region Abstract methods
    public abstract ILimit? GetLimit();

    public abstract IRate GetRate(IRate? other = null);
    #endregion

    //#region Static operators
    //public static bool operator ==(Rate? rate, IRate? other)
    //{
    //    return rate?.Equals(other) == true;
    //}
    //public static bool operator ==(IRate? rate, Rate? other)
    //{
    //    return rate?.Equals(other) == true;
    //}

    //public static bool operator !=(Rate? rate, IRate? other)
    //{
    //    return rate?.Equals(other) != true;
    //}
    //public static bool operator !=(IRate? rate, Rate? other)
    //{
    //    return rate?.Equals(other) != true;
    //}

    //public static bool operator >(Rate? rate, IRate? other)
    //{
    //    if (rate is null) return false;

    //    return rate.CompareTo(other) > 0;
    //}
    //public static bool operator >(IRate? rate, Rate? other)
    //{
    //    if (rate is null) return false;

    //    return rate.CompareTo(other) > 0;
    //}

    //public static bool operator <(Rate? rate, IRate? other)
    //{
    //    if (rate is null) return true;

    //    return rate.CompareTo(other) < 0;
    //}
    //public static bool operator <(IRate? rate, Rate? other)
    //{
    //    if (rate is null) return true;

    //    return rate.CompareTo(other) < 0;
    //}

    //public static bool operator >=(Rate? rate, IRate? other)
    //{
    //    if (rate is null) return false;

    //    return rate.CompareTo(other) >= 0;
    //}
    //public static bool operator >=(IRate? rate, Rate? other)
    //{
    //    if (rate is null) return false;

    //    return rate.CompareTo(other) >= 0;
    //}

    //public static bool operator <=(Rate? rate, IRate? other)
    //{
    //    if (rate is null) return true;

    //    return rate.CompareTo(other) <= 0;
    //}
    //public static bool operator <=(IRate? rate, Rate? other)
    //{
    //    if (rate is null) return true;

    //    return rate.CompareTo(other) <= 0;
    //}

    //public static decimal operator /(Rate? rate, IRate? other)
    //{
    //    if (rate is null) return 0m;

    //    return rate.ProportionalTo(other);
    //}
    //public static decimal operator /(IRate? rate, Rate? other)
    //{
    //    if (rate is null) return 0m;

    //    return rate.ProportionalTo(other);
    //}
    //#endregion
}
