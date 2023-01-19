using CsabaDu.Foo_Var.Measures.Statics;

namespace CsabaDu.Foo_Var.Measures.Interfaces.DataTypes.MeasureTypes;

public interface IPieceCount : IMeasure
{
    IPieceCount GetCount(int quantity, Pieces pieces);


    IPieceCount GetCount(IBaseMeasure? other = null);
}
