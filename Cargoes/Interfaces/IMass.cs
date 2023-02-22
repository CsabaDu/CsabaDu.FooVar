using CsabaDu.FooVar.Common.Interfaces.Behaviors;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;

namespace CsabaDu.FooVar.Cargoes.Interfaces;

public interface IMass : IFit<IMass>
{
    IWeight Weight { get; init; }

    IBody GetBody();
    IFlatRate GetDensity();
    IMass GetMass(IWeight? weight = null);
}
