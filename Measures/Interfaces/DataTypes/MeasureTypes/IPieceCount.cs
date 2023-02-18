using CsabaDu.FooVar.Measures.Statics;

namespace CsabaDu.FooVar.Measures.Interfaces.DataTypes.MeasureTypes;

public interface IPieceCount : IMeasure
{
    IPieceCount GetCount(int quantity, Pieces pieces);


    IPieceCount GetCount(IBaseMeasure? other = null);
}
