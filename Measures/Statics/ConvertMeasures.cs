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

    public static ValueType? ToQuantity(this ValueType quantity, TypeCode conversionTypeCode)
    {
        Type quantityType = quantity.GetType();
        TypeCode quantityTypeCode = Type.GetTypeCode(quantityType);

        if (conversionTypeCode == quantityTypeCode) return quantity.GetRoundedQuantity();

        try
        {
            return conversionTypeCode switch
            {
                TypeCode.Int32 => Convert.ToInt32(quantity),
                TypeCode.UInt32 => Convert.ToDouble(quantity) < 0 ? null : Convert.ToUInt32(quantity),
                TypeCode.Int64 => Convert.ToInt64(quantity),
                TypeCode.UInt64 => Convert.ToDouble(quantity) < 0 ? null : Convert.ToUInt64(quantity),
                TypeCode.Double => Math.Round(Convert.ToDouble(quantity), 8),
                TypeCode.Decimal => decimal.Round(Convert.ToDecimal(quantity), 8),

                _ => null,
            };
        }
        catch (OverflowException)
        {
            return null;
        }
        catch (Exception)
        {
            return null; // Log?
        }
    }

    public static ValueType? ToQuantity(this ValueType quantity, Type conversionType)
    {
        TypeCode conversionTypeCode = Type.GetTypeCode(conversionType);

        return quantity.ToQuantity(conversionTypeCode);
    }

    private static ValueType GetRoundedQuantity(this ValueType quantity)
    {
        return Type.GetTypeCode(quantity.GetType()) switch
        {
            TypeCode.Double => Math.Round((double)quantity, 8),
            TypeCode.Decimal => decimal.Round((decimal)quantity, 8),

            _ => quantity,
        };
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
        decimal quantity = (decimal)time.GetQuantity(TypeCode.Decimal);

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
        decimal quantity = (decimal)distance.GetQuantity(TypeCode.Decimal);

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
        decimal quantity = (decimal)extent.GetQuantity(TypeCode.Decimal);

        decimal exchangeRate = extent.GetExchangeRate() / DistancePerExtent;

        if (distanceUnit != DistanceUnit.meter)
        {
            exchangeRate *= distanceUnit.GetExchangeRate();
        }

        quantity *= exchangeRate;

        return new Distance(quantity, distanceUnit);
    }
}
