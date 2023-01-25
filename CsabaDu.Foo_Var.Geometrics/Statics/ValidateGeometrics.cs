using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.Foo_Var.Geometrics.Statics;

public static class ValidateGeometrics
{
    private static ShapeTrait AllShapeTraits => ShapeTrait.Plane | ShapeTrait.Round;

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
        Type[] interfaces = GetShapeTypeInterfaces(shapeTraits, shapeType);

        ValidateShapeTraitsBySpreadType(shapeTraits, interfaces, shapeType);
    }

    public static void ValidateShapeTraitsByEdgeType(this ShapeTrait shapeTraits, Type shapeType)
    {
        Type[] interfaces = GetShapeTypeInterfaces(shapeTraits, shapeType);

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
        bool hasCircularShapeFlag = (shapeTraits.HasFlag(ShapeTrait.Round));
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

    private static Type[] GetShapeTypeInterfaces(ShapeTrait shapeTraits, Type shapeType)
    {
        _ = shapeType ?? throw new ArgumentNullException(nameof(shapeType));

        shapeTraits.ValidateShapeTraits();

        return shapeType.GetInterfaces();
    }
}
