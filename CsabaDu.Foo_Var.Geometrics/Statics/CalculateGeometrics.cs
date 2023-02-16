using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;
using CsabaDu.Foo_Var.Measures.Factories;
using CsabaDu.Foo_Var.Measures.Interfaces.Factories;

namespace CsabaDu.Foo_Var.Geometrics.Statics;

public static class CalculateGeometrics
{
    private static readonly IMeasureFactory MeasureFactory = new MeasureFactory();
    private static readonly decimal MeterSquareExchangeRate = AreaUnit.meterSquare.GetExchangeRate();
    private static readonly decimal MeterCubicExchangeRate = VolumeUnit.meterCubic.GetExchangeRate();

    internal static IEnumerable<IExtent> GetInnerShapeExtentList(this IEnumerable<ICuboid> innerTangentCuboidList)
    {
        List<IExtent> innerShapeExtentList = new();

        foreach (ICuboid item in innerTangentCuboidList)
        {
            innerShapeExtentList.AddRange(item.GetShapeExtentList());
        }

        return innerShapeExtentList;
    }

    internal static IEnumerable<IExtent> GetValidatedEnclosingShapeExtentList(IEnumerable<IExtent> innerShapeExtentList)
    {
        ValidateInnerShapeExtentList(innerShapeExtentList);

        IExtent length = innerShapeExtentList.ElementAt(0);
        IExtent width = innerShapeExtentList.ElementAt(1);
        IExtent height = innerShapeExtentList.ElementAt(2);
        int count = innerShapeExtentList.Count() / CuboidShapeExtentCount;

        if (count == 1) return new List<IExtent>() { length, width, height };

        for (int i = 1; i < count; i++)
        {
            int lengthIndex = i * CuboidShapeExtentCount; // TODO Gondold át
            length = GetComparedShapeExtent(length, innerShapeExtentList.ElementAt(lengthIndex));

            int widthIndex = lengthIndex + 1;
            width = GetComparedShapeExtent(width, innerShapeExtentList.ElementAt(widthIndex));

            int heightIndex = widthIndex + 1;
            height = height.GetExtent(height.SumWith(innerShapeExtentList.ElementAt(heightIndex)));
        }

        return new List<IExtent>() { length, width, height };
    }

    internal static IExtent GetComparedShapeExtent(IExtent firstExtent, IExtent lastExtent, Comparison? comparison = Comparison.Greater)
    {
        bool isFirstExtentShorter = firstExtent.CompareTo(lastExtent) < 0;

        IExtent shorterExtent = isFirstExtentShorter ? firstExtent : lastExtent;
        IExtent longerExtent = isFirstExtentShorter ? lastExtent : firstExtent;

        return comparison switch
        {
            Comparison.Greater => longerExtent,
            Comparison.Less => shorterExtent,

            _ => throw new ArgumentOutOfRangeException(nameof(comparison), comparison, null),
        };
    }

    internal static IArea GetCircleArea(IExtent radius, AreaUnit areaUnit = default)
    {
        radius.ValidateShapeExtent();

        double radiusQuantity = (double)radius.Quantity;

        decimal quantity = GetCircleAreaQuantity(radiusQuantity);
        quantity /= areaUnit.GetMeterSquareExchangeRate(radius);

        return GetArea(quantity, areaUnit);
    }

    internal static IArea GetRectangleArea(IExtent length, IExtent width, AreaUnit areaUnit = AreaUnit.meterSquare)
    {
        ValidateShapeExtents(length, width);

        decimal lengthQuantity = length.GetDecimalQuantity();
        decimal widthQuantity = width.GetDecimalQuantity();

        decimal quantity = GetRectangleAreaQuantity(lengthQuantity, widthQuantity);
        quantity /= areaUnit.GetMeterSquareExchangeRate(length, width);

        return GetArea(quantity, areaUnit);
    }

