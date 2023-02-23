using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

public interface ICrossSection : ISection
{
    IDryBody GetCrossSectionBody();
    ICrossSection GetCrossSection();
}
