namespace CsabaDu.Foo_Var.Measures.Interfaces.DataTypes.MeasureTypes;

public interface IVolume : IMeasure
{
    IVolume GetVolume(double quantity, VolumeUnit volumeUnit);

    IVolume GetVolume(IBaseMeasure? other = null);
}
