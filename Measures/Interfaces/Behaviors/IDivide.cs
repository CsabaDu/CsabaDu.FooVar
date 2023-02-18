namespace CsabaDu.FooVar.Measures.Interfaces.Behaviors;

public interface IDivide<in U, out T> where U : notnull where T : class
{
    T DividedBy(U divisor);
}