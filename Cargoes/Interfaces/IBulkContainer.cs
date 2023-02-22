using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.FooVar.Cargoes.Interfaces
{
    public interface IBulkContainer : ICargoContainer<ICuboid>, IContainerBody
    {
        //IDryMass<ICuboid> BulkContainerCapacity { get; init; }
        //ICuboid? DryBody { get; init; }

        IBulkContainer GetBulkContainer();
        IBulkContainer GetBulkContainer(IDryMass<ICuboid> bulkContainerCapacity);
    }
}
