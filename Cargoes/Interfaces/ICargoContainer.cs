using CsabaDu.FooVar.Common.Interfaces.Behaviors;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;
using static CsabaDu.FooVar.Geometrics.Statics.ShapeTraits;

namespace CsabaDu.FooVar.Cargoes.Interfaces
{
    public interface IDryContainer<T> : ICargoContainer<T>/*, IFit<ICargoDoor>*/ where T : class, IDryBody
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

    public interface IIglu : IDryContainer<IComplexDryBody>
    {

    }

    public interface ICargoContainer<T> : IDryMass<T>, IContainerCapacity<T> where T : class, IDryBody
    {
        IDryMass<T> GetDryCapacity();
        //ICargoContainer<T> GetCargoContainer();
    }

    public interface ICargoContainer : IDryMass, IFit<ICargoContainer>
    {
        IBulkMass GetBulkCapacity();
        //IDryMass<IDryBody> GetDryCapacity();
        ICargoContainer GetCargoContainer();
    }

    public interface IContainerBody<T> : IContainerCapacity<T> where T : class, IDryBody
    {
        T? ContainerBody { get; init; }
    }

    public interface IContainerCapacity<T> : ICargoContainer where T : class, IDryBody
    {
        IDryMass<T> DryCapacity { get; init; }
    }
}