    internal static IVolume GetCylinderVolume(IExtent radius, IExtent height, VolumeUnit volumeUnit = default)
    {
        ValidateShapeExtents(radius, height);

        double radiusQuantity = (double)radius.Quantity;
        decimal heightQuantity = height.GetDecimalQuantity();

        decimal quantity = GetCylinderVolumeQuantity(radiusQuantity, heightQuantity);
        quantity /= volumeUnit.GetMeterQubicExchangeRate(radius, height);

        return GetVolume(quantity, volumeUnit);
    }

    internal static IVolume GetCuboidVolume(IExtent length, IExtent width, IExtent height, VolumeUnit volumeUnit = VolumeUnit.meterCubic)
    {
        ValidateShapeExtents(length, width, height);

        decimal lengthQuantity = length.GetDecimalQuantity();
        decimal widthQuantity = width.GetDecimalQuantity();
        decimal heightQuantity = height.GetDecimalQuantity();

        decimal quantity = GetCuboidVolumeQuantity(lengthQuantity, widthQuantity, heightQuantity);
        quantity /= volumeUnit.GetMeterQubicExchangeRate(length, width, height);

        return GetVolume(quantity, volumeUnit);
    }

    internal static IExtent GetCircleDiagonal(IExtent radius, ExtentUnit extentUnit)
    {
        return radius.GetExtent(radius.MultipliedBy(2).ExchangeTo(extentUnit));
    }

    internal static IExtent GetRectangleDiagonal(IExtent length, IExtent width, ExtentUnit extentUnit)
    {
        decimal quantity = GetQuantitySquares(length);
        quantity += GetQuantitySquares(width);

        return GetShapeExtentSqrt(quantity, extentUnit);
    }

    internal static IExtent GetCylinderDiagonal(IExtent radius, IExtent height, ExtentUnit extentUnit)
    {
        IExtent baseCircleDiagonal = GetCircleDiagonal(radius, extentUnit);

        return GetRectangleDiagonal(baseCircleDiagonal, height, extentUnit);
    }

    internal static IExtent GetCuboidDiagonal(IExtent length, IExtent width, IExtent height, ExtentUnit extentUnit)
    {
        decimal quantity = GetQuantitySquares(length);
        quantity += GetQuantitySquares(width);
        quantity += GetQuantitySquares(height);

        return GetShapeExtentSqrt(quantity, extentUnit);
    }

    private static decimal GetMeterSquareExchangeRate(params IExtent[] shapeExtents)
    {
        int count = GetShapeExtentsCount(shapeExtents);

        if (count == 0) return MeterSquareExchangeRate;

        ValidateShapeExtents(shapeExtents);

        decimal lengthExchangeRate = shapeExtents.GetExchangeRate(0);

        switch (count)
        {
            case 1:
                return GetMeterSquareExchangeRate(lengthExchangeRate, lengthExchangeRate);
            case 2:
                decimal widthExchangeRate = shapeExtents.GetExchangeRate(1);
                return GetMeterSquareExchangeRate(lengthExchangeRate, widthExchangeRate);

            default:
                throw new ArgumentOutOfRangeException(nameof(shapeExtents), count, null);
        }
    }

    private static decimal GetMeterSquareExchangeRate(decimal lengthExchangeRate, decimal widthExchangeRate)
    {
        lengthExchangeRate *= widthExchangeRate;

        return lengthExchangeRate / MeterSquareExchangeRate;
    }

    private static decimal GetMeterSquareExchangeRate(this AreaUnit areaUnit, params IExtent[] shapeExtents)
    {
        decimal exchangeRate = GetMeterSquareExchangeRate(shapeExtents);

        return areaUnit == AreaUnit.meterSquare ? exchangeRate : exchangeRate / MeterSquareExchangeRate;
    }

    private static decimal GetExchangeRate(this IExtent[] shapeExtents, int index)
    {
        Enum extentUnit = shapeExtents[index].GetMeasureUnit();

        return extentUnit.GetExchangeRate();
    }

