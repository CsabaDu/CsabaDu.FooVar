using CsabaDu.FooVar.Measures.Interfaces.DataTypes;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes.MeasureTypes;

namespace CsabaDu.FooVar.Commodities.Interfaces
{


    public interface IGoods : ICommodity
    {
        IMeasure GoodsMeasure { get; init; }
        IWeight Weight { get; init; }

        IGoods GetGoods();
        IGoods GetGoods(ICommodity commodity, IMeasure goodsMeasure, IWeight weight);
    }
}
