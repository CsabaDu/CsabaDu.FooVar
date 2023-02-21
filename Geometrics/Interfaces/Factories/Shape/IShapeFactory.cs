using CsabaDu.FooVar.Geometrics.Interfaces.Behaviors.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape;

namespace CsabaDu.FooVar.Geometrics.Interfaces.Factories.Shape;

public interface IShapeFactory : IRectangularShapeFactory, ICircularShapeFactory, IPlaneShapeFactory, IDryBodyFactory
{
    IShape GetShape(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits);
}
