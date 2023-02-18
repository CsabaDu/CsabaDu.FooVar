namespace CsabaDu.FooVar.Geometrics.Interfaces.Behaviors.Shape;

public interface IShapeExtentList : IShapeExtentTypes, IShapeExtent
{
    IReadOnlyList<IExtent> GetShapeExtentList();

    void ValidateShapeExtentList(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits);
    void ValidateShapeExtentCount(int count, ShapeTrait? shapeTraits = null);
}