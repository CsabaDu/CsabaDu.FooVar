using CsabaDu.FooVar.Measures.Factories;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes;
using CsabaDu.FooVar.Measures.Interfaces.Factories;

namespace CsabaDu.FooVar.Measures.DataTypes;

internal abstract class Measure : BaseMeasure, IMeasure
{
    #region Properties
    public IMeasureFactory MeasureFactory { get; init; }
    #endregion

    #region Constructors
    private protected Measure(IMeasureFactory measureFactory, ValueType quantity, IMeasurement measurement) : base(quantity, measurement)
    {
        MeasureFactory = measureFactory ?? throw new ArgumentNullException(nameof(measureFactory));
    }

    private protected Measure(IMeasureFactory measureFactory, ValueType quantity, Enum measureUnit, decimal? exchangeRate = null) : base(quantity, measureUnit, exchangeRate)
    {
        MeasureFactory = measureFactory ?? throw new ArgumentNullException(nameof(measureFactory));
    }

    private protected Measure(IMeasureFactory measureFactory, IBaseMeasure other) : base(other)
    {
        MeasureFactory = measureFactory ?? throw new ArgumentNullException(nameof(measureFactory));
    }
    #endregion

    #region Public methods
    public IMeasure GetMeasure(ValueType quantity, Enum measureUnit, decimal? exchangeRate = null)
    {
        return MeasureFactory.GetMeasure(quantity, measureUnit, exchangeRate);
    }

    public IMeasure GetMeasure(ValueType quantity, IMeasurement? measurement = null)
    {
        measurement ??= Measurement;

        return MeasureFactory.GetMeasure(quantity, measurement);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit, decimal? exchangeRate = null) => GetMeasure(quantity, measureUnit, exchangeRate);

    public override sealed IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement? measurement = null) => GetMeasure(quantity, measurement);

    public override sealed IBaseMeasure GetBaseMeasure(IBaseMeasure? other = null) => GetMeasure(other);

    public bool? FitsIn(IBaseMeasure? other = null, LimitType? limitType = null)
    {
        if (other == null) return true;

        limitType ??= default;

        if (other.GetMeasureUnitType() != GetMeasureUnitType()) return null;

        IBaseMeasure ceilingBaseMeasure = Round(RoundingMode.Ceiling);
        IBaseMeasure floorBaseMeasure = Round(RoundingMode.Floor);

        IBaseMeasure baseMeasure = limitType == LimitType.BeNotLess || limitType == LimitType.BeGreater ?
            ceilingBaseMeasure
            : floorBaseMeasure;

        int comparison = baseMeasure.CompareTo(other);

        if (limitType != LimitType.BeEqual) return comparison.FitsIn(limitType);

        return comparison == 0 && ceilingBaseMeasure.Equals(other);
    }

    public IMeasure SumWith(IMeasure? other, SummingMode summingMode = SummingMode.Add)
    {
        if (other == null) return this;

        ValidateBaseMeasureOperand(other);

        decimal quantity = (decimal)GetQuantity(TypeCode.Decimal);

        decimal exchangedOtherQuantity = GetExchangedQuantity(other);

        decimal decimalSumQuantity = GetSumQuantity(quantity, exchangedOtherQuantity, summingMode);

        return GetMeasure(decimalSumQuantity!);
    }

    public IMeasure MultipliedBy(decimal multiplier)
    {
        decimal multipliedDecimalQuantity = decimal.Multiply(DecimalQuantity, multiplier);

        return GetMeasure(multipliedDecimalQuantity);
    }

    public IMeasure DividedBy(decimal divisor)
    {
        if (divisor == 0) throw new ArgumentOutOfRangeException(nameof(divisor), divisor, null);

        decimal dividedDecimalQuantity = decimal.Divide(DecimalQuantity, divisor);

        return GetMeasure(dividedDecimalQuantity);
    }
    #endregion

    #region Protected methods
    protected void ValidateBaseMeasureOperand(IBaseMeasure other)
    {
        Enum measureUnit = other.GetMeasureUnit();

        if (!HasSameMeasureUnitType(measureUnit)) throw new ArgumentOutOfRangeException(nameof(other), other.GetMeasureUnitType(), null);
    }

    protected static decimal GetSumQuantity(decimal quantity, decimal otherQuantity, SummingMode summingMode)
    {
        return summingMode switch
        {
            SummingMode.Add => decimal.Add(quantity, otherQuantity),
            SummingMode.Subtract => decimal.Subtract(quantity, otherQuantity),

            _ => throw new ArgumentOutOfRangeException(nameof(summingMode)),
        };
    }

    protected static decimal GetValidDecimalOperand(ValueType? quantity)
    {
        if (quantity is decimal decimalQuantity) return decimalQuantity;

        return (decimal)ValidateMeasures.GetValidQuantity(quantity).ToQuantity(typeof(decimal))!;
    }
    #endregion

    #region Private methods
    private decimal GetExchangedQuantity(IMeasure other)
    {
        if (other.Measurement == Measurement) return (decimal)other.GetQuantity(TypeCode.Decimal);

        Enum measureUnit = GetMeasureUnit();

        if (other.TryExchangeTo(measureUnit, out IBaseMeasure? exchanged)) return (decimal)exchanged!.GetQuantity(TypeCode.Decimal);

        throw new ArgumentOutOfRangeException(nameof(other), other.GetMeasureUnitType(), null);
    }
    #endregion

    #region Abstract methods
    public abstract IMeasure GetMeasure(IBaseMeasure? other = null);
    #endregion

    //#region Static operators
    //public static IMeasure? operator +(Measure? measure, IMeasure? other)
    //{
    //    if (measure is null) return other;

    //    return measure.SumWith(other, SummingMode.Add);
    //}
    //public static IMeasure? operator +(IMeasure? measure, Measure? other)
    //{
    //    if (measure is null) return other;

    //    return measure.SumWith(other, SummingMode.Add);
    //}

    //public static IMeasure? operator -(Measure? measure, IMeasure? other)
    //{
    //    if (measure is null) return other!.MultipliedBy(-1);

    //    return measure.SumWith(other, SummingMode.Subtract);
    //}
    //public static IMeasure? operator -(IMeasure? measure, Measure? other)
    //{
    //    if (measure is null) return other!.MultipliedBy(-1);

    //    return measure.SumWith(other, SummingMode.Subtract);
    //}

    //public static IMeasure? operator *(Measure? measure, ValueType? quantity)
    //{
    //    if (measure is null) return null;

    //    decimal multiplier = GetValidDecimalOperand(quantity);

    //    return measure.MultipliedBy(multiplier);
    //}
    //public static IMeasure? operator *(ValueType? quantity, Measure? measure)
    //{
    //    if (measure is null) return null;

    //    decimal multiplier = GetValidDecimalOperand(quantity);

    //    return measure.MultipliedBy(multiplier);
    //}

    //public static IMeasure? operator /(Measure? measure, ValueType? quantity)
    //{
    //    if (measure is null) return null;

    //    decimal divisor = GetValidDecimalOperand(quantity);

    //    return measure.DividedBy(divisor);
    //}
    //#endregion
}
