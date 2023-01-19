using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;

namespace CsabaDu.Foo_Var.Measures.DataTypes;

internal sealed class Limit : BaseMeasure, ILimit
{
    #region Properties
    public LimitType LimitType { get; init; }
    #endregion

    #region Constructors
    internal Limit(Enum measureUnit, ValueType? quantity = null, decimal? exchangeRate = null, LimitType limitType = default) : base(quantity ?? 0, measureUnit, exchangeRate)
    {
        Quantity = ValidateMeasures.GetValidQuantity(quantity, Common.Statics.BaseMeasureType.Limit);
        LimitType = limitType;
    }

    internal Limit(IMeasurement measurement, ValueType? quantity = null, LimitType limitType = default) : base(quantity ?? 0, measurement)
    {
        Quantity = ValidateMeasures.GetValidQuantity(quantity, Common.Statics.BaseMeasureType.Limit);
        LimitType = limitType;
    }

    internal Limit(IBaseMeasure baseMeasure, LimitType limitType = default) : base(baseMeasure ?? throw new ArgumentNullException(nameof(baseMeasure)))
    {
        ValueType baseMeasureQuantity = baseMeasure.GetQuantity();

        Quantity = ValidateMeasures.GetValidQuantity(baseMeasureQuantity, Common.Statics.BaseMeasureType.Limit);
        LimitType = limitType;
    }

    internal Limit(ILimit other) : this(other, other?.LimitType ?? default) { }
    #endregion

    #region Public methods
    public ILimit GetLimit(Enum measureUnit, ValueType? quantity = null, decimal? exchangeRate = null, LimitType limitType = default)
    {
        return new Limit(measureUnit, quantity, exchangeRate, limitType);
    }

    public ILimit GetLimit(IMeasurement? measurement = null, ValueType? quantity = null, LimitType limitType = default)
    {
        return new Limit(measurement ?? Measurement, quantity, limitType);
    }

    public ILimit GetLimit(IBaseMeasure? baseMeasure = null, LimitType limitType = default)
    {
        return new Limit(baseMeasure ?? this, limitType);
    }

    public ILimit GetLimit(ILimit? other = null)
    {
        return other == null ? this : GetLimit(other, other.LimitType);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit, decimal? exchangeRate = null)
    {
        return GetLimit(measureUnit, quantity, default);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement? measurement = null)
    {
        return GetLimit(measurement ?? Measurement, quantity, default);
    }

    public override IBaseMeasure GetBaseMeasure(IBaseMeasure? other = null)
    {
        return other == null ? this : GetLimit(other);
    }

    public override bool Equals(object? obj)
    {
        return obj is ILimit other && Equals(this, other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Quantity, Measurement, LimitType);
    }

    public bool Equals(ILimit? limit, ILimit? other)
    {
        if (limit == null && other == null) return true;

        if (limit == null || other == null) return false;

        if (!limit.Equals(other)) return false;

        return limit.LimitType == other.LimitType;
    }

    public int GetHashCode([DisallowNull] ILimit limit)
    {
        return limit.GetHashCode();
    }
    #endregion

    #region Static operators
    public static bool operator ==(Limit limit, ILimit other)
    {
        return limit?.Equals(limit, other) == true;
    }
    public static bool operator ==(ILimit limit, Limit other)
    {
        return limit?.Equals(limit, other) == true;
    }

    public static bool operator !=(Limit limit, ILimit other)
    {
        return limit?.Equals(limit, other) != true;
    }
    public static bool operator !=(ILimit limit, Limit other)
    {
        return limit?.Equals(limit, other) != true;
    }
    #endregion
}
