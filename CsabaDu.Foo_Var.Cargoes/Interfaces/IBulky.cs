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
