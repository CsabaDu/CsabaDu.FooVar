namespace CsabaDu.FooVar.Measures.Interfaces.Behaviors;

public interface ISum<T> where T : class
{
    T SumWith(T? other, SummingMode summingMode = SummingMode.Add);
}
