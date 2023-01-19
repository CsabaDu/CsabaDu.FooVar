namespace CsabaDu.Foo_Var.Tests.Statics
{
    internal static class SampleParams
    {
        #region Quantities
        internal static ValueType ZeroQuantity => default(int);
        internal static ValueType PositiveQuantity => uint.MaxValue;
        internal static ValueType NegativeQuantity => DecimalMinusOne;
        internal static ValueType FractionQuantity => double.Epsilon;
        #endregion

        #region MeasureUnits
        internal static Type SampleMeasureUnitType => typeof(WeightUnit);
        internal static int WeightMeasureNamesLength => Enum.GetNames(SampleMeasureUnitType).Length;
        internal static Enum MediumValueSampleMeasureUnit => WeightUnit.kg;
        internal static Enum MaxValueSampleMeasureUnit => (WeightUnit)(WeightMeasureNamesLength - 1);
        internal static Enum DefaultSampleMeasureUnit => default(WeightUnit);
        internal static Enum DifferentTypeSampleMeasureUnit => VolumeUnit.meterCubic;
        internal static Enum NotMeasureUnitTypeEnum => default(LimitType);
        internal static Enum NotDefinedSampleMeasureUnit => (WeightUnit)WeightMeasureNamesLength;
        internal static Enum MeasureUnitShouldHaveAdHocRate => (Currency)1;
        internal static Enum DefaultPieces => default(Pieces);
        #endregion

        #region Decimals
        internal static decimal DecimalZero => decimal.Zero;
        internal static decimal DecimalPositive => decimal.MaxValue;
        internal static decimal DecimalNegative => decimal.MinValue;
        internal static decimal DecimalOne => decimal.One;
        internal static decimal DecimalMinusOne => decimal.MinusOne;
        #endregion

        #region LimitTypes
        internal static LimitType DefaultLimitType = default(LimitType);
        internal static LimitType NonDefaultLimitType => (LimitType)Enum.GetNames(typeof(LimitType)).Length - 1;
        #endregion
    }
}
