using CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.Factories.Shape;

public interface IShapeFactory : IRectangularShapeFactory, ICircularShapeFactory, IPlaneShapeFactory, IGeometricBodyFactory
{
    IShape GetShape(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits);
}
