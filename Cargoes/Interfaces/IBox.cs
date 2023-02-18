using CsabaDu.FooVar.Common.Interfaces.Behaviors;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.FooVar.Cargoes.Interfaces
{
    public interface IBox : IPacking<ICuboid>
    {
        IBox GetBox();
    }
}
