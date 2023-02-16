using CsabaDu.Foo_Var.Geometrics.Interfaces.Behaviors.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.Foo_Var.Geometrics.Statics;

public static class ValidateGeometrics
{
    private static readonly ShapeTrait AllShapeTraits = ShapeTrait.Plane | ShapeTrait.Circular;
    internal static readonly int CuboidShapeExtentCount = ShapeTrait.None.GetShapeExtentCount();

    public static void ValidateInnerShapeExtentList(IEnumerable<IExtent> innerShapeExtentList)
    {
        _ = innerShapeExtentList ?? throw new ArgumentNullException(nameof(innerShapeExtentList));

        int count = innerShapeExtentList.Count();

        if (count % CuboidShapeExtentCount != 0) throw new ArgumentOutOfRangeException(nameof(innerShapeExtentList), count, null);

        ValidateShapeExtentListElements(innerShapeExtentList);
    }

    public static void ValidateShape(this ShapeTrait shapeTraits, IShape shape)
    {
        _ = shape ?? throw new ArgumentNullException(nameof(shape));

        if (shape.ShapeTraits != shapeTraits) throw new ArgumentOutOfRangeException(nameof(shape), shape.GetType(), null);
    }

    public static void ValidateShapeTraits(this ShapeTrait shapeTraits, Type? shapeType = null)
    {
        if (!AllShapeTraits.HasFlag(shapeTraits)) throw new ArgumentOutOfRangeException(nameof(shapeTraits), shapeTraits, null);

        if (shapeType == null) return;

        Type[] interfaces = shapeType.GetInterfaces();

        ValidateShapeTraitsBySpreadType(shapeTraits, interfaces, shapeType);
        ValidateShapeTraitsByEdgeType(shapeTraits, interfaces, shapeType);
    }

    public static void ValidateShapeTraitsBySpreadType(this ShapeTrait shapeTraits, Type shapeType)
    {
        Type[] interfaces = shapeTraits.GetShapeTypeInterfaces(shapeType);

        ValidateShapeTraitsBySpreadType(shapeTraits, interfaces, shapeType);
    }

    public static void ValidateShapeTraitsByEdgeType(this ShapeTrait shapeTraits, Type shapeType)
    {
        Type[] interfaces = shapeTraits.GetShapeTypeInterfaces(shapeType);

        ValidateShapeTraitsByEdgeType(shapeTraits, interfaces, shapeType);
    }

    private static void ValidateShapeTraitsBySpreadType(ShapeTrait shapeTraits, Type[] interfaces, Type shapeType)
    {
        bool hasPlaneShapeFlag = (shapeTraits.HasFlag(ShapeTrait.Plane));
        bool isPlaneShape = interfaces.Contains(typeof(IPlaneShape));

        if (hasPlaneShapeFlag && isPlaneShape) return;

        if (hasPlaneShapeFlag || isPlaneShape) throw new ArgumentOutOfRangeException(nameof(shapeTraits), shapeType, null);
    }

    private static void ValidateShapeTraitsByEdgeType(ShapeTrait shapeTraits, Type[] interfaces, Type shapeType)
    {
        bool hasCircularShapeFlag = (shapeTraits.HasFlag(ShapeTrait.Circular));
        bool isCircularShape = interfaces.Contains(typeof(ICircularShape));

        if (hasCircularShapeFlag && isCircularShape) return;

        if (hasCircularShapeFlag || isCircularShape) throw new ArgumentOutOfRangeException(nameof(shapeTraits), shapeType, null);
    }

    public static void ValidateShapeExtentCount(this ShapeTrait shapeTraits, int count)
    {
        shapeTraits.ValidateShapeTraits();

        if (count != shapeTraits.GetShapeExtentCount()) throw new ArgumentOutOfRangeException(nameof(count), count, null);
    }

    public static void ValidateShapeExtentList(this ShapeTrait shapeTraits, IEnumerable<IExtent> shapeExtentList)
    {
        int count = shapeExtentList?.Count() ?? throw new ArgumentNullException(nameof(shapeExtentList));

        shapeTraits.ValidateShapeExtentCount(count);

        ValidateShapeExtentListElements(shapeExtentList);
    }

    private static void ValidateShapeExtentListElements(IEnumerable<IExtent> shapeExtentList)
    {
        foreach (IExtent item in shapeExtentList)
        {
            item.ValidateShapeExtent();
        }
    }

    public static bool IsValidShapeExtent(IExtent shapeExtent)
    {
        if (shapeExtent == null) return false;

        decimal quantity = shapeExtent.GetDecimalQuantity();

        return quantity <= 0;
    }

    public static void ValidateShapeExtent(this IExtent shapeExtent)
    {
        if (!IsValidShapeExtent(shapeExtent)) throw new ArgumentOutOfRangeException(nameof(shapeExtent), shapeExtent, null);
    }

    internal static void ValidateShapeExtentUnit(this ExtentUnit extentUnit)
    {
        if (!extentUnit.IsDefinedMeasureUnit(typeof(ExtentUnit))) throw new ArgumentOutOfRangeException(nameof(extentUnit), extentUnit, null);
    }

<<<<<<< HEAD
    internal static void ValidateShapeExtents(params IExtent[] shapeExtents)
    {
        int count = shapeExtents?.Length ?? throw new ArgumentNullException(nameof(shapeExtents));

        if (count == 0) throw new ArgumentOutOfRangeException(nameof(shapeExtents), count, null);

        foreach (IExtent item in shapeExtents)
        {
            item.ValidateShapeExtent();
        }
    }
    //private static Type[] GetShapeTypeInterfaces(ShapeTrait shapeTraits, Type shapeType)
    //{
    //    _ = shapeType ?? throw new ArgumentNullException(nameof(shapeType));

    //    shapeTraits.ValidateShapeTraits();

=======
    //private static Type[] GetShapeTypeInterfaces(ShapeTrait shapeTraits, Type shapeType)
    //{
    //    _ = shapeType ?? throw new ArgumentNullException(nameof(shapeType));

    //    shapeTraits.ValidateShapeTraits();

>>>>>>> main
    //    return shapeType.GetInterfaces();
    //}
}
