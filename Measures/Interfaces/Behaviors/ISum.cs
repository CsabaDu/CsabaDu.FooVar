using CsabaDu.FooVar.Measures.Interfaces.DataTypes;

namespace CsabaDu.FooVar.Measures.Interfaces.Behaviors;

public interface ISum<T> where T : class, IMeasure
{
    T SumWith(T? other, SummingMode summingMode = SummingMode.Add);
}
