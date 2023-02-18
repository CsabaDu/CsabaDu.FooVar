using CsabaDu.FooVar.Measures.Factories;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes;

namespace CsabaDu.FooVar.Measures.DataTypes;

internal abstract class BaseMeasure : Measurable, IBaseMeasure
{
    #region Fields
    protected readonly decimal DecimalQuantity;
    #endregion

    #region Properties
    public object Quantity { get; init; }

    public IMeasurement Measurement { get; init; }
    #endregion

    #region Constructors
    private protected BaseMeasure(ValueType quantity, Enum measureUnit, decimal? exchangeRate = null) : base(new MeasurementFactory(), measureUnit)
    {
        Quantity = ValidateMeasures.GetValidQuantity(quantity);
        Measurement = MeasurementFactory.GetMeasurement(measureUnit, exchangeRate);

        DecimalQuantity = GetDecimalQuantity(quantity);
    }

    private protected BaseMeasure(ValueType quantity, IMeasurement measurement) : base(measurement)
    {
        Quantity = ValidateMeasures.GetValidQuantity(quantity);
        Measurement = MeasurementFactory.GetMeasurement(measurement);

        DecimalQuantity = GetDecimalQuantity(quantity);
    }

    private protected BaseMeasure(IBaseMeasure other) : base(other?.Measurement ?? throw new ArgumentNullException(nameof(other)))
    {
        Quantity = other.Quantity;
        Measurement = MeasurementFactory.GetMeasurement(other.Measurement);

        DecimalQuantity = GetDecimalQuantity(other.GetQuantity());
    }
    #endregion

    #region Public methods
    public decimal GetExchangeRate() => Measurement.ExchangeRate;

    public override sealed IMeasurable GetMeasurable(Enum? measureUnit = null)
    {
        if (measureUnit == null) return this;

        ValueType quantity = GetQuantity();

        return GetBaseMeasure(quantity, measureUnit);
    }

    public ValueType GetQuantity() => (ValueType)Quantity;

    public ValueType GetQuantity(RoundingMode roundingMode)
    {
        decimal roundedQuantity = RoundedDecimalQuantity(roundingMode);

        return ConvertDecimalToQuantityType(roundedQuantity)!;
    }

    public ValueType GetQuantity(Type type)
    {
        _ = type ?? throw new ArgumentNullException(nameof(type));

        return ConvertDecimalToType(DecimalQuantity, type) ?? throw new ArgumentOutOfRangeException(nameof(type), type, null);
    }

    public decimal GetDecimalQuantity() => DecimalQuantity;

    public IBaseMeasure? ExchangeTo(Enum measureUnit)
    {
        if (measureUnit == MeasureUnit) return this;

        if (!Measurement.IsExchangeableTo(measureUnit)) return null;

        IMeasurement measurement = Measurement.GetMeasurement(measureUnit);

        decimal exchangeRate = measurement.ExchangeRate;

        if (ExchangeTo(exchangeRate) is not ValueType quantity) return null;

        return GetBaseMeasure(quantity, measurement);
    }

    public ValueType? ExchangeTo(decimal exchangeRate)
    {
        if (exchangeRate <= decimal.Zero) return null;

        decimal decimalQuantity = DecimalQuantity / exchangeRate;

        decimalQuantity *= GetExchangeRate();

        return ConvertDecimalToQuantityType(decimalQuantity);
    }

    public bool IsExchangeableTo(Enum measureUnit)
    {
        return measureUnit.IsDefinedMeasureUnit() && HasSameMeasureUnitType(measureUnit);
    }

    public IBaseMeasure Round(RoundingMode roundingMode = default)
    {
        decimal roundedDecimalQuantity = RoundedDecimalQuantity(roundingMode);

        ValueType roundedQuantity = ConvertDecimalToQuantityType(roundedDecimalQuantity)!;

        return GetBaseMeasure(roundedQuantity);
    }

    public decimal ProportionalTo(IBaseMeasure? other)
    {
        _ = other ?? throw new ArgumentNullException(nameof(other));

        ValidateMeasureUnitConvertibility(other);

        decimal otherQuantity = (decimal)other.GetQuantity(typeof(decimal));

        if (otherQuantity == decimal.Zero) throw new ArgumentOutOfRangeException(nameof(other), otherQuantity, null);

        if (DecimalQuantity == decimal.Zero) return decimal.Zero;

        decimal otherExchangeRate = other.GetExchangeRate();

        decimal exchangeRate = GetExchangeRate();

        decimal ratio = Math.Abs(DecimalQuantity / otherQuantity);

        if (otherExchangeRate == exchangeRate) return ratio;

        exchangeRate /= otherExchangeRate;

        ratio /= exchangeRate;

        return ratio;
    }

    public bool TryExchangeTo(Enum measureUnit, [NotNullWhen(true)] out IBaseMeasure? exchanged)
    {
        exchanged = ExchangeTo(measureUnit);

        return exchanged != null;
    }

    public bool TryExchangeTo(decimal exchangeRate, [NotNullWhen(true)] out ValueType? exchanged)
    {
        exchanged = ExchangeTo(exchangeRate);

        return exchanged != null;
    }

    public int CompareTo(IBaseMeasure? other)
    {
        if (other == null) return 1;

        ValidateMeasureUnitConvertibility(other);

        decimal exchangeRate = GetExchangeRate();

        decimal otherExchangeRate = other.GetExchangeRate();

        decimal otherQuantity = (decimal)other.GetQuantity().ToQuantity(typeof(decimal))!;

        if (otherExchangeRate == exchangeRate) return DecimalQuantity.CompareTo(otherQuantity);

        exchangeRate /= otherExchangeRate;

        decimal otherExchangedQuantity = otherQuantity / exchangeRate;

        return DecimalQuantity.CompareTo(otherExchangedQuantity);
    }

