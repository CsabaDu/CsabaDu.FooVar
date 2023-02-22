using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.FooVar.Cargoes.Interfaces
{
    public interface ITankContainer : IContainerBody<ICylinder>
    {
        //IDryMass<ICylinder> TankContainerCapacity { get; init; }
        //ICylinder? DryBody { get; init; }

        IBulkContainer GetTankContainer();
        IBulkContainer GetTankContainer(IDryMass<ICylinder> tankContainerCapacity);
    }
}
