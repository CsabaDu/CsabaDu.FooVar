namespace CsabaDu.Foo_Var.Common.Statics;

// Measures
public enum LimitType : byte { BeNotLess, BeNotGreater, BeGreater, BeLess, BeEqual }

public enum RoundingMode : byte { General, Ceiling, Floor, Half }

public enum SummingMode : byte { Add, Subtract }

public enum BaseMeasureType : byte { Measure, Denominator, Limit }

// Geometrics
public enum Side : byte { Outer, Inner }

public enum Comparison : byte { Greater, Less }