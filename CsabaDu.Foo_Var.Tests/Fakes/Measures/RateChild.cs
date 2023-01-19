﻿using CsabaDu.Foo_Var.Measures.Factories;
using CsabaDu.Foo_Var.Measures.DataTypes;
using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;

namespace CsabaDu.Foo_Var.Tests.Fakes.Measures;

internal class RateChild : Rate
{
    public RateChild(IRate other) : base(other) { }

    public RateChild(IMeasure numerator, IDenominator denominator) : base(numerator, denominator) { }

    public RateChild(ValueType quantity, IMeasurement measurement, IDenominator denominator) : base(quantity, measurement, denominator) { }

    public RateChild(ValueType quantity, Enum measureUnit, IDenominator denominator, decimal? exchangeRate) : base(quantity, measureUnit, denominator, exchangeRate) { }

    public override ILimit? GetLimit()
    {
        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();
        ValueType quantity = RandomParams.GetRandomLimitQuantity();
        LimitType limitType = RandomParams.GetRandomLimitType();

        return new Limit(measureUnit, quantity, null, limitType);
    }

    public override IRate GetRate(IRate? other = null)
    {
        if (other == null) return this;

        if (other is ILimitedRate) return new LimitedRate(other);

        return RateFactory.GetRate(other.GetNumerator(), other.Denominator, other.GetLimit());
    }
}
