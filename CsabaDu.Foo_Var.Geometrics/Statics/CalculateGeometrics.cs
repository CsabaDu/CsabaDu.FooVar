﻿using CsabaDu.Foo_Var.Measures.Factories;

namespace CsabaDu.Foo_Var.Geometrics.Statics;

public static class CalculateGeometrics
{
    private static readonly MeasureFactory MeasureFactory = new();
    private static readonly decimal MeterSquareExchangeRate = AreaUnit.meterSquare.GetExchangeRate();
    private static readonly decimal MeterCubicExchangeRate = VolumeUnit.meterCubic.GetExchangeRate();

    internal static IArea GetCircleArea(IExtent radius, AreaUnit areaUnit = default)
    {
        _ = radius ?? throw new ArgumentNullException(nameof(radius));

        double radiusQuantity = (double)radius.Quantity;

        decimal quantity = GetCircleAreaQuantity(radiusQuantity);
        quantity /= areaUnit.GetMeterSquareExchangeRate(radius);

        return GetArea(quantity, areaUnit);
    }

    internal static IArea GetRectangleArea(IExtent length, IExtent width, AreaUnit areaUnit = AreaUnit.meterSquare)
    {
        length.ValidateShapeExtent();
        width.ValidateShapeExtent();

        decimal lengthQuantity = length.GetDecimalQuantity();
        decimal widthQuantity = width.GetDecimalQuantity();

        decimal quantity = GetRectangleAreaQuantity(lengthQuantity, widthQuantity);
        quantity /= areaUnit.GetMeterSquareExchangeRate(length, width);

        return GetArea(quantity, areaUnit);
    }

    internal static IVolume GetDrumVolume(IExtent radius, IExtent height, VolumeUnit volumeUnit = default)
    {
        radius.ValidateShapeExtent();
        height.ValidateShapeExtent();

        double radiusQuantity = (double)radius.Quantity;
        decimal heightQuantity = height.GetDecimalQuantity();

        decimal quantity = GetDrumVolumeQuantity(radiusQuantity, heightQuantity);
        quantity /= volumeUnit.GetMeterQubicExchangeRate(radius, height);

        return GetVolume(quantity, volumeUnit);
    }

    internal static IVolume GetBoxVolume(IExtent length, IExtent width, IExtent height, VolumeUnit volumeUnit = VolumeUnit.meterCubic)
    {
        length.ValidateShapeExtent();
        width.ValidateShapeExtent();
        height.ValidateShapeExtent();

        decimal lengthQuantity = length.GetDecimalQuantity();
        decimal widthQuantity = width.GetDecimalQuantity();
        decimal heightQuantity = height.GetDecimalQuantity();

        decimal quantity = GetBoxVolumeQuantity(lengthQuantity, widthQuantity, heightQuantity);
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

    internal static IExtent GetDrumDiagonal(IExtent radius, IExtent height, ExtentUnit extentUnit)
    {
        IExtent baseCircleDiagonal = GetCircleDiagonal(radius, extentUnit);

        return GetRectangleDiagonal(baseCircleDiagonal, height, extentUnit);
    }

    internal static IExtent GetBoxDiagonal(IExtent length, IExtent width, IExtent height, ExtentUnit extentUnit)
    {
        decimal quantity = GetQuantitySquares(length);
        quantity += GetQuantitySquares(width);
        quantity += GetQuantitySquares(height);

        return GetShapeExtentSqrt(quantity, extentUnit);
    }

    private static decimal GetMeterSquareExchangeRate(params IExtent[] shapeExtents)
    {
        if (shapeExtents == null) return MeterSquareExchangeRate;

        int count = shapeExtents.Length;

        if (count == 0) return MeterSquareExchangeRate;

        foreach (IExtent item in shapeExtents)
        {
            item.ValidateShapeExtent();
        }

        Enum extentUnit = shapeExtents[0].GetMeasureUnit();
        decimal exchangeRate = extentUnit.GetExchangeRate();

        switch (count)
        {
            case 1:
                exchangeRate *= exchangeRate;
                return exchangeRate / MeterSquareExchangeRate;
            case 2:
                Enum otherExtentUnit = shapeExtents[1].GetMeasureUnit();
                decimal otherExchangeRate = otherExtentUnit.GetExchangeRate();
                exchangeRate *= otherExchangeRate;
                return exchangeRate / MeterSquareExchangeRate;

            default:
                throw new ArgumentOutOfRangeException(nameof(shapeExtents), count, null);
        }
    }

    private static decimal GetMeterSquareExchangeRate(this AreaUnit areaUnit, params IExtent[] shapeExtents)
    {
        decimal exchangeRate = GetMeterSquareExchangeRate(shapeExtents);

        return areaUnit == AreaUnit.meterSquare ? exchangeRate : exchangeRate / MeterSquareExchangeRate;
    }

    private static decimal GetMeterQubicExchangeRate(params IExtent[] shapeExtents)
    {
        if (shapeExtents == null) return MeterCubicExchangeRate;

        int count = shapeExtents.Length;

        if (count == 0) return MeterCubicExchangeRate;

        foreach (IExtent item in shapeExtents)
        {
            item.ValidateShapeExtent();
        }

        Enum extentUnit = shapeExtents[0].GetMeasureUnit();
        decimal exchangeRate = extentUnit.GetExchangeRate();
        Enum heightExtentUnit = shapeExtents[count - 1].GetMeasureUnit();

        switch (count)
        {
            case 2:
                exchangeRate *= exchangeRate;
                exchangeRate *= heightExtentUnit == extentUnit ? exchangeRate : heightExtentUnit.GetExchangeRate();
                return exchangeRate / MeterCubicExchangeRate;
            case 3:
                exchangeRate *= heightExtentUnit == extentUnit ? exchangeRate : heightExtentUnit.GetExchangeRate();
                Enum widthExtentUnit = shapeExtents[1].GetMeasureUnit();
                exchangeRate *= widthExtentUnit == extentUnit ? exchangeRate : widthExtentUnit.GetExchangeRate();
                return exchangeRate / MeterCubicExchangeRate;

            default:
                throw new ArgumentOutOfRangeException(nameof(shapeExtents), count, null);
        }
    }

    private static decimal GetMeterQubicExchangeRate(this VolumeUnit volumeUnit, params IExtent[] shapeExtents)
    {
        decimal exchangeRate = GetMeterQubicExchangeRate(shapeExtents);

        return volumeUnit == VolumeUnit.meterCubic ? exchangeRate : exchangeRate / MeterCubicExchangeRate;
    }

    private static decimal GetCircleAreaQuantity(double radiusQuantity)
    {
        return Convert.ToDecimal(Math.Pow(radiusQuantity, 2) * Math.PI);
    }

    private static decimal GetRectangleAreaQuantity(decimal lengthQuantity, decimal widthQuantity)
    {
        return decimal.Multiply(lengthQuantity, widthQuantity);
    }

    private static IArea GetArea(decimal quantity, AreaUnit areaUnit)
    {
        return (IArea)MeasureFactory.GetMeasure(quantity, areaUnit);
    }

    private static decimal GetDrumVolumeQuantity(double radiusQuantity, decimal heightQuantity)
    {
        decimal basePlateQuantity = GetCircleAreaQuantity(radiusQuantity);

        return GetGeometricBodyVolumeQuantity(basePlateQuantity, heightQuantity);
    }

    private static decimal GetBoxVolumeQuantity(decimal lengthQuantity, decimal widthQuantity, decimal heightQuantity)
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

        return (IExtent)new MeasureFactory().GetMeasure(quantity, extentUnit);
    }
}
