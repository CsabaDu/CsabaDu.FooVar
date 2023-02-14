using CsabaDu.Foo_Var.Common.Interfaces.Behaviors;
using CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.Factories;
using CsabaDu.Foo_Var.Geometrics.Interfaces.Factories.Shape;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape;

public interface IShape : IShapeTraits, IShapeExtentList, IDiagonal, ITangentShape, IExchange<IShape, ExtentUnit>, IFit<IShape>
{
    IShapeFactory ShapeFactory { get; init; }
    IShape GetShape(ExtentUnit? extentUnit = null);
    IShape GetShape(params IExtent[] shapeExtents);
    IShape GetShape(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits);
    IShape GetShape(IShape other);

    //void ValidateShapeExtents(params IExtent[] shapeExtents);
}