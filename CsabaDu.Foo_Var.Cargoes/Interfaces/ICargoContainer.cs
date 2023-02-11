using CsabaDu.Foo_Var.Common.Interfaces.Behaviors;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;
using static CsabaDu.Foo_Var.Geometrics.Statics.ShapeTraits;

namespace CsabaDu.Foo_Var.Cargoes.Interfaces
{
    public interface ICargoContainer : IDry, IFit<ICargoContainer>
    {
        IBulk GetBulkCapacity();
    }
    public interface ICargoContainer<T> : ICargoContainer, IDry<T> where T : class, IGeometricBody
    {
        IDry<T>? GetDryCapacity();
        ICargoContainer<T> GetCargoContainer();
    }

    public interface IDryContainer<T> : ICargoContainer<T> where T : class, IGeometricBody
    {
        ICargoDoor CargoDoor { get; init; }

        IDryContainer<T> GetDryContainer();
        IDryContainer<T> GetDryContainer(ICargoContainer<T> cargoContainer, ICargoDoor cargoDoor);
    }
    public interface ICargoDoor
    {
        IRectangle CargoDoorShape { get; init; }
        ShapeExtentType DoorHeightShapeExtentType { get; init; }
        Comparison? ContainerSide { get; init; }
    }

    public interface IBoxContainer : IDryContainer<ICuboid>
    {

    }

    public interface IIglu : IDryContainer<IComplexSpatialShape>
    {

    }
}
