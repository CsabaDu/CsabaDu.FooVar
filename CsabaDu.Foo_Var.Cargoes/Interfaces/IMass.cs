using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Spread;

namespace CsabaDu.Foo_Var.Cargoes.Interfaces;

public interface IMass
{
    IWeight Weight { get; init; }

    IBody? GetBody();
    IMass GetMass();
    IMass GetMass(IWeight weight, IBody? body = null);
}
