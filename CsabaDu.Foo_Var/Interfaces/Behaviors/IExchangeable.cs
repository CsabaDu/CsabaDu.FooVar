namespace CsabaDu.Foo_Var.Common.Interfaces.Behaviors;

public interface IExchangeable<T, in U> where T : class where U : notnull
{
    bool IsExchangeableTo(U context);
}
