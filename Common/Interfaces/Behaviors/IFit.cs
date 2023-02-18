namespace CsabaDu.FooVar.Common.Interfaces.Behaviors;

public interface IFit<T> : IComparable<T>, IEquatable<T> where T : class
{
    bool? FitsIn(T? other = null, LimitType? limitType = null);
}
