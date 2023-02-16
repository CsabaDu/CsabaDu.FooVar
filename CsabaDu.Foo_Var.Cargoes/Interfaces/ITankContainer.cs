using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.Foo_Var.Cargoes.Interfaces
{
    public interface ITankContainer : /*IContainerCapacity<ICylinder>, */IContainerBody<ICylinder>
    {
        //IDry<ICylinder> TankContainerCapacity { get; init; }
        //ICylinder? GeometricBody { get; init; }

        IBulkContainer GetTankContainer();
        IBulkContainer GetTankContainer(IDry<ICylinder> tankContainerCapacity);
    }
}
