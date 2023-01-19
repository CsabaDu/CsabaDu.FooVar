namespace CsabaDu.Foo_Var.Measures.Interfaces.Behaviors;

public interface IExchangeRate<T> : IExchange<T, Enum> where T : class
{
    decimal GetExchangeRate();
}
