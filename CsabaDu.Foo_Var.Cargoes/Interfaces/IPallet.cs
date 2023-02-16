using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.Foo_Var.Cargoes.Interfaces
{
    public interface IPallet : IDry<ICuboid>
    {
        IPallet GetPallet();
    }
}
