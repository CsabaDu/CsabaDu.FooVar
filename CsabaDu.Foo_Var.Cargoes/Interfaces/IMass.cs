using CsabaDu.Foo_Var.Common.Interfaces.Behaviors;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Spread;

namespace CsabaDu.Foo_Var.Cargoes.Interfaces;

public interface IMass : IFit<IMass>
{
    IWeight Weight { get; init; }

    IBody? GetBody();
    IMass GetMass();
    IMass GetMass(IWeight weight);
}
