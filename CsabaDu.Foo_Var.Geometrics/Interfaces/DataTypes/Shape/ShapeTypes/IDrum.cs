using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;

namespace CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

public interface IDrum : ISpatialShape<ICircle>, IRoundShape
{
    IDrum GetDrum(params IExtent[] shapeExtents);
    IDrum GetDrum(IPlaneShape baseShape, IExtent height);
    IDrum GetDrum(ExtentUnit extentUnit);
    IDrum GetDrum(IGeometricBody geometricBody);
}