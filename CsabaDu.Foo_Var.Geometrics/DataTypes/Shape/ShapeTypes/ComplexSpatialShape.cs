using CsabaDu.Foo_Var.Geometrics.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.Foo_Var.Geometrics.DataTypes.Shape.ShapeTypes
{
    internal class ComplexSpatialShape : GeometricBody, IComplexSpatialShape
    {
        public ComplexSpatialShape(IExtent height) : base(height, ShapeTrait.None)
        {
        }

        public ComplexSpatialShape(IEnumerable<IExtent> shapeExtentList) : base(shapeExtentList, ShapeTrait.None)
        {
        }

        public override IVolume Volume { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
        public IEnumerable<ICuboid> InnerTangentCuboids { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }

        public IComplexSpatialShape GetComplexSpatialShape(params IExtent[] shapeExtents)
        {
            throw new NotImplementedException();
        }

        public IComplexSpatialShape GetComplexSpatialShape(ExtentUnit extentUnit)
        {
            throw new NotImplementedException();
        }

        public IComplexSpatialShape GetComplexSpatialShape(IEnumerable<ICuboid> innerTangentCuboids, ICuboid? dimensions = null)
        {
            throw new NotImplementedException();
        }

        public IComplexSpatialShape GetComplexSpatialShape(IEnumerable<IExtent> innerShapeExtentList, IEnumerable<IExtent>? shapeExtentList = null)
        {
            throw new NotImplementedException();
        }

        public override IExtent GetDiagonal(ExtentUnit extentUnit = ExtentUnit.meter)
        {
            throw new NotImplementedException();
        }

        public IRectangularShape GetDimensions()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IExtent> GetInnerShapeExtentList()
        {
            throw new NotImplementedException();
        }

        public ICuboid GetInnerTangentCuboid(Comparison comparison)
        {
            throw new NotImplementedException();
        }

        public override IPlaneShape GetProjection(ShapeExtentType shapeExtentType)
        {
            throw new NotImplementedException();
        }

        public override IReadOnlyList<IExtent> GetShapeExtentList()
        {
            throw new NotImplementedException();
        }

        public override IShape GetTangentShape(Side shapeSide = Side.Outer)
        {
            throw new NotImplementedException();
        }
    }
}
