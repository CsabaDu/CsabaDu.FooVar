using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.FooVar.Cargoes.Interfaces;

public interface IPacking<T> : IContainerCapacity<T> where T : class, IDryBody
{
    object PackingMaterial { get; init; }

    IPacking<T> GetPacking();
    IPacking<T> GetPacking(object packingMaterial, IDryMass<T>? drCapacity = null);
}
