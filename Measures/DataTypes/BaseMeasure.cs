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

        var (quantity, otherQuantity) = TryGetValidQuantityArgsToCompareRatio(other);

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
        decimal thisExchangeRate = GetExchangeRate();

        try
        {
            quantity /= exchangeRate;
            quantity *= thisExchangeRate;
        }
        catch (OverflowException)
        {
            try
            {
                quantity *= thisExchangeRate;
                quantity /= exchangeRate;
            }
            catch (Exception)
            {
                return null;
            }
        }
        catch (Exception)
        {
            return null;
        }

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
        TypeCode typeCode =  Type.GetTypeCode(type ?? throw new ArgumentNullException(nameof(type)));

        return ConvertDecimalToTypeCode(DecimalQuantity, typeCode) ?? throw new ArgumentOutOfRangeException(nameof(type), type, null);
    }

    public decimal ProportionalTo(IBaseMeasure other)
    {
        _ = other ?? throw new ArgumentNullException(nameof(other));

        if (DecimalQuantity == decimal.Zero) return decimal.Zero;

        if (other.GetDecimalQuantity() == decimal.Zero) throw new ArgumentOutOfRangeException(nameof(other), decimal.Zero, null);

        ValidateMeasureUnitConvertibility(other);

        var (quantity, otherQuantity) = TryGetValidQuantityArgsToCompareRatio(other);

        return Math.Abs(quantity) / Math.Abs(otherQuantity);
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
        TypeCode conversionTypeCode = Type.GetTypeCode(Quantity.GetType());

        return ConvertDecimalToTypeCode(quantity, conversionTypeCode);
    }

    private static ValueType? ConvertDecimalToTypeCode(decimal quantity, TypeCode conversionTypeCode)
    {
        var (minValue, maxValue) = ValidateMeasures.GetQuantityValueLimits(conversionTypeCode);

        if (quantity < minValue || quantity > maxValue) return null;

        switch (conversionTypeCode)
        {
            case TypeCode.UInt32:
            case TypeCode.UInt64:
                if (quantity < 0) return null;
                break;
            case TypeCode.Double:
            case TypeCode.Decimal:
                quantity = decimal.Round(quantity, 8);
                break;
        }

        return quantity.ToQuantity(conversionTypeCode);
    }

    private static decimal GetDecimalQuantity(ValueType quantity)
    {
        return (decimal?)quantity.ToQuantity(TypeCode.Decimal) ?? throw new ArgumentOutOfRangeException(nameof(quantity), quantity, null);
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

    private (decimal, decimal) TryGetValidQuantityArgsToCompareRatio(IBaseMeasure other)
    {
        decimal otherQuantity = other.GetDecimalQuantity();
        decimal otherExchangeRate = other.GetExchangeRate();

        return TryGetValidQuantityArgsToCompareRatio(otherQuantity, otherExchangeRate);
    }

    private (decimal, decimal) TryGetValidQuantityArgsToCompareRatio(decimal other, decimal otherExchangeRate)
    {
        decimal quantity = DecimalQuantity;
        decimal exchangeRate = GetExchangeRate();

        if (otherExchangeRate == exchangeRate) return (quantity, other);

        if (other == quantity) return (exchangeRate, otherExchangeRate);

        try
        {
            quantity *= exchangeRate;
            other *= otherExchangeRate;
        }
        catch (OverflowException)
        {
            try
            {
                quantity /= otherExchangeRate;
                other /= exchangeRate;
            }
            catch (OverflowException)
            {
                throw new ArgumentOutOfRangeException(nameof(other), "");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message, ex.InnerException);
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message, ex.InnerException);
        }

        return (quantity, other);
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
