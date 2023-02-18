using CsabaDu.FooVar.Common.Interfaces.Behaviors;
using CsabaDu.FooVar.Geometrics.Interfaces.Behaviors.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.Factories;
using CsabaDu.FooVar.Geometrics.Interfaces.Factories.Shape;

namespace CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape;

public interface IShape : IShapeTraits, IShapeExtentList, IDiagonal, ITangentShape, IExchange<IShape, ExtentUnit>, IFit<IShape>
{
    IShapeFactory ShapeFactory { get; init; }
    IShape GetShape(ExtentUnit? extentUnit = null);
    IShape GetShape(params IExtent[] shapeExtents);
    IShape GetShape(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits);
    IShape GetShape(IShape other);

    //void ValidateShapeExtents(params IExtent[] shapeExtents);
}