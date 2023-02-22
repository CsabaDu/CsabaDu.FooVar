using CsabaDu.FooVar.Common.Interfaces.Behaviors;
using CsabaDu.FooVar.Geometrics.Interfaces.Behaviors.Spread;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape;

namespace CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread
{
    public interface ISpread
    {
        IMeasure GetSpreadMeasure();
        ISpread GetSpread();
    }

    public interface ISpread<T, U> : ISpread, ISpreadMeasure<T, U>, IFit<ISpread<T, U>>, IExchange<ISpread<T, U>, U>, IProportional<ISpread<T, U>, U> where T : class, IMeasure where U : struct, Enum
    {
        ISpread<T, U> GetSpread(U spreadMeasureUnit);
        ISpread<T, U> GetSpread(T spreadMeasure);
        ISpread<T, U> GetSpread(ISpread<T, U> spread);
        ISpread<T, U> GetSpread(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits);
        ISpread<T, U> GetSpread(IShape shape);
    }
}
