using CsabaDu.Foo_Var.Geometrics.DataTypes.Shape.ShapeTypes;
using CsabaDu.Foo_Var.Geometrics.Factories;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;
using CsabaDu.Foo_Var.Geometrics.Interfaces.Factories;
using CsabaDu.Foo_Var.Geometrics.Interfaces.Factories.Shape;
using System.Collections.Immutable;

namespace CsabaDu.Foo_Var.Geometrics.DataTypes.Shape;

internal abstract class Shape : IShape
{
    public ShapeTrait ShapeTraits { get; init; }
    public ImmutableSortedSet<ShapeExtentType> ShapeExtentTypeSet { get; init; }
    public int ShapeExtentTypeCount { get; init; }
    public IShapeFactory ShapeFactory { get; init; }

    private protected Shape(ShapeTrait shapeTraits)
    {
        ValidateShapeTraits(shapeTraits);

        ShapeTraits = shapeTraits;
        ShapeExtentTypeSet = ShapeExtentTypeSetList[shapeTraits];
        ShapeExtentTypeCount = ShapeExtentTypeSet.Count;
        ShapeFactory = new ShapeFactory();
    }

    private protected Shape(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits) : this(shapeTraits)
    {
        ValidateShapeExtentList(shapeExtentList, shapeTraits);
    }

    public int CompareTo(IShape? other)
    {
        if (other == null) return 1;

        if (ShapeTraits != other.ShapeTraits) throw new ArgumentOutOfRangeException(nameof(other), other.GetType(), null);

        IEnumerable<IExtent> otherShapeExtentList = other.GetShapeExtentList();

        return CompareTo(otherShapeExtentList) ?? throw new ArgumentException(null, nameof(other));
    }

    public bool Equals(IShape? other)
    {
        if (other == null) return false;

        if (other.ShapeTraits != ShapeTraits) return false;

        IEnumerable<IExtent> shapeExtentList = other.GetShapeExtentList();

        for (int i = 0; i < shapeExtentList.Count(); i++)
        {
            if (shapeExtentList.ElementAt(i) != GetShapeExtentList().ElementAt(i)) return false;
        }

        return true;
    }

    public IShape? ExchangeTo(ExtentUnit extentUnit)
    {
        List<IExtent> shapeExtentList = new();

        foreach (IExtent item in GetShapeExtentList())
        {
            IBaseMeasure exchanged = item.ExchangeTo(extentUnit)!;
            IExtent shapeExtent = item.GetExtent(exchanged);
            shapeExtentList.Add(shapeExtent);
        }

        return GetShape(shapeExtentList, ShapeTraits);
    }

    public bool? FitsIn(IShape? other = null, LimitType? limitType = null)
    {
        if (other == null) return null;

        ShapeTrait otherShapeTraits = other.ShapeTraits;

        if (otherShapeTraits.HasFlag(ShapeTrait.Plane) != ShapeTraits.HasFlag(ShapeTrait.Plane)) return null;

        limitType ??= LimitType.BeNotGreater;

        Side shapeSide = limitType == LimitType.BeNotLess || limitType == LimitType.BeGreater ?
            Side.Outer
            : Side.Inner;

        IEnumerable<IExtent> otherShapeExtentList = otherShapeTraits == ShapeTraits ?
            other.GetShapeExtentList()
            : other.GetTangentShape(shapeSide).GetShapeExtentList();

        int? nullableComparison = CompareTo(otherShapeExtentList);

        if (nullableComparison is not int comparison) return false;

        return comparison.FitsIn(limitType);
    }

    public IExtent GetDiagonal(IShape shape, ExtentUnit extentUnit)
    {
        extentUnit.ValidateShapeExtentUnit();

        IEnumerable<IExtent> shapeExtentList = shape.GetShapeExtentList();
        IExtent firstShapeExtent = shapeExtentList.First();
        IExtent lastShapeExtent = shapeExtentList.Last();
        ShapeTrait shapeTraits = shape.ShapeTraits;

        if (!shapeTraits.Equals(ShapeTrait.None))
        {
            if (shapeTraits.HasFlag(ShapeTrait.Plane | ShapeTrait.Round))
            {
                return GetCircleDiagonal(firstShapeExtent, extentUnit);
            }

            if (shapeTraits.HasFlag(ShapeTrait.Plane))
            {
                return GetRectangleDiagonal(firstShapeExtent, lastShapeExtent, extentUnit);
            }

            if (shapeTraits.HasFlag(ShapeTrait.Round))
            {
                return GetCylinderDiagonal(firstShapeExtent, lastShapeExtent, extentUnit);
            }
        }

        IExtent secondShapeExtent = shapeExtentList.ElementAt(1);

        return GetCuboidDiagonal(firstShapeExtent, secondShapeExtent, lastShapeExtent, extentUnit);
    }

