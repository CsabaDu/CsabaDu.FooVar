using CsabaDu.Foo_Var.Common.Interfaces.Behaviors;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;
using static CsabaDu.Foo_Var.Geometrics.Statics.ShapeTraits;

namespace CsabaDu.Foo_Var.Cargoes.Interfaces
{
    public interface IDryContainer<T> : ICargoContainer<T>/*, IFit<ICargoDoor>*/ where T : class, IGeometricBody
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

    public interface ICargoContainer<T> : IDry<T>, IContainerCapacity<T> where T : class, IGeometricBody
    {
        IDry<T> GetDryCapacity();
        //ICargoContainer<T> GetCargoContainer();
    }

    public interface ICargoContainer : IDry, IFit<ICargoContainer>
    {
        IBulk GetBulkCapacity();
        //IDry<IGeometricBody> GetDryCapacity();
        ICargoContainer GetCargoContainer();
    }

    public interface IContainerBody<T> : IContainerCapacity<T> where T : class, IGeometricBody
    {
        T? ContainerBody { get; init; }
    }

    public interface IContainerCapacity<T> : ICargoContainer where T : class, IGeometricBody
    {
        IDry<T> DryCapacity { get; init; }
    }
}
