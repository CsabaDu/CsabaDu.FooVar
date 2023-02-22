using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;
using static CsabaDu.FooVar.Geometrics.Statics.ShapeTraits;

namespace CsabaDu.FooVar.Cargoes.Interfaces
{
    public interface IDryContainer<T> : IContainerCapacity<T>/*, IFit<ICargoDoor>*/ where T : class, IDryBody
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

    public interface IContainerCapacity<T> : IDryMass<T>, ICargoContainer<T> where T : class, IDryBody
    {
        IDryMass<T> GetDryCapacity();
        //ICargoContainer<T> GetCargoContainer();
    }

    public interface ICargoContainer : IDryMass/*, IFit<ICargoContainer>*/
    {
        IBulkMass GetBulkCapacity();
        //IDryMass<IDryBody> GetDryCapacity();
        ICargoContainer GetCargoContainer();
    }

    public interface IContainerBody<T> : ICargoContainer<T> where T : class, IDryBody
    {
        IDryMass? ContainerBody { get; }

        void SetContainerBody(IDryMass containerBody);
    }

    public interface ICargoContainer<T> : IDryMass where T : class, IDryBody
    {
        IDryMass<T> DryCapacity { get; init; }

        IBulkMass GetBulkCapacity();
        ICargoContainer GetCargoContainer();
        IWeight GetContainerGrossWeight();
    }
}
