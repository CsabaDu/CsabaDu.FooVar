namespace CsabaDu.FooVar.Cargoes.Statics;

public static class CargoCaracteristics
{
    [Flags]
    public enum LoadingRestriction
    {
        None = 0,
        NotStackable = 1,
        NotTurnable =  2,
    }
}
