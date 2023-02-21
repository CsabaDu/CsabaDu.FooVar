using CsabaDu.FooVar.Measures.Interfaces.DataTypes;

namespace CsabaDu.FooVar.Measures.Interfaces.Behaviors;

public interface IExchangeRate<T> : IExchange<T, Enum> where T : class, IBaseMeasure
{
    decimal GetExchangeRate();
}
