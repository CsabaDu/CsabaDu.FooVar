namespace CsabaDu.FooVar.Measures.Interfaces.DataTypes.MeasureTypes;

public interface IVolume : IMeasure
{
    IVolume GetVolume(double quantity, VolumeUnit volumeUnit);

    IVolume GetVolume(IBaseMeasure? other = null);
}
