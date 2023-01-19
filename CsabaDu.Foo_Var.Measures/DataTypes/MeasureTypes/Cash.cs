using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;
using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes.MeasureTypes;

namespace CsabaDu.Foo_Var.Measures.DataTypes.MeasureTypes;

internal sealed class Cash : Measure, ICash
{
    public Cash(ValueType quantity, Currency currency, decimal? exchangeRate = null) : base(quantity, currency, exchangeRate)
    {
        Quantity = quantity.ToQuantity(typeof(decimal))!;
    }

    public Cash(ValueType quantity, IMeasurement measurement) : base(quantity, measurement)
    {
        measurement.ValidateMeasureUnitType(typeof(Currency));

        Quantity = quantity.ToQuantity(typeof(decimal))!;
    }

    public Cash(IBaseMeasure other) : base(other)
    {
        other.ValidateMeasureUnitType(typeof(Currency));

        Quantity = other.GetQuantity().ToQuantity(typeof(decimal))!;
    }

    public ICash GetCash(decimal quantity, Currency currency)
    {
        return new Cash(quantity, currency);
    }

    public ICash GetCash(IBaseMeasure? other = null)
    {
        return other == null ? this : new Cash(other);
    }

    public override IMeasure GetMeasure(IBaseMeasure? other = null) => GetCash(other);
}
