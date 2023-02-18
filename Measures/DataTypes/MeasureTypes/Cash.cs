using CsabaDu.FooVar.Measures.Factories;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes.MeasureTypes;

namespace CsabaDu.FooVar.Measures.DataTypes.MeasureTypes;

internal sealed class Cash : Measure, ICash
{
    public Cash(ValueType quantity, Currency currency, decimal? exchangeRate = null) : base(new MeasureFactory(), quantity, currency, exchangeRate)
    {
        Quantity = quantity.ToQuantity(typeof(decimal))!;
    }

    public Cash(ValueType quantity, IMeasurement measurement) : base(new MeasureFactory(), quantity, measurement)
    {
        measurement.ValidateMeasureUnitType(typeof(Currency));

        Quantity = quantity.ToQuantity(typeof(decimal))!;
    }

    public Cash(IBaseMeasure other) : base(new MeasureFactory(), other)
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
