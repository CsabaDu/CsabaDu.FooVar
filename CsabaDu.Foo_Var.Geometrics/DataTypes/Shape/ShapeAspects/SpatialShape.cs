using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.Foo_Var.Geometrics.DataTypes.Shape.ShapeAspects;

internal abstract class SpatialShape<T> : GeometricBody, ISpatialShape<T> where T : IPlaneShape
{
    private protected SpatialShape(T bases, IExtent height, ShapeTrait shapeTraits) : base(shapeTraits)
    {
        _ = bases ?? throw new ArgumentNullException(nameof(bases));
        ValidateShapeExtent(height);

        Bases = bases;
        Height = height;
    }

    private protected SpatialShape(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits) : base(shapeExtentList, shapeTraits)
    {
        Bases = (T)GetBases(shapeExtentList);
        Height = shapeExtentList.Last();
    }

    public T Bases { get; init; }
    public IExtent Height { get; init; }

    public override sealed IExtent GetHeight() => Height;

    public T GetHorizontalProjection() => Bases;

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

        IExtent horizontalEdge = Bases.GetDiagonal();

        if (Bases is IRectangle rectangle && comparison != null)
        {
            horizontalEdge = comparison == Comparison.Greater ?
                rectangle.Width
                : rectangle.Length;
        }

        shapeExtentList.Insert(0, horizontalEdge);

        return (IRectangle)GetShape(shapeExtentList, ShapeTrait.Plane);
    }
}