    public IShape GetShape(ExtentUnit? extentUnit = null)
    {
        if (extentUnit is not ExtentUnit measureUnit) return this;

        return ExchangeTo(measureUnit) ?? throw new ArgumentOutOfRangeException(nameof(extentUnit), extentUnit, null);
    }

    public IShape GetShape(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits)
    {
        return ShapeFactory.GetShape(shapeExtentList, shapeTraits);
    }

    public IShape GetShape(IShape other)
    {
        _ = other ?? throw new ArgumentNullException(nameof(other));

        return GetShape(other.GetShapeExtentList(), other.ShapeTraits);
    }

    public IExtent GetShapeExtent(ShapeExtentType shapeExtentType)
    {
        if (!ShapeExtentTypeSet.Contains(shapeExtentType)) throw new ArgumentOutOfRangeException(nameof(shapeExtentType), shapeExtentType, null);

        int index = ShapeExtentTypeSet.TakeWhile(x => x == shapeExtentType).Count() - 1;

        return GetShapeExtentList().ElementAt(index);
    }

    public Type GetShapeType() => ShapeTraits.GetShapeType();

    public Type GetShapeType(ShapeTrait shapeTraits) => shapeTraits.GetShapeType();

    public ShapeTrait GetShapeTraits(Type shapeType)
    {
        _ = shapeType ?? throw new ArgumentNullException(nameof(shapeType));

        return shapeType switch
        {
            ICuboid => ShapeTrait.None,
            ICircle => ShapeTrait.Plane | ShapeTrait.Round,
            ICylinder => ShapeTrait.Round,
            IRectangle => ShapeTrait.Plane,

            _ => throw new ArgumentOutOfRangeException(nameof(shapeType), shapeType, null),
        };
    }

    public bool TryExchangeTo(ExtentUnit extentUnit, [NotNullWhen(true)] out IShape? exchanged)
    {
        exchanged = ExchangeTo(extentUnit);

        return exchanged != null;
    }

    public void ValidateShapeExtent(IExtent shapeExtent)
    {
        decimal quantity = shapeExtent.GetDecimalQuantity();

        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(shapeExtent), quantity, null);
    }

    public void ValidateShapeExtentCount(int count, ShapeTrait? shapeTraits = null)
    {
        if (shapeTraits is not ShapeTrait notNullShapeTraits)
        {
            notNullShapeTraits = ShapeTraits;
        }

        notNullShapeTraits.ValidateShapeExtentCount(count);
    }

    public void ValidateShapeExtentList(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits)
    {
        shapeTraits.ValidateShapeExtentList(shapeExtentList);
    }

    public void ValidateShapeExtents(params IExtent[] shapeExtents)
    {
        ValidateShapeExtentList(shapeExtents.ToList(), ShapeTraits);
    }

    public void ValidateShapeType(Type shapeType)
    {
        _ = shapeType ?? throw new ArgumentNullException(nameof(shapeType));

        if (!shapeType.GetInterfaces().Contains(GetShapeType())) throw new ArgumentOutOfRangeException(nameof(shapeType), shapeType, null);
    }

    public void ValidateShapeTraits(ShapeTrait shapeTraits)
    {
        shapeTraits.ValidateShapeTraits(GetShapeType());
    }

    private int? CompareTo(IEnumerable<IExtent> otherShapeExtentList)
    {
        IEnumerable<IExtent> shapeExtentList = GetShapeExtentList();
        int baseComparison = shapeExtentList.First().CompareTo(otherShapeExtentList.First());
        int count = ShapeExtentTypeCount;

        if (count == 1) return baseComparison;

        for (int i = 1; i < count; i++)
        {
            IExtent other = otherShapeExtentList.ElementAt(i);
            int recentComparison = shapeExtentList.ElementAt(i).CompareTo(other);

            if (recentComparison != 0)
            {
                if (baseComparison == 0) baseComparison = recentComparison;

                if (recentComparison != baseComparison) return null;
            }
        }

        return baseComparison;
    }

    public abstract IExtent GetDiagonal(ExtentUnit extentUnit = ExtentUnit.meter);
    public abstract IShape GetShape(params IExtent[] shapeExtents);
    public abstract IReadOnlyList<IExtent> GetShapeExtentList();
    public abstract IShape GetTangentShape(Side shapeSide = Side.Outer);
}
