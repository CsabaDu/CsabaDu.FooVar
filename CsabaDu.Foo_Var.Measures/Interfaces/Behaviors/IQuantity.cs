namespace CsabaDu.Foo_Var.Measures.Interfaces.Behaviors;

public interface IQuantity<out T> : IRound<T>, IExchange<ValueType, decimal> where T : class
{
    ValueType GetQuantity();

    ValueType GetQuantity(RoundingMode roundingMode);

    ValueType GetQuantity(Type type);

    decimal GetDecimalQuantity();
}
