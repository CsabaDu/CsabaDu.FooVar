namespace CsabaDu.FooVar.Common.Interfaces.Behaviors;

public interface IProportional<T, U> : IExchangeable<T, U>, IComparable<T>, IEquatable<T> where T : class where U : notnull
{
    decimal ProportionalTo(T? other);
}