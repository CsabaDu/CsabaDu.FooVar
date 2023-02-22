using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Factories;

public interface ISpreadFactory : IBodyFactory, ISurfaceFactory
{
    ISpread? GetSpread(params object[] args);
}
