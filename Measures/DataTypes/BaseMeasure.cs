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
    public int CompareTo(IBaseMeasure? other)
    {
        if (other == null) return 1;

        ValidateMeasureUnitConvertibility(other);

        decimal exchangeRate = GetExchangeRate();
        decimal otherExchangeRate = other.GetExchangeRate();

        decimal quantity = DecimalQuantity;
        decimal otherQuantity = other.GetDecimalQuantity();

        if (otherExchangeRate == exchangeRate) return quantity.CompareTo(otherQuantity);

        quantity *= exchangeRate;
        otherQuantity *= otherExchangeRate;

        return quantity.CompareTo(otherQuantity);
    }

    public bool Equals(IBaseMeasure? other)
    {
        return other is IBaseMeasure baseMeasure
            && Measurement.IsExchangeableTo(baseMeasure.GetMeasureUnit())
            && CompareTo(baseMeasure) == 0;
    }

    public override bool Equals(object? obj)
    {
        return obj is IBaseMeasure other && Equals(other);
    }

    public IBaseMeasure? ExchangeTo(Enum measureUnit)
    {
        if (measureUnit == null) return null;

        if (measureUnit == MeasureUnit) return this;

        if (!Measurement.IsExchangeableTo(measureUnit)) return null;

        IMeasurement measurement = Measurement.GetMeasurement(measureUnit);

        decimal exchangeRate = measurement.ExchangeRate;

        ValueType quantity = ExchangeTo(exchangeRate)!;

        return GetBaseMeasure(quantity, measurement);
    }

    public ValueType? ExchangeTo(decimal exchangeRate)
    {
        if (exchangeRate <= decimal.Zero) return null;

        decimal quantity = DecimalQuantity;
        quantity *= GetExchangeRate();
        quantity /= exchangeRate;

        return ConvertDecimalToQuantityType(quantity);
    }

    public bool IsExchangeableTo(Enum measureUnit)
    {
        return measureUnit?.IsDefinedMeasureUnit() == true
            && HasSameMeasureUnitType(measureUnit);
    }

    public decimal GetDecimalQuantity() => DecimalQuantity;

    public decimal GetExchangeRate() => Measurement.ExchangeRate;

    public override int GetHashCode()
    {
        return HashCode.Combine(Quantity, Measurement);
    }

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

    public decimal ProportionalTo(IBaseMeasure other)
    {
        _ = other ?? throw new ArgumentNullException(nameof(other));

        ValidateMeasureUnitConvertibility(other);

        decimal quantity = DecimalQuantity;
        decimal otherQuantity = other.GetDecimalQuantity();

        if (otherQuantity == decimal.Zero) throw new ArgumentOutOfRangeException(nameof(other), otherQuantity, null);

        if (quantity == decimal.Zero) return decimal.Zero;

        decimal otherExchangeRate = other.GetExchangeRate();

        decimal exchangeRate = GetExchangeRate();

        decimal ratio = Math.Abs(quantity / otherQuantity);

        if (otherExchangeRate == exchangeRate) return ratio;

        exchangeRate /= otherExchangeRate;

        ratio /= exchangeRate;

        return ratio;
    }

    public IBaseMeasure Round(RoundingMode roundingMode = default)
    {
        decimal roundedDecimalQuantity = RoundedDecimalQuantity(roundingMode);

        ValueType roundedQuantity = ConvertDecimalToQuantityType(roundedDecimalQuantity)!;

        return GetBaseMeasure(roundedQuantity);
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

    private static ValueType? ConvertDecimalToType(decimal quantity, Type conversionType)
    {
        //if (conversionType == typeof(decimal)) return quantity;

        if (quantity < 0 && (conversionType == typeof(uint) || conversionType == typeof(ulong))) return null;

        if (conversionType == typeof(double) || conversionType == typeof(decimal))
        {
            quantity = decimal.Round(quantity, 8);
        }
        else if (conversionType == typeof(float))
        {
            quantity = decimal.Round(quantity, 4);
        }

        return quantity.ToQuantity(conversionType);
    }

    private static decimal GetDecimalQuantity(ValueType quantity)
    {
        return (decimal)quantity.ToQuantity(typeof(decimal))!;
    }

    private decimal HalfDecimalQuantity()
    {
        decimal quantity = DecimalQuantity;
        decimal floorQuantity = decimal.Floor(quantity);

        if (floorQuantity == quantity) return floorQuantity;

        decimal halfQuantity = floorQuantity + 0.5m;

        if (quantity <= halfQuantity) return halfQuantity;

        return decimal.Ceiling(quantity);
    }

    private decimal RoundedDecimalQuantity(RoundingMode roundingMode)
    {
        decimal quantity = DecimalQuantity;

        return roundingMode switch
        {
            RoundingMode.General => decimal.Round(quantity),
            RoundingMode.Ceiling => decimal.Ceiling(quantity),
            RoundingMode.Floor => decimal.Floor(quantity),
            RoundingMode.Half => HalfDecimalQuantity(),

            _ => throw new ArgumentOutOfRangeException(nameof(roundingMode)),
        };
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
