using CsabaDu.Foo_Var.Geometrics.Factories;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Spread;
using CsabaDu.Foo_Var.Geometrics.Interfaces.Factories;

namespace CsabaDu.Foo_Var.Geometrics.DataTypes.Spread;

internal abstract class Spread<T, U> : ISpread<T, U> where T : IMeasure where U : struct, Enum
{
<<<<<<< HEAD
    public ISpreadFactory SpreadFactory { get; init; }

    private protected Spread(ISpreadFactory spreadFactory)
    {
        SpreadFactory = spreadFactory ?? throw new ArgumentNullException(nameof(spreadFactory));
    }
=======
    //public ISpreadFactory BodyFactory { get; init; }

    //private protected Spread()
    //{
    //    BodyFactory = new BodyFactory();
    //}
>>>>>>> main

    public int CompareTo(ISpread<T, U>? other)
    {
        if (other == null) return 1;

        return GetSpreadMeasure().CompareTo(other.GetSpreadMeasure());
    }

    public  bool Equals(ISpread<T, U>? other)
    {
        return other is ISpread<T, U> spread && GetSpreadMeasure().Equals(spread.GetSpreadMeasure()); 
    }

    public ISpread<T, U>? ExchangeTo(U spreadMeasureUnit)
    {
        IBaseMeasure? exchanged = GetSpreadMeasure().ExchangeTo(spreadMeasureUnit);

        if (exchanged == null) return null;

        T spreadMeasure = (T)GetSpreadMeasure().GetMeasure(exchanged);

        return GetSpread(spreadMeasure);
    }

    public bool? FitsIn(ISpread<T, U>? other = null, LimitType? limitType = null)
    {
        if (other == null) return null;

        limitType ??= LimitType.BeNotGreater;

        return GetSpreadMeasure().FitsIn(other.GetSpreadMeasure(), limitType);
    }

    public ISpread<T, U> GetSpread(U? spreadMeasureUnit = null)
    {
        if (spreadMeasureUnit is not U measureUnit) return this;

        return ExchangeTo(measureUnit) ?? throw new ArgumentOutOfRangeException(nameof(spreadMeasureUnit), spreadMeasureUnit, null);
    }

    public T GetSpreadMeasure(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits, U? spreadMeasureUnit = null)
    {
        shapeTraits.ValidateShapeTraits();
        shapeTraits.ValidateShapeExtentList(shapeExtentList);

        IExtent firstShapeExtent = shapeExtentList.First();
        IExtent lastShapeExtent = shapeExtentList.Last();

        if (shapeTraits.HasFlag(ShapeTrait.Plane) && (spreadMeasureUnit == null || spreadMeasureUnit is AreaUnit))
        {
            return GetPlaneShapeArea(spreadMeasureUnit, firstShapeExtent, lastShapeExtent, shapeTraits);
        }
        
        if (spreadMeasureUnit == null || spreadMeasureUnit is VolumeUnit)
        {
            IExtent secondShapeExtent = shapeExtentList.ElementAt(1);

            return GetGeometricBodyVolume(spreadMeasureUnit, firstShapeExtent, secondShapeExtent, lastShapeExtent, shapeTraits);
        }

        throw new ArgumentOutOfRangeException(nameof(spreadMeasureUnit), spreadMeasureUnit, null);
    }

    private static T GetPlaneShapeArea(U? spreadMeasureUnit, IExtent firstShapeExtent, IExtent lastShapeExtent, ShapeTrait shapeTraits)
    {
        Enum measureUnit = spreadMeasureUnit ?? (Enum)AreaUnit.meterSquare;

        if (shapeTraits.HasFlag(ShapeTrait.Circular)) return (T)GetCircleArea(firstShapeExtent, (AreaUnit)measureUnit);

        return (T)GetRectangleArea(firstShapeExtent, lastShapeExtent, (AreaUnit)measureUnit);
    }

    private static T GetGeometricBodyVolume(U? spreadMeasureUnit, IExtent firstShapeExtent, IExtent secondShapeExtent, IExtent lastShapeExtent, ShapeTrait shapeTraits)
    {
        Enum measureUnit = spreadMeasureUnit ?? (Enum)VolumeUnit.meterCubic;

        if (shapeTraits.HasFlag(ShapeTrait.Circular)) return (T)GetCylinderVolume(firstShapeExtent, lastShapeExtent, (VolumeUnit)measureUnit);

        return (T)GetCuboidVolume(firstShapeExtent, secondShapeExtent, lastShapeExtent, (VolumeUnit)measureUnit);
    }

    public bool IsExchangeableTo(U spreadMeasureUnit)
    {
        return GetSpreadMeasure().IsExchangeableTo(spreadMeasureUnit);
    }

    public decimal ProportionalTo(ISpread<T, U>? other)
    {
        _ = other ?? throw new ArgumentNullException(nameof(other));

        return GetSpreadMeasure().ProportionalTo(other.GetSpreadMeasure());
    }

    public bool TryExchangeTo(U spreadMeasureUnit, [NotNullWhen(true)] out ISpread<T, U>? exchanged)
    {
        exchanged = ExchangeTo(spreadMeasureUnit);

        return exchanged != null;
    }

    public abstract T GetSpreadMeasure(U? spreadMeasureUnit = null);

    public void ValidateSpreadMeasure(T spreadMeasure)
    {
        decimal quantity = spreadMeasure?.GetDecimalQuantity() ?? throw new ArgumentNullException(nameof(spreadMeasure));

        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(spreadMeasure), quantity, null);
    }

    public abstract ISpread<T, U> GetSpread(T spreadMeasure);
    public abstract ISpread<T, U> GetSpread(ISpread<T, U> spread);
    public abstract ISpread<T, U> GetSpread(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits);
    public abstract ISpread<T, U> GetSpread(IShape shape);
}
