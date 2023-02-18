namespace CsabaDu.FooVar.Common.Interfaces.Behaviors;

public interface IRound<out T> where T : class
{
    T Round(RoundingMode roundingMode = default);
}
