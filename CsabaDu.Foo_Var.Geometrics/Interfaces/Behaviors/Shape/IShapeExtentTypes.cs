using System.Collections.Immutable;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;

public interface IShapeExtentTypes
{
    ImmutableSortedSet<ShapeExtentType> ShapeExtentTypeSet { get; init; }
    int ShapeExtentTypeCount { get; init; }
}