using CsabaDu.FooVar.Measures.Interfaces.DataTypes;

namespace CsabaDu.FooVar.Measures.Interfaces.Behaviors;

public interface IQuantity<out T> : IRound<T>, IExchange<ValueType, decimal> where T : class, IBaseMeasure
{
    TypeCode QuantityTypeCode { get; init; }

    ValueType GetQuantity();
    ValueType GetQuantity(RoundingMode roundingMode);
    ValueType GetQuantity(TypeCode typeCode);
    decimal GetDecimalQuantity();
}
