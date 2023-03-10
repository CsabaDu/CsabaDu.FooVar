using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.FooVar.Geometrics.DataTypes.Shape.ShapeAspects;

internal abstract class SpatialShape<T> : DryBody, ISpatialShape<T> where T : IPlaneShape
{
    private protected SpatialShape(T baseFace, IExtent height, ShapeTrait shapeTraits) : base(shapeTraits)
    {
        _ = baseFace ?? throw new ArgumentNullException(nameof(baseFace));
        ValidateShapeExtent(height);

        BaseFace = baseFace;
        Height = height;
    }

    private protected SpatialShape(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits) : base(shapeExtentList, shapeTraits)
    {
        BaseFace = (T)GetBaseFace(shapeExtentList);
        Height = shapeExtentList.Last();
    }

    public T BaseFace { get; init; }
    public IExtent Height { get; init; }

    public override sealed IExtent GetHeight() => Height;

    public T GetHorizontalProjection() => BaseFace;

    public override sealed IPlaneShape GetProjection(ShapeExtentType perpendicular)
    {
        T horizontalProjection = GetHorizontalProjection();

        if (perpendicular == ShapeExtentType.Height) return horizontalProjection;

        if (horizontalProjection is ICircle && perpendicular == ShapeExtentType.Radius) return GetVerticalProjection();

        if (horizontalProjection is IRectangle)
        {
            if (perpendicular == ShapeExtentType.Length) return GetVerticalProjection(Comparison.Less);
            if (perpendicular == ShapeExtentType.Width) return GetVerticalProjection(Comparison.Greater);
        }

        throw new ArgumentOutOfRangeException(nameof(perpendicular), perpendicular, null);
    }

    public IRectangle GetVerticalProjection(Comparison? comparison = null)
    {
        List<IExtent> shapeExtentList = new() { Height, };

        IExtent horizontalEdge = BaseFace.GetDiagonal();

        if (BaseFace is IRectangle rectangle && comparison != null)
        {
            horizontalEdge = comparison == Comparison.Greater ?
                rectangle.Width
                : rectangle.Length;
        }

        shapeExtentList.Insert(0, horizontalEdge);

        return (IRectangle)GetShape(shapeExtentList, ShapeTrait.Plane);
    }
}
