using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.FooVar.Cargoes.Interfaces
{
    public interface IDrum : IPacking<ICylinder>
    {
        IDrum GetDrum();
    }
}
