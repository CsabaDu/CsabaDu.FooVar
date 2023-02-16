﻿using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.Foo_Var.Geometrics.DataTypes.Shape.ShapeAspects;

internal abstract class SpatialShape<T> : GeometricBody, ISpatialShape<T> where T : IPlaneShape
{
<<<<<<< HEAD
    private protected SpatialShape(T baseFace, IExtent height, ShapeTrait shapeTraits) : base(shapeTraits)
    {
        _ = baseFace ?? throw new ArgumentNullException(nameof(baseFace));
        ValidateShapeExtent(height);

        BaseFace = baseFace;
=======
    private protected SpatialShape(T baseShape, IExtent height, ShapeTrait shapeTraits) : base(shapeTraits)
    {
        _ = baseShape ?? throw new ArgumentNullException(nameof(baseShape));
        ValidateShapeExtent(height);

        BaseShape = baseShape;
>>>>>>> main
        Height = height;
    }

    private protected SpatialShape(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits) : base(shapeExtentList, shapeTraits)
    {
<<<<<<< HEAD
        BaseFace = (T)GetBaseFace(shapeExtentList);
        Height = shapeExtentList.Last();
    }

    public T BaseFace { get; init; }
    public IExtent Height { get; init; }
=======
        BaseShape = (T)GetBaseShape(shapeExtentList);
        Height = shapeExtentList.Last();
    }

    public T BaseShape { get; init; }
    public IExtent Height { get; init; }

    public override sealed IExtent GetHeight() => Height;
>>>>>>> main

    public override sealed IExtent GetHeight() => Height;

    public T GetHorizontalProjection() => BaseFace;

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
