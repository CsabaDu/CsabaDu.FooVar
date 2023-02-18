using CsabaDu.FooVar.Common.Interfaces.Behaviors;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;
using static CsabaDu.FooVar.Geometrics.Statics.ShapeTraits;

namespace CsabaDu.FooVar.Cargoes.Interfaces
{
    public interface IDryCargo
    {
        IGeometricBody GetGeometricBody();
    }

    public interface ICargoPallet : IPallet
    {
        ICargoPallet GetCargoPallet();
    }

    public interface IUldPallet : IUld<ICuboid>, IPallet
    {

    }

    public interface IContentSize : IFit<ICuboid>
    {
        IDry<ICuboid>? ContentSize { get; init; }
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
}
