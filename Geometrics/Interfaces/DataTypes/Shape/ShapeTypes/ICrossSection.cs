using CsabaDu.FooVar.Geometrics.Interfaces.Behaviors.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

public interface ICrossSection : IDryBody/*, IDimensions*/
{
    IDryBody GetCrossSectionBody();
    ICrossSection GetCrossSection();
}
