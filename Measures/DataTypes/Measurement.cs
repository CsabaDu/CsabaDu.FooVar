using CsabaDu.FooVar.Measures.Factories;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes;
using CsabaDu.FooVar.Measures.Interfaces.Factories;

namespace CsabaDu.FooVar.Measures.DataTypes;

internal sealed class Measurement : Measurable, IMeasurement
{
    #region Properties
    public decimal ExchangeRate { get; init; }

    public Type MeasureUnitType { get; init; }
    #endregion

    #region Constructors
    internal Measurement(Enum measureUnit, decimal? exchangeRate = null) : base(new MeasurementFactory(), measureUnit)
    {
        measureUnit.ValidateExchangeRate(exchangeRate, false);

        if (!measureUnit.IsValidMeasureUnit()) throw new ArgumentOutOfRangeException(nameof(measureUnit));

        ExchangeRate = measureUnit.GetExchangeRate();
        MeasureUnitType = GetMeasureUnitType();
    }
    #endregion

    #region Public methods
    public bool IsExchangeableTo(Enum measureUnit)
    {
        return HasSameMeasureUnitType(measureUnit) && measureUnit.IsValidMeasureUnit();
    }

    public int CompareTo(IMeasurement? other)
    {
        if (other == null) return 1;

        if (IsExchangeableTo(other.GetMeasureUnit())) return ExchangeRate.CompareTo(other.ExchangeRate);

        throw new ArgumentOutOfRangeException(nameof(other));
    }

    public bool Equals(IMeasurement? other)
    {
        return other != null && IsExchangeableTo(other.GetMeasureUnit()) && other.ExchangeRate == ExchangeRate;
    }

    public override bool Equals(object? obj)
    {
        return obj is IMeasurement other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(MeasureUnitType, ExchangeRate);
    }

    public decimal ProportionalTo(IMeasurement other)
    {
        _ = other ?? throw new ArgumentNullException(nameof(other));

        Enum otherMeasureUnit = other.GetMeasureUnit();

        if (!IsExchangeableTo(otherMeasureUnit)) throw new ArgumentOutOfRangeException(nameof(other), otherMeasureUnit.ToString(), null);

        decimal otherExchangeRate = other.ExchangeRate;

        if (otherExchangeRate == ExchangeRate) return 1m;

        return ExchangeRate / otherExchangeRate;
    }

    public override IMeasurable GetMeasurable(Enum? measureUnit = null)
    {
        if (measureUnit == null) return GetMeasurement();

        return GetMeasurement(measureUnit);
    }

    public IMeasurement GetMeasurement(Enum measureUnit, decimal? exchangeRate = null)
    {
        return MeasurementFactory.GetMeasurement(measureUnit, exchangeRate);
    }

    public IMeasurement GetMeasurement(IMeasurement? other = null)
    {
        if (other == null) return this;

        return MeasurementFactory.GetMeasurement(other);
    }
    #endregion

    //#region Static operators
    //public static bool operator ==(Measurement? measurement, IMeasurement? other)
    //{
    //    return measurement?.Equals(other) == true;
    //}
    //public static bool operator ==(IMeasurement? measurement, Measurement? other)
    //{
    //    return measurement?.Equals(other) == true;
    //}

    //public static bool operator !=(Measurement? measurement, IMeasurement? other)
    //{
    //    return measurement?.Equals(other) != true;
    //}
    //public static bool operator !=(IMeasurement? measurement, Measurement? other)
    //{
    //    return measurement?.Equals(other) != true;
    //}

    //public static bool operator >(Measurement? measurement, IMeasurement? other)
    //{
    //    if (measurement is null) return false;

    //    return measurement.CompareTo(other) > 0;
    //}
    //public static bool operator >(IMeasurement? measurement, Measurement? other)
    //{
    //    if (measurement is null) return false;

    //    return measurement.CompareTo(other) > 0;
    //}

    //public static bool operator <(Measurement? measurement, IMeasurement? other)
    //{
    //    if (measurement is null) return true;

    //    return measurement.CompareTo(other) < 0;
    //}
    //public static bool operator <(IMeasurement? measurement, Measurement? other)
    //{
    //    if (measurement is null) return true;

    //    return measurement.CompareTo(other) < 0;
    //}

    //public static bool operator >=(Measurement? measurement, IMeasurement? other)
    //{
    //    if (measurement is null) return false;

    //    return measurement.CompareTo(other) >= 0;
    //}
    //public static bool operator >=(IMeasurement? measurement, Measurement? other)
    //{
    //    if (measurement is null) return false;

    //    return measurement.CompareTo(other) >= 0;
    //}

    //public static bool operator <=(Measurement? measurement, IMeasurement? other)
    //{
    //    if (measurement is null) return true;

    //    return measurement.CompareTo(other) <= 0;
    //}
    //public static bool operator <=(IMeasurement? measurement, Measurement? other)
    //{
    //    if (measurement is null) return true;

    //    return measurement.CompareTo(other) <= 0;
    //}

    //public static decimal operator /(Measurement? measurement, IMeasurement? other)
    //{
    //    if (measurement is null) return 0m;

    //    return measurement.ProportionalTo(other);
    //}
    //public static decimal operator /(IMeasurement? measurement, Measurement? other)
    //{
    //    if (measurement is null) return 0m;

    //    return measurement.ProportionalTo(other);
    //}
    //#endregion
}
