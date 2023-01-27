using CsabaDu.Foo_Var.Measures.DataTypes.MeasureTypes;
using System.Diagnostics.CodeAnalysis;

namespace CsabaDu.Foo_Var.Measures.Statics;

public static class ExchangeMeasures
{
    #region Properties
    internal static IDictionary<Enum, decimal> Rates { get; } = new Dictionary<Enum, decimal>(DefaultRates);

    internal static ICollection<Enum> DefaultMeasureUnits => DefaultRates.Keys;

    private static IDictionary<Enum, decimal> DefaultRates
    {
        get
        {
            IDictionary<Enum, decimal> defaultRates = ConstantRates;

            // Add not-constant default measure units with default exchange rate.
            defaultRates.Add(Currency.Default, 1m);
            defaultRates.Add(Pieces.Default, 1m);

            return defaultRates;
        }
    }

    internal static ICollection<Enum> ConstantMeasureUnits => ConstantRates.Keys;

    private static IDictionary<Enum, decimal> ConstantRates => new Dictionary<Enum, decimal>()
    {
        // AreaMeasures
        { AreaUnit.mmSquare, 1m },
        { AreaUnit.cmSquare, 100m },
        { AreaUnit.dmSquare, 10000m },
        { AreaUnit.meterSquare, 1000000m },
        { AreaUnit.kmSquare, 1000000000000m },

        // DistanceMeasures
        { DistanceUnit.meter, 1m },
        { DistanceUnit.km, 1000m },

        // ExtentMeasures
        { ExtentUnit.mm, 1m },
        { ExtentUnit.cm, 10m },
        { ExtentUnit.dm, 100m },
        { ExtentUnit.meter, 1000m },

        // TimeMeasures
        { TimeUnit.minute, 1m },
        { TimeUnit.hour, 60m },
        { TimeUnit.day, 1440m },
        { TimeUnit.week, 10080m },
        { TimeUnit.decade, 14400m },

        // VolumeMeasures
        { VolumeUnit.mmCubic, 1m },
        { VolumeUnit.cmCubic, 1000m },
        { VolumeUnit.dmCubic, 1000000m },
        { VolumeUnit.meterCubic, 1000000000m },

        // WeightMeasures
        { WeightUnit.g, 1m },
        { WeightUnit.kg, 1000m },
        { WeightUnit.ton, 1000000m },
    };
    #endregion

    public static bool TryAddExchangeRate(this Enum measureUnit, decimal exchangeRate)
    {
        if (exchangeRate <= 0) return false;

        return TryAddToExchangeRates(measureUnit, exchangeRate);
    }

    public static bool TryAddReplaceExchangeRate(this Enum measureUnit, decimal exchangeRate)
    {
        if (exchangeRate <= 0) return false;

        if (!measureUnit.ShouldHaveAdHocExchangeRate()) return false;

        if (TryAddToExchangeRates(measureUnit, exchangeRate)) return true;

        if (!Rates.Remove(measureUnit)) return false;

        return TryAddToExchangeRates(measureUnit, exchangeRate);
    }

    public static bool ShouldHaveAdHocExchangeRate(this Enum measureUnit)
    {
        if (!measureUnit.IsDefinedMeasureUnit()) return false;

        return !DefaultMeasureUnits.Contains(measureUnit);
    }

    public static IDictionary<Enum, decimal> GetExchangeRates() => new Dictionary<Enum, decimal>(Rates);

    public static IDictionary<Enum, decimal> GetExchangeRates(this Type measureUnitType)
    {
        return Rates.Where(x => x.Key.GetType() == measureUnitType).ToDictionary(x => x.Key, x => x.Value);
    }

    public static bool TryGetExchangeRates(this Type measureUnitType, [NotNullWhen(true)] out IDictionary<Enum, decimal> exchangeRates)
    {
        exchangeRates = measureUnitType.GetExchangeRates();

        return exchangeRates.Count > 0;
    }

    public static bool TryGetExchangeRate(this Enum measureUnit, [MaybeNullWhen(false)] out decimal exchangeRate)
    {
        return Rates.TryGetValue(measureUnit, out exchangeRate);
    }

    public static bool TryGetMeasureUnit(this Type measureUnitType, decimal exchangeRate, [NotNullWhen(true)] out Enum? measureUnit)
    {
        measureUnit = measureUnitType.GetExchangeRates().FirstOrDefault(x => x.Value == exchangeRate).Key;

        return measureUnit != default;
    }

    private static bool TryAddToExchangeRates(Enum measureUnit, decimal exchangeRate)
    {
        return measureUnit?.IsDefinedMeasureUnit() == true && Rates.TryAdd(measureUnit, exchangeRate);
    }

    public static decimal GetExchangeRate(this Enum measureUnit)
    {
        if (!measureUnit.TryGetExchangeRate(out decimal exchangeRate)) throw new ArgumentOutOfRangeException(nameof(measureUnit));

        return exchangeRate;
    }
}
