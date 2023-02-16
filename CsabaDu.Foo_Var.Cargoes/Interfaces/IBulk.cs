<<<<<<< HEAD
﻿using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Spread;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Spread.SpreadTypes;

namespace CsabaDu.Foo_Var.Cargoes.Interfaces;

public interface IBulk : IMass/*, IDensity*/
{
    IBulkBody BulkBody {get; init;}

    IBulk GetBulk();
    IBulk GetBulk(IWeight weight, IBody body);
=======
﻿using CsabaDu.Foo_Var.Common.Interfaces.Behaviors;
using CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Spread;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Spread.SpreadTypes;
using static CsabaDu.Foo_Var.Geometrics.Statics.ShapeTraits;

namespace CsabaDu.Foo_Var.Cargoes.Interfaces
{
    public interface IBulk
    {
        IWeight Weight { get; init; }
        IBody Body {get; init;}

        IBulk GetBulk();
    }

    public interface IPallet
    {
        IDry PalletTraits { get; init; }

        IRectangle GetPalletSurfaceShape();
    }

    public interface ICargoPallet : IPallet
    {
        ICargoPallet GetCargoPallet();
    }

    public interface IUldPallet : IUld<ICuboid>, IPallet
    {

    }

    public interface IDry : IBulk
    {
        IGeometricBody GetGeometricBody();
        IDry GetDry();
    }

    public interface ICargoContainer<T> : IDry where T : IGeometricBody
    {
        T ContainerShape { get; init; }
        IDry? DryCapacity { get; init; }

        IBulk GetBulkCapacity();
    }

    public interface ICommodity
    {
        string CommodityName { get; set; }
        Type GoodsMeasureUnitType { get; init; }
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
        IBulkBody GetBulkBody();
        ICargoItem GetCargoItem();
    }

    public interface IBulkCargoItem : ICargoItem
    {
        IBulkCargoItem GetBulkCargoItem();
    }

    public interface IContentSize : IFit<ICuboid>
    {
        IDry? ContentSize { get; init; }
    }

    public interface IDryCargoItem<T> : IContentSize, ICargoItem, IDry where T : class, IGeometricBody
    {
        ICargoContainer<T> Packing { get; init; }
        IPieceCount CargoItemPieceCount { get; init; }

        IDryCargoItem<T> GetDryCargoItem();
        IWeight GetGrossWeight();
    }

    public interface IBox : ICargoContainer<ICuboid>, IFit<ICargoDoor>
    {
        IBox GetBox();
    }

    public interface IDrum : ICargoContainer<ICylinder>
    {
        IDrum GetDrum();
    }

    public interface ICargoDoor
    {
        IRectangle CargoDoorShape { get; init; }
        ShapeExtentType PerpendicularShapeExtentType { get; init; }
    }

    public interface IBoxContainer : IUld<ICuboid>, ICargoDoor
    {
        ICargoDoor CargoDoor { get; init; }
    }

    public interface ICargo : IBulk
    {
        IEnumerable<ICargoItem> PackingList { get; init; }
    }

    public interface IBulkCargoContainer<T> : ICargoContainer<T> where T : class, IGeometricBody
    {

    }

    public interface IUld<T> : ICargoContainer<T> where T : class, IGeometricBody
    {

    }

    public interface IIglu : IUld<ICuboid>
    {

    }
>>>>>>> main
}
