using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.FooVar.Cargoes.Interfaces
{
    public interface IDryContainer<T> : IContainerCapacity<T>/*, IFit<ICargoDoor>*/ where T : class, IDryBody
    {
        ICrossSection CargoDoor { get; init; }

        IDryContainer<T> GetDryContainer();
        IDryContainer<T> GetDryContainer(ICargoContainer<T> cargoContainer, ICrossSection cargoDoor);
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
    }

    public interface ICargoContainer : IDryMass/*, IFit<ICargoContainer>*/
    {
        IBulkMass GetBulkCapacity();
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
