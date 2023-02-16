using CsabaDu.Foo_Var.Common.Interfaces.Behaviors;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.Foo_Var.Cargoes.Interfaces
{
    public interface IBox : IPacking<ICuboid>
    {
        IBox GetBox();
    }
}
