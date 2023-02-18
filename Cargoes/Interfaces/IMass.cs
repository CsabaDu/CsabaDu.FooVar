using CsabaDu.FooVar.Common.Interfaces.Behaviors;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;

namespace CsabaDu.FooVar.Cargoes.Interfaces;

public interface IMass : IDensity, IFit<IMass>
{
    IWeight Weight { get; init; }

    IBody? GetBody();
    IMass GetMass();
    IMass GetMass(IWeight weight);
}
