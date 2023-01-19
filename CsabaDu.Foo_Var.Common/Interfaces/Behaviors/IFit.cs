namespace CsabaDu.Foo_Var.Common.Interfaces.Behaviors;

public interface IFit<T> : IComparable<T>, IEquatable<T> where T : class
{
    bool? FitsIn(T? other = null, LimitType? limitType = null);
}
