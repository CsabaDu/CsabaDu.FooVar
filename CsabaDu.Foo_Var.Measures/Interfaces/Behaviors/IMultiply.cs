namespace CsabaDu.Foo_Var.Measures.Interfaces.Behaviors;

public interface IMultiply<in U, out T> where U : notnull where T : class
{
    T MultipliedBy(U multiplier);
}