using CsabaDu.Foo_Var.Common.Interfaces.Behaviors;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;
using static CsabaDu.Foo_Var.Geometrics.Statics.ShapeTraits;

namespace CsabaDu.Foo_Var.Cargoes.Interfaces
{
    public interface IDryCargo
    {
        IGeometricBody GetGeometricBody();
    }

    public interface IPallet
    {
        IDry<ICuboid> PalletTraits { get; init; }

        IRectangle GetPalletSurfaceShape();
    }

    public interface ICargoPallet : IPallet
    {
        ICargoPallet GetCargoPallet();
    }

    public interface IUldPallet : IUld<ICuboid>, IPallet
    {

    }

    public interface ICargoContainer<T> : IDry<T> where T : class, IGeometricBody
    {
        T ContainerShape { get; init; }
        IDry<T>? DryCapacity { get; init; }

        IBulk GetBulkCapacity();
    }

    public interface IContentSize : IFit<ICuboid>
    {
        IDry<ICuboid>? ContentSize { get; init; }
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
}
