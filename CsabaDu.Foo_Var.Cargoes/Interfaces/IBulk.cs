using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Spread;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Spread.SpreadTypes;

namespace CsabaDu.Foo_Var.Cargoes.Interfaces
{
    public interface IBulk
    {
        IBody Body { get; init; }
        IWeight Weight { get; init; }

        IBulkBody GetBulkBody();
        IBulk GetBulk();
    }

    public interface IDry : IBulk
    {
        IGeometricBody GetGeometricBody();
        IWeight GetWeight();
        IDry GetDry();
    }

    public interface IPacking<T> : IDry where T : IPlaneShape
    {
        ISpatialShape<T> PackingShape { get; init; }
        IDry DryCapacity { get; init; }

        IBulk GetBulkCapacity();
    }

    public interface ICommodity
    {
        string CommodityName { get; set; }
        Type MeasureUnitType { get; init; }
        object CustomsCode { get; init; }
        object HandlingType { get; init; }
    }

    public interface IGoods : ICommodity
    {
        IMeasure GoodsMeasure { get; init; }

        IGoods GetGoods();
    }

    public interface ICargoItem : IBulk, IGoods
    {
        ICargoItem GetCargoItem();
    }
}
