namespace CsabaDu.Foo_Var.Measures.Interfaces.Behaviors;

public interface ISum<T> where T : class
{
    T SumWith(T? other, SummingMode summingMode = SummingMode.Add);
}
