namespace CsabaDu.FooVar.Measures.Interfaces.Behaviors;

public interface IExchangeRate<T> : IExchange<T, Enum> where T : class
{
    decimal GetExchangeRate();
}
