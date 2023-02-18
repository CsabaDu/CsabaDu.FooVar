using CsabaDu.FooVar.Measures.DataTypes.MeasureTypes;
using CsabaDu.FooVar.Measures.Factories;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes;
using CsabaDu.FooVar.Measures.Interfaces.DataTypes.MeasureTypes;
using CsabaDu.FooVar.Measures.Interfaces.Factories;

namespace CsabaDu.FooVar.Measures.Statics;

public static class ConvertMeasures
{
    private static IRateFactory RateFactory => new RateFactory(new MeasureFactory());

    private const decimal DistancePerExtent = 1000m;

    public static ValueType? ToQuantity(this ValueType quantity, Type type)
    {
        Type quantityType = quantity.GetType();

        quantity = quantity.GetRoundedQuantity(quantityType);

        if (type == quantityType) return quantity;

        if (type == null) return quantityType.IsValidQuantityType() ? quantity : null;

        if (!ValidateMeasures.IsValidQuantityParamType(quantityType)) return null;

        try
        {
            return type.FullName switch
            {
                "System.Int32" => Convert.ToInt32(quantity),
                "System.UInt32" => Convert.ToUInt32(quantity),
                "System.Int64" => Convert.ToInt64(quantity),
                "System.UInt64" => Convert.ToUInt64(quantity),
                "System.Double" => Math.Round(Convert.ToDouble(quantity), 8),
                "System.Decimal" => decimal.Round(Convert.ToDecimal(quantity), 8),

                _ => null,
            };
        }
        catch (Exception)
        {
            return null;
        }
    }

    private static ValueType GetRoundedQuantity(this ValueType quantity, Type quantityType)
    {
        if (quantityType == typeof(decimal)) return decimal.Round(Convert.ToDecimal(quantity), 8);

        if (quantityType == typeof(double)) return Math.Round(Convert.ToDouble(quantity), 8);

        return quantity;
    }

    public static IFlatRate ToFlatRate(this IRate rate)
    {
        return RateFactory.GetFlatRate(rate);
    }

    public static ILimitedRate ToLimitedRate(this IRate rate, ILimit? limit = null)
    {
        IFlatRate flatRate = rate.ToFlatRate();

        return RateFactory.GetLimitedRate(flatRate, limit ?? rate.GetLimit());
    }

    public static TimeSpan ToTimeSpan(this ITime time)
    {
        decimal quantity = (decimal)time.GetQuantity(typeof(decimal));

        if ((TimeUnit)time.GetMeasureUnit() != TimeUnit.minute)
        {
            quantity *= time.GetExchangeRate();
        }

        long ticks = Convert.ToInt64(quantity) * TimeSpan.TicksPerMinute;

        return new TimeSpan(ticks);
    }

    public static ITime ToTime(this TimeSpan timeSpan, TimeUnit timeUnit = default)
    {
        double quantity = timeSpan.TotalMinutes;

        if (timeUnit == TimeUnit.minute) return new Time(quantity, timeUnit);

        decimal decimalQuantity = (decimal)quantity.ToQuantity(typeof(decimal))!;

        decimal exchangeRate = timeUnit.GetExchangeRate();

        decimalQuantity /= exchangeRate;

        return new Time(decimalQuantity, timeUnit);
    }

    public static IExtent ToExtent(this IDistance distance, ExtentUnit extentUnit = default)
    {
        decimal quantity = (decimal)distance.GetQuantity(typeof(decimal));

        decimal exchangeRate = distance.GetExchangeRate() * DistancePerExtent;

        if (extentUnit != ExtentUnit.meter)
        {
            exchangeRate /= extentUnit.GetExchangeRate();
        }

        quantity /= exchangeRate;

        return new Extent(quantity, extentUnit);
    }

    public static IDistance ToDistance(this IExtent extent, DistanceUnit distanceUnit)
    {
        decimal quantity = (decimal)extent.GetQuantity(typeof(decimal));

        decimal exchangeRate = extent.GetExchangeRate() / DistancePerExtent;

        if (distanceUnit != DistanceUnit.meter)
        {
            exchangeRate *= distanceUnit.GetExchangeRate();
        }

        quantity *= exchangeRate;

        return new Distance(quantity, distanceUnit);
    }
}
