using CsabaDu.FooVar.Measures.Factories;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes.MeasureTypes;

namespace CsabaDu.FooVar.Measures.DataTypes.MeasureTypes;

internal sealed class PieceCount : Measure, IPieceCount
{
    internal PieceCount(ValueType quantity, Pieces pieces, decimal? exchangeRate = null) : base(new MeasureFactory(), quantity, pieces, exchangeRate)
    {
        Quantity = quantity.ToQuantity(TypeCode.Int64) ?? throw new ArgumentOutOfRangeException(nameof(quantity), quantity, null);
    }

    internal PieceCount(ValueType quantity, IMeasurement measurement) : base(new MeasureFactory(), quantity, measurement)
    {
        measurement.ValidateMeasureUnitType(typeof(Pieces));

        Quantity = quantity.ToQuantity(TypeCode.Int64) ?? throw new ArgumentOutOfRangeException(nameof(quantity), quantity, null);
    }

    internal PieceCount(IBaseMeasure other) : base(new MeasureFactory(), other)
    {
        other.ValidateMeasureUnitType(typeof(Pieces));
        ValueType quantity = other.GetQuantity();

        Quantity = quantity.ToQuantity(TypeCode.Int64) ?? throw new ArgumentOutOfRangeException(nameof(other), quantity, null);
    }

    public IPieceCount GetCount(int quantity, Pieces pieces)
    {
        return new PieceCount(quantity, pieces);
    }

    public IPieceCount GetCount(IBaseMeasure? other = null)
    {
        return other == null ? this : new PieceCount(other);
    }

    public override IMeasure GetMeasure(IBaseMeasure? other = null) => GetCount(other);
}
