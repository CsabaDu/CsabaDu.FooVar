using CsabaDu.Foo_Var.Geometrics.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.Foo_Var.Geometrics.DataTypes.Shape.ShapeTypes;

internal class ComplexSpatialShape : GeometricBody, IComplexSpatialShape
{
    public ComplexSpatialShape(IEnumerable<ICuboid> innerTangentCuboids, ICuboid? dimensions) : base(ShapeTrait.None)
    {
        ValidateCuboids(innerTangentCuboids, dimensions);

        Dimensions = dimensions ?? GetDimensions(innerTangentCuboids);
        InnerTangentCuboids = innerTangentCuboids;
        Volume = Dimensions.Volume;
    }

    public ComplexSpatialShape(IEnumerable<IExtent> innerShapeExtentList, IEnumerable<IExtent>? outerShapeExtentList) : base(outerShapeExtentList ?? GetEnclosingShapeExtentList(innerShapeExtentList), ShapeTrait.None)
    {
        ValidateShapeExtentLists(innerShapeExtentList);
        outerShapeExtentList ??= GetEnclosingShapeExtentList(innerShapeExtentList);

        Dimensions = GetDimensions(outerShapeExtentList);
        InnerTangentCuboids = GetInnerTangentCuboids(innerShapeExtentList);
        Volume = Dimensions.Volume;
    }

    public override IVolume Volume { get; init; }
    public IEnumerable<ICuboid> InnerTangentCuboids { get; init; }
    public ICuboid Dimensions { get; init; }

    private static IEnumerable<ICuboid> GetInnerTangentCuboids(IEnumerable<IExtent> innerShapeExtentList)
    {
        throw new NotImplementedException();
    }

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
        return Dimensions.GetDiagonal();
    }

    public IRectangularShape GetDimensions() => Dimensions;

    private ICuboid GetDimensions(IEnumerable<ICuboid> innerTangentCuboids)
    {
        IEnumerable<IExtent> innerShapeExtentList = innerTangentCuboids.GetInnerShapeExtentList();

        return GetDimensions(innerShapeExtentList);
    }

    private ICuboid GetDimensions(IEnumerable<IExtent> innerShapeExtentList)
    {
        IEnumerable<IExtent> enclosigShapeExtentList = GetEnclosingShapeExtentList(innerShapeExtentList);

        return (ICuboid)ShapeFactory.GetRectangularShape(enclosigShapeExtentList.ToArray());
    }

    public override IExtent GetHeight()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IExtent> GetInnerShapeExtentList()
    {
        return InnerTangentCuboids.GetInnerShapeExtentList();
    }

    public int GetInnerTangentCuboidsCount() => InnerTangentCuboids.Count();

    public override IPlaneShape GetProjection(ShapeExtentType shapeExtentType)
    {
        return Dimensions.GetProjection(shapeExtentType);
    }

    public override IReadOnlyList<IExtent> GetShapeExtentList()
    {
        return Dimensions.GetShapeExtentList();
    }

    public override IShape GetTangentShape(Side shapeSide = Side.Outer)
    {
        return Dimensions.GetTangentShape(shapeSide);
    }

    public void ValidateShapeExtentLists(IEnumerable<IExtent> innerShapeExtentList, IEnumerable<IExtent>? outerShapeExtentList = null)
    {
        IEnumerable<IExtent> enclosingShapeExtentList = GetEnclosingShapeExtentList(innerShapeExtentList);

        outerShapeExtentList ??= enclosingShapeExtentList;

        ValidateShapeExtentList(outerShapeExtentList, ShapeTraits);

        for (int i = 0; i < ShapeExtentTypeCount; i++)
        {
            IExtent outerShapeExtent = outerShapeExtentList.ElementAt(i);
            IExtent enclosingShapeExtent = enclosingShapeExtentList.ElementAt(i);

            bool? fitsIn = enclosingShapeExtent.FitsIn(outerShapeExtent, LimitType.BeNotGreater);

            if (fitsIn != true) throw new ArgumentOutOfRangeException(nameof(outerShapeExtentList));
        }
    }

    public void ValidateCuboids(IEnumerable<ICuboid> innerTangentCuboids, ICuboid? dimensions = null)
    {
        int count = innerTangentCuboids?.Count() ?? throw new ArgumentNullException(nameof(innerTangentCuboids));

        if (count == 0) throw new ArgumentOutOfRangeException(nameof(innerTangentCuboids), count, null);

        IEnumerable<IExtent> innerShapeExtentist = innerTangentCuboids.GetInnerShapeExtentList();

        IEnumerable<IExtent>? outerShapeExtentList = dimensions?.GetShapeExtentList();

        ValidateShapeExtentLists(innerShapeExtentist, outerShapeExtentList);
    }
}
