namespace CsabaDu.Foo_Var.Common.Statics;

public static class Extensions
{
    public static bool? FitsIn(this int comparison, LimitType? limitType)
    {
        return limitType switch
        {
            LimitType.BeNotLess => comparison >= 0,
            LimitType.BeNotGreater => comparison <= 0,
            LimitType.BeGreater => comparison > 0,
            LimitType.BeLess => comparison < 0,
            LimitType.BeEqual => comparison == 0,
            _ => null,
        };
    }
}
