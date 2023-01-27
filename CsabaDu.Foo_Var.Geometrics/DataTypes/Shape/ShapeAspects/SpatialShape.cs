using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.Foo_Var.Geometrics.DataTypes.Shape.ShapeAspects;

internal abstract class SpatialShape<T> : GeometricBody, ISpatialShape<T> where T : IPlaneShape
{
    private protected SpatialShape(T baseShape, IExtent height, ShapeTrait shapeTraits) : base(shapeTraits)
    {
        _ = baseShape ?? throw new ArgumentNullException(nameof(baseShape));
        ValidateShapeExtent(height);

        BaseShape = baseShape;
        Height = height;
    }

    private protected SpatialShape(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits) : base(shapeExtentList, shapeTraits)
    {
        BaseShape = (T)GetBaseShape(shapeExtentList);
        Height = shapeExtentList.Last();
    }

    public T BaseShape { get; init; }
    public IExtent Height { get; init; }

    public override sealed IExtent GetHeight() => Height;

    public T GetHorizontalProjection() => BaseShape;

    public override sealed IPlaneShape GetProjection(ShapeExtentType shapeExtentType)
    {
        T horizontalProjection = GetHorizontalProjection();

        if (shapeExtentType == ShapeExtentType.Height) return horizontalProjection;

        if (horizontalProjection is ICircle && shapeExtentType == ShapeExtentType.Radius) return GetVerticalProjection();

        if (horizontalProjection is IRectangle)
        {
            if (shapeExtentType == ShapeExtentType.Length) return GetVerticalProjection(Comparison.Less);
            if (shapeExtentType == ShapeExtentType.Width) return GetVerticalProjection(Comparison.Greater);
        }

        throw new ArgumentOutOfRangeException(nameof(shapeExtentType), shapeExtentType, null);
    }

    public IRectangle GetVerticalProjection(Comparison? comparison = null)
    {
        List<IExtent> shapeExtentList = new() { Height, };

        IExtent horizontalEdge = BaseShape.GetDiagonal();

        if (BaseShape is IRectangle rectangle && comparison != null)
        {
            horizontalEdge = comparison == Comparison.Greater ?
                rectangle.Width
                : rectangle.Length;
        }

        shapeExtentList.Insert(0, horizontalEdge);

        return (IRectangle)GetShape(shapeExtentList, ShapeTrait.Plane);
    }
}