    public bool Equals(IBaseMeasure? other)
    {
        return other != null && Measurement.IsExchangeableTo(other.GetMeasureUnit()) && CompareTo(other) == 0;
    }

    public override bool Equals(object? obj)
    {
        return obj is IBaseMeasure other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Quantity, Measurement);
    }
    #endregion

    #region Abstract methods
    public abstract IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit, decimal? exchangeRate = null);

    public abstract IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement? measurement = null);

    public abstract IBaseMeasure GetBaseMeasure(IBaseMeasure? other = null);
    #endregion

    #region Private methods
    private ValueType? ConvertDecimalToQuantityType(decimal quantity)
    {
        Type type = Quantity.GetType();

        return ConvertDecimalToType(quantity, type);
    }

    private static ValueType? ConvertDecimalToType(decimal quantity, Type type)
    {
        if (type == typeof(decimal)) return quantity;

        if (type == typeof(uint) || type == typeof(ulong))
        {
            if (quantity < 0) return null;

            quantity = decimal.Round(quantity);
        }
        else
        {
            quantity = decimal.Round(quantity, 8);
        }

        return quantity.ToQuantity(type);
    }

    private static decimal GetDecimalQuantity(ValueType quantity)
    {
        return (decimal)quantity.ToQuantity(typeof(decimal))!;
    }

    private decimal RoundedDecimalQuantity(RoundingMode roundingMode)
    {
        return roundingMode switch
        {
            RoundingMode.General => decimal.Round(DecimalQuantity),
            RoundingMode.Ceiling => decimal.Ceiling(DecimalQuantity),
            RoundingMode.Floor => decimal.Floor(DecimalQuantity),
            RoundingMode.Half => HalfDecimalQuantity(),

            _ => throw new ArgumentOutOfRangeException(nameof(roundingMode)),
        };
    }

    private decimal HalfDecimalQuantity()
    {
        decimal floorDecimalQuantity = decimal.Floor(DecimalQuantity);

        if (floorDecimalQuantity == DecimalQuantity) return floorDecimalQuantity;

        decimal halfDecimalQuantity = floorDecimalQuantity + 0.5m;

        if (DecimalQuantity <= halfDecimalQuantity) return halfDecimalQuantity;

        return decimal.Ceiling(DecimalQuantity);
    }

    private void ValidateMeasureUnitConvertibility(IBaseMeasure other)
    {
        Enum otherMeasureUnit = other.GetMeasureUnit();

        if (!Measurement.IsExchangeableTo(otherMeasureUnit)) throw new ArgumentOutOfRangeException(nameof(other), otherMeasureUnit, null);
    }

    #endregion

    //#region Static operators
    //public static bool operator ==(BaseMeasure? baseMeasure, IBaseMeasure? other)
    //{
    //    return baseMeasure?.Equals(other) == true;
    //}
    //public static bool operator ==(IBaseMeasure? baseMeasure, BaseMeasure? other)
    //{
    //    return baseMeasure?.Equals(other) == true;
    //}

    //public static bool operator !=(BaseMeasure? baseMeasure, IBaseMeasure? other)
    //{
    //    return baseMeasure?.Equals(other) != true;
    //}
    //public static bool operator !=(IBaseMeasure? baseMeasure, BaseMeasure? other)
    //{
    //    return baseMeasure?.Equals(other) != true;
    //}

    //public static bool operator >(IBaseMeasure? baseMeasure, BaseMeasure? other)
    //{
    //    if (baseMeasure is null) return false;

    //    return baseMeasure.CompareTo(other) > 0;
    //}
    //public static bool operator >(BaseMeasure? baseMeasure, IBaseMeasure? other)
    //{
    //    if (baseMeasure is null) return false;

    //    return baseMeasure.CompareTo(other) > 0;
    //}

    //public static bool operator <(BaseMeasure? baseMeasure, IBaseMeasure? other)
    //{
    //    if (baseMeasure is null) return true;

    //    return baseMeasure.CompareTo(other) < 0;
    //}
    //public static bool operator <(IBaseMeasure? baseMeasure, BaseMeasure? other)
    //{
    //    if (baseMeasure is null) return true;

    //    return baseMeasure.CompareTo(other) < 0;
    //}

    //public static bool operator >=(BaseMeasure? baseMeasure, IBaseMeasure? other)
    //{
    //    if (baseMeasure is null) return false;

    //    return baseMeasure.CompareTo(other) >= 0;
    //}
    //public static bool operator >=(IBaseMeasure? baseMeasure, BaseMeasure? other)
    //{
    //    if (baseMeasure is null) return false;

    //    return baseMeasure.CompareTo(other) >= 0;
    //}

    //public static bool operator <=(BaseMeasure? baseMeasure, IBaseMeasure? other)
    //{
    //    if (baseMeasure is null) return true;

    //    return baseMeasure.CompareTo(other) <= 0;
    //}
    //public static bool operator <=(IBaseMeasure? baseMeasure, BaseMeasure? other)
    //{
    //    if (baseMeasure is null) return true;

    //    return baseMeasure.CompareTo(other) <= 0;
    //}

    //public static decimal operator /(BaseMeasure? baseMeasure, IBaseMeasure? other)
    //{
    //    if (baseMeasure is null) return 0m;

    //    return baseMeasure.ProportionalTo(other);
    //}
    //public static decimal operator /(IBaseMeasure? baseMeasure, BaseMeasure? other)
    //{
    //    if (baseMeasure is null) return 0m;

    //    return baseMeasure.ProportionalTo(other);
    //}
    //#endregion
}
