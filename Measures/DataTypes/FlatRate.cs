using CsabaDu.FooVar.Measures.Factories;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes;

namespace CsabaDu.FooVar.Measures.DataTypes;

internal sealed class FlatRate : Rate, IFlatRate
{
    #region Properties
    #endregion

    #region Constructors
    internal FlatRate(ValueType quantity, Enum measureUnit, IDenominator denominator, decimal? exchangeRate) : base(new RateFactory(new MeasureFactory()), quantity, measureUnit, denominator, exchangeRate) { }

    internal FlatRate(ValueType quantity, IMeasurement measurement, IDenominator denominator) : base(new RateFactory(new MeasureFactory()), quantity, measurement, denominator) { }

    internal FlatRate(IMeasure numerator, IDenominator denominator) : base(new RateFactory(numerator.MeasureFactory), numerator, denominator) { }

    internal FlatRate(IRate rate) : base(new RateFactory(new MeasureFactory()), rate) { }
    #endregion

    #region Public methods
    public override ILimit? GetLimit() => null;

    public IFlatRate GetFlatRate(IMeasure numerator, IDenominator? denominator = null)
    {
        denominator ??= Denominator;

        return RateFactory.GetFlatRate(numerator, denominator);
    }

    public IFlatRate GetFlatRate(ValueType quantity, Enum measureUnit, IDenominator? denominator = null, decimal? exchangeRate = null)
    {
        denominator ??= Denominator;

        return RateFactory.GetFlatRate(quantity, measureUnit, denominator, exchangeRate);
    }

    public IFlatRate GetFlatRate(ValueType quantity, IMeasurement? measurement = null, IDenominator? denominator = null)
    {
        measurement ??= Measurement;
        denominator ??= Denominator;

        return RateFactory.GetFlatRate(quantity, measurement, denominator);
    }

    public IFlatRate GetFlatRate(IRate? rate = null)
    {
        return rate == null ? this : RateFactory.GetFlatRate(rate);
    }

    public override IRate GetRate(IRate? other = null) => GetFlatRate(other);

    public IFlatRate SumWith(IFlatRate? other, SummingMode summingMode = SummingMode.Add)
    {
        if (other == null) return this;

        if (!IsExchangeableTo(other)) throw new ArgumentOutOfRangeException(nameof(other));

        IDenominator otherDenominator = other.Denominator;

        IMeasure otherNumerator = other.GetNumerator();

        if (Denominator.Equals(otherDenominator))
        {
            IMeasure numerator = SumWith(otherNumerator, summingMode);

            return GetFlatRate(numerator);
        }

        decimal quantity = (decimal)other.GetQuantity(TypeCode.Decimal);

        quantity = GetSumQuantity(DecimalQuantity, quantity, summingMode);

        decimal ratio = Denominator.ProportionalTo(otherDenominator);

        quantity /= ratio;

        return GetFlatRate(quantity);
    }

    public IMeasure MultipliedBy(IMeasure multiplier)
    {
        ValidateBaseMeasureOperand(multiplier);

        decimal quantity = (decimal)multiplier.GetQuantity(TypeCode.Decimal);

        quantity *= DecimalQuantity;

        if (Denominator.Measurement.Equals(multiplier.Measurement)) return GetMeasure(quantity);

        decimal ratio = Denominator.ProportionalTo(multiplier);

        quantity /= ratio;

        return GetMeasure(quantity!);
    }
    #endregion

    //#region Static operators
    //public static IFlatRate? operator +(FlatRate? flatRate, IFlatRate? other)
    //{
    //    if (flatRate is null) return other;

    //    return flatRate.SumWith(other, SummingMode.Add);
    //}
    //public static IFlatRate? operator +(IFlatRate? flatRate, FlatRate? other)
    //{
    //    if (flatRate is null) return other;

    //    return flatRate.SumWith(other, SummingMode.Add);
    //}

    //public static IFlatRate? operator -(FlatRate? flatRate, IFlatRate? other)
    //{
    //    if (flatRate is null) return other!.GetFlatRate(other.MultipliedBy(-1), other.Denominator);

    //    return flatRate.SumWith(other, SummingMode.Subtract);
    //}
    //public static IFlatRate? operator -(IFlatRate? flatRate, FlatRate? other)
    //{
    //    if (flatRate is null) return other!.GetFlatRate(other.MultipliedBy(-1), other.Denominator);

    //    return flatRate.SumWith(other, SummingMode.Subtract);
    //}

    //public static IFlatRate? operator *(FlatRate? flatRate, ValueType? quantity)
    //{
    //    if (flatRate is null) return null;

    //    decimal decimalMultiplier = GetValidDecimalOperand(quantity);

    //    IMeasure multipliedNumerator = flatRate.MultipliedBy(decimalMultiplier);

    //    return flatRate.GetFlatRate(multipliedNumerator, flatRate.Denominator);
    //}
    //public static IFlatRate? operator *(ValueType? quantity, FlatRate? flatRate)
    //{
    //    if (flatRate is null) return null;

    //    decimal decimalMultiplier = GetValidDecimalOperand(quantity);

    //    IMeasure multipliedNumerator = flatRate.MultipliedBy(decimalMultiplier);

    //    return flatRate.GetFlatRate(multipliedNumerator, flatRate.Denominator);
    //}

    //public static IFlatRate? operator /(FlatRate? flatRate, ValueType? quantity)
    //{
    //    if (flatRate is null) return null;

    //    decimal decimalDivisor = GetValidDecimalOperand(quantity);

    //    IMeasure dividedNumerator = flatRate.DividedBy(decimalDivisor);

    //    return flatRate.GetFlatRate(dividedNumerator, flatRate.Denominator);
    //}
    //#endregion
}
