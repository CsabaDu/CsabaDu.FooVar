using System.Collections.Immutable;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Behaviors.Shape;

public interface IShapeExtentTypes
{
    ImmutableSortedSet<ShapeExtentType> ShapeExtentTypeSet { get; init; }
    int ShapeExtentTypeCount { get; init; }

    void ValidateShapeExtentType(ShapeExtentType shapeExtentType);
}