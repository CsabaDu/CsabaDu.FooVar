namespace CsabaDu.Foo_Var.Common.Interfaces.Behaviors;

public interface IRound<out T> where T : class
{
    T Round(RoundingMode roundingMode = default);
}
