using CsabaDu.FooVar.Measures.Interfaces.DataTypes;

namespace CsabaDu.FooVar.Measures.Interfaces.Behaviors;

public interface IMultiply<in U, out T> where U : notnull where T : class, IMeasure
{
    T MultipliedBy(U multiplier);
}