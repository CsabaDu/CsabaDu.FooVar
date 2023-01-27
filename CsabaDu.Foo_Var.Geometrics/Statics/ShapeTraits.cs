using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;
using System.Collections.Immutable;

namespace CsabaDu.Foo_Var.Geometrics.Statics;

public static class ShapeTraits
{
    [Flags]
    public enum ShapeTrait
    {
        None = 0,
        Plane = 1,
        Circular = 2,
    }

    public enum ShapeExtentType : byte { Radius, Length, Width, Height }

    public static SortedList<ShapeTrait, ImmutableSortedSet<ShapeExtentType>> ShapeExtentTypeSetList =>
        new()
        {
            { 0, GetShapeExtentTypeSet(ShapeTrait.None) },
            { (ShapeTrait)1, GetShapeExtentTypeSet(ShapeTrait.Plane) },
            { (ShapeTrait)2, GetShapeExtentTypeSet(ShapeTrait.Circular) },
            { (ShapeTrait)3, GetShapeExtentTypeSet(ShapeTrait.Plane | ShapeTrait.Circular) },
        };

    public static ImmutableSortedSet<ShapeExtentType> GetShapeExtentTypeSet(ShapeTrait shapeTraits = ShapeTrait.None)
    {
        shapeTraits.ValidateShapeTraits();

        ISet<ShapeExtentType> shapeExtentTypeSet = ImmutableSortedSet<ShapeExtentType>.Empty;

        if (shapeTraits.HasFlag(ShapeTrait.Circular))
        {
            shapeExtentTypeSet.Add(ShapeExtentType.Radius);
        }
        else
        {
            shapeExtentTypeSet.Add(ShapeExtentType.Length);
            shapeExtentTypeSet.Add(ShapeExtentType.Width);
        }

        if (!shapeTraits.HasFlag(ShapeTrait.Plane))
        {
            shapeExtentTypeSet.Add(ShapeExtentType.Height);
        }

        return shapeExtentTypeSet.ToImmutableSortedSet();
    }

    public static ImmutableSortedSet<ShapeExtentType> GetShapeExtentTypeSet(Type shapeType)
    {
        _ = shapeType ?? throw new ArgumentNullException(nameof(shapeType));

        Type[] interfaces = shapeType.GetInterfaces();

        if (!interfaces.Contains(typeof(IShape))) throw new ArgumentOutOfRangeException(nameof(shapeType), shapeType, null);

        ISet<ShapeExtentType> shapeExtentTypeSet = ImmutableSortedSet<ShapeExtentType>.Empty;

        AddShapeExtentTypesByEdgeType(shapeExtentTypeSet, interfaces, shapeType);
        AddShapeExtentTypesBySpreadType(shapeExtentTypeSet, interfaces, shapeType);

        return shapeExtentTypeSet.ToImmutableSortedSet();
    }

    private static void AddShapeExtentTypesByEdgeType(ISet<ShapeExtentType> shapeExtentTypeSet, Type[] interfaces, Type shapeType)
    {
        if (interfaces.Contains(typeof(ICircularShape)) && interfaces.Contains(typeof(IRectangularShape)))
        {
            throw new ArgumentOutOfRangeException(nameof(shapeType), shapeType, null);
        }
        else if (interfaces.Contains(typeof(ICircularShape)))
        {
            shapeExtentTypeSet.Add(ShapeExtentType.Radius);
        }
        else if (interfaces.Contains(typeof(IRectangularShape)))
        {
            shapeExtentTypeSet.Add(ShapeExtentType.Length);
            shapeExtentTypeSet.Add(ShapeExtentType.Width);
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(shapeType), shapeType, null);
        }
    }

    private static void AddShapeExtentTypesBySpreadType(ISet<ShapeExtentType> shapeExtentTypeSet, Type[] interfaces, Type shapeType)
    {
        if (interfaces.Contains(typeof(IGeometricBody)) && interfaces.Contains(typeof(IPlaneShape)))
        {
            throw new ArgumentOutOfRangeException(nameof(shapeType), shapeType, null);
        }
        else if (interfaces.Contains(typeof(IGeometricBody)))
        {
            shapeExtentTypeSet.Add(ShapeExtentType.Height);
        }
        else if (!interfaces.Contains(typeof(IPlaneShape)))
        {
            throw new ArgumentOutOfRangeException(nameof(shapeType), shapeType, null);
        }
    }

    public static Type GetShapeType(this ShapeTrait shapeTraits)
    {
        if (shapeTraits.Equals(ShapeTrait.None)) return typeof(ICuboid);

        if (shapeTraits.HasFlag(ShapeTrait.Plane) && shapeTraits.HasFlag(ShapeTrait.Circular)) return typeof(ICylinder);

        if (shapeTraits.HasFlag(ShapeTrait.Plane)) return typeof(IRectangle);

        if (shapeTraits.HasFlag(ShapeTrait.Circular)) return typeof(ICircle);

        return typeof(IShape);
    }

    public static ShapeTrait GetShapeTraits(Type shapeType)
    {
        _ = shapeType ?? throw new ArgumentNullException(nameof(shapeType));

        return shapeType switch
        {
            ICuboid => ShapeTrait.None,
            IRectangle => ShapeTrait.Plane,
            ICylinder => ShapeTrait.Circular,
            ICircle => ShapeTrait.Plane | ShapeTrait.Circular,

            _ => throw new ArgumentOutOfRangeException(nameof(shapeType), shapeType, null),
        };
    }

    public static int GetShapeExtentCount(this ShapeTrait shapeTraits)
    {
        if (shapeTraits.Equals(ShapeTrait.None)) return 3; // Cuboid

        if (!shapeTraits.HasFlag(ShapeTrait.Plane)) return 2;  // Cylinder

        if (shapeTraits.HasFlag(ShapeTrait.Circular)) return 1; // Circle

        return 2; // Rectangle
    }

    internal static Type[] GetShapeTypeInterfaces(this ShapeTrait shapeTraits, Type shapeType)
    {
        _ = shapeType ?? throw new ArgumentNullException(nameof(shapeType));

        shapeTraits.ValidateShapeTraits();

        return shapeType.GetInterfaces();
    }
}