    private static decimal GetMeterQubicExchangeRate(params IExtent[] shapeExtents)
    {
        int count = GetShapeExtentsCount(shapeExtents);

        if (count == 0) return MeterCubicExchangeRate;

        ValidateShapeExtents(shapeExtents);

        decimal lengthExchangeRate = shapeExtents.GetExchangeRate(0);
        decimal heightExchangeRate = shapeExtents.GetExchangeRate(count - 1);

        switch (count)
        {
            case 2:
                return GetMeterQubicExchangeRate(lengthExchangeRate, lengthExchangeRate, heightExchangeRate);
            case 3:
                decimal widthExchangeRate = shapeExtents.GetExchangeRate(1);
                return GetMeterQubicExchangeRate(lengthExchangeRate, widthExchangeRate, heightExchangeRate);

            default:
                throw new ArgumentOutOfRangeException(nameof(shapeExtents), count, null);
        }
    }

    private static decimal GetMeterQubicExchangeRate(this VolumeUnit volumeUnit, params IExtent[] shapeExtents)
    {
        decimal exchangeRate = GetMeterQubicExchangeRate(shapeExtents);

        return volumeUnit == VolumeUnit.meterCubic ? exchangeRate : exchangeRate / MeterCubicExchangeRate;
    }

    private static decimal GetMeterQubicExchangeRate(decimal lengthExchangeRate, decimal widthExchangeRate, decimal heightExchangeRate)
    {
        lengthExchangeRate *= widthExchangeRate;
        lengthExchangeRate *= heightExchangeRate;

        return lengthExchangeRate / MeterCubicExchangeRate;
    }

    private static decimal GetCircleAreaQuantity(double radiusQuantity)
    {
        return Convert.ToDecimal(Math.Pow(radiusQuantity, 2) * Math.PI);
    }

    private static int GetShapeExtentsCount(IExtent[]? shapeExtents)
    {
        if (shapeExtents == null) return 0;

        return shapeExtents.Length;
    }

    private static decimal GetRectangleAreaQuantity(decimal lengthQuantity, decimal widthQuantity)
    {
        return decimal.Multiply(lengthQuantity, widthQuantity);
    }

    private static IArea GetArea(decimal quantity, AreaUnit areaUnit)
    {
        return (IArea)MeasureFactory.GetMeasure(quantity, areaUnit);
    }

    private static decimal GetCylinderVolumeQuantity(double radiusQuantity, decimal heightQuantity)
    {
        decimal basePlateQuantity = GetCircleAreaQuantity(radiusQuantity);

        return GetGeometricBodyVolumeQuantity(basePlateQuantity, heightQuantity);
    }

    private static decimal GetCuboidVolumeQuantity(decimal lengthQuantity, decimal widthQuantity, decimal heightQuantity)
    {
        decimal basePlateQuantity = GetRectangleAreaQuantity(lengthQuantity, widthQuantity);

        return GetGeometricBodyVolumeQuantity(basePlateQuantity, heightQuantity);
    }

    private static decimal GetGeometricBodyVolumeQuantity(decimal basePlateQuantity, decimal heightQuantity)
    {
        return basePlateQuantity * heightQuantity;
    }

    private static IVolume GetVolume(decimal quantity, VolumeUnit volumeUnit)
    {
        return (IVolume)MeasureFactory.GetMeasure(quantity, volumeUnit);
    }

    private static decimal GetQuantitySquares(IExtent shapeExtent)
    {
        decimal quantity = shapeExtent.GetDecimalQuantity() * shapeExtent.GetExchangeRate();

        return quantity * quantity;
    }

    private static IExtent GetShapeExtentSqrt(decimal quantity, ExtentUnit extentUnit)
    {
        quantity = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(quantity)));
        quantity /= extentUnit.GetExchangeRate();

        return (IExtent)MeasureFactory.GetMeasure(quantity, extentUnit);
    }
}
