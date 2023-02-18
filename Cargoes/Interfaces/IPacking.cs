using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.FooVar.Cargoes.Interfaces;

public interface IPacking<T> : ICargoContainer<T> where T : class, IGeometricBody
{
    IDry<T> DryCapacity { get; init; }
    object PackingMaterial { get; init; }

    IPacking<T> GetPacking();
    IPacking<T> GetPacking(object packingMaterial, IDry<T>? drCapacity = null);
}
