﻿namespace CsabaDu.Foo_Var.Measures.Interfaces.DataTypes.MeasureTypes;

public interface ITime : IMeasure
{
    ITime GetTime(double quantity, TimeUnit timeUnit);

    ITime GetTime(IBaseMeasure? other = null);
}
