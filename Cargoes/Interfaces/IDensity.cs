namespace CsabaDu.FooVar.Cargoes.Interfaces
{
    public interface IDensity : IFit<IDensity>
    {
        IFlatRate GetDensity();
    }
}
