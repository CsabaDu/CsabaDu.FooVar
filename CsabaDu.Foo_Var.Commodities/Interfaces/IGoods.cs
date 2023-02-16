using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes;
using CsabaDu.Foo_Var.Measures.Interfaces.DataTypes.MeasureTypes;

namespace CsabaDu.Foo_Var.Commodities.Interfaces
{


    public interface IGoods : ICommodity
    {
        IMeasure GoodsMeasure { get; init; }
        IWeight Weight { get; init; }

        IGoods GetGoods();
        IGoods GetGoods(ICommodity commodity, IMeasure goodsMeasure, IWeight weight);
    }
}
