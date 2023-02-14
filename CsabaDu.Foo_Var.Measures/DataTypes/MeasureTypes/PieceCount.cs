using CsabaDu.Foo_Var.Measures.Factories;
using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;
using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes.MeasureTypes;

namespace CsabaDu.Foo_Var.Measures.DataTypes.MeasureTypes;

internal sealed class PieceCount : Measure, IPieceCount
{
    public PieceCount(ValueType quantity, Pieces pieces, decimal? exchangeRate = null) : base(new MeasureFactory(), quantity, pieces, exchangeRate)
    {
        Quantity = quantity.ToQuantity(typeof(int))!;
    }

    public PieceCount(ValueType quantity, IMeasurement measurement) : base(new MeasureFactory(), quantity, measurement)
    {
        measurement.ValidateMeasureUnitType(typeof(Pieces));

        Quantity = quantity.ToQuantity(typeof(int))!;
    }

    public PieceCount(IBaseMeasure other) : base(new MeasureFactory(), other)
    {
        other.ValidateMeasureUnitType(typeof(Pieces));

        Quantity = other.GetQuantity().ToQuantity(typeof(int))!;
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
