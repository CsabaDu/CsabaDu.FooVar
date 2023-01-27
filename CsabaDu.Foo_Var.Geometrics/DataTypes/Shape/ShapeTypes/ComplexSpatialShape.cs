using CsabaDu.Foo_Var.Geometrics.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.Foo_Var.Geometrics.DataTypes.Shape.ShapeTypes;

internal class ComplexSpatialShape : GeometricBody, IComplexSpatialShape
{
    public ComplexSpatialShape(IEnumerable<ICuboid> innerTangentCuboidList, ICuboid? dimensions) : base(ShapeTrait.None)
    {
        ValidateCuboids(innerTangentCuboidList, dimensions);

        Dimensions = dimensions ?? GetDimensions(innerTangentCuboidList);
        InnerTangentCuboidList = innerTangentCuboidList;
        Volume = Dimensions.Volume;
    }

    public ComplexSpatialShape(IEnumerable<IExtent> innerShapeExtentList, IEnumerable<IExtent>? outerShapeExtentList) : base(outerShapeExtentList ??= GetValidatedEnclosingShapeExtentList(innerShapeExtentList), ShapeTrait.None)
    {
        Dimensions = GetDimensions(outerShapeExtentList);
        InnerTangentCuboidList = GetInnerTangentCuboidList(innerShapeExtentList);
        Volume = Dimensions.Volume;
    }

    public override IVolume Volume { get; init; }
    public IEnumerable<ICuboid> InnerTangentCuboidList { get; init; }
    public ICuboid Dimensions { get; init; }

    private IEnumerable<ICuboid> GetInnerTangentCuboidList(IEnumerable<IExtent> innerShapeExtentList)
    {
        ValidateInnerShapeExtentList(innerShapeExtentList);

        int count = innerShapeExtentList.Count() / ShapeExtentTypeCount;
        List<ICuboid> innerTangentCuboidList = new();

        for (int i = 0; i < count; i++)
        {
            int lengthIndex = i * ShapeExtentTypeCount;
            IExtent length = innerShapeExtentList.ElementAt(lengthIndex);

            int widthIndex = lengthIndex + 1;
            IExtent width = innerShapeExtentList.ElementAt(widthIndex);

            int heightIndex = widthIndex + 1;
            IExtent height = innerShapeExtentList.ElementAt(heightIndex);

            ICuboid cuboid = ShapeFactory.GetCuboid(length, width, height);
            innerTangentCuboidList.Add(cuboid);
        }

        return innerTangentCuboidList;
    }

    public IComplexSpatialShape GetComplexSpatialShape() => this;

    public IComplexSpatialShape GetComplexSpatialShape(ExtentUnit extentUnit)
    {
        ICuboid dimensions = (ICuboid)Dimensions.ExchangeTo(extentUnit)!;
        List<ICuboid> innerTangentCuboidList = new();

        foreach (ICuboid item in InnerTangentCuboidList)
        {
            ICuboid innerTangentCuboid = (ICuboid)item.ExchangeTo(extentUnit)!;
            innerTangentCuboidList.Add(innerTangentCuboid);
        }

        return GetComplexSpatialShape(innerTangentCuboidList, dimensions);
    }

    public IComplexSpatialShape GetComplexSpatialShape(IEnumerable<ICuboid> innerTangentCuboidList, ICuboid? dimensions = null)
    {
        return new ComplexSpatialShape(innerTangentCuboidList, dimensions);
    }

    public IComplexSpatialShape GetComplexSpatialShape(IEnumerable<IExtent> innerShapeExtentList, IEnumerable<IExtent>? outerShapeExtentList = null)
    {
        return new ComplexSpatialShape(innerShapeExtentList, outerShapeExtentList);
    }

    public override IExtent GetDiagonal(ExtentUnit extentUnit = ExtentUnit.meter)
    {
        return Dimensions.GetDiagonal();
    }

    public IRectangularShape GetDimensions() => Dimensions;

    private ICuboid GetDimensions(IEnumerable<ICuboid> innerTangentCuboidList)
    {
        IEnumerable<IExtent> innerShapeExtentList = innerTangentCuboidList.GetInnerShapeExtentList();

        return GetDimensions(innerShapeExtentList);
    }

    private ICuboid GetDimensions(IEnumerable<IExtent> innerShapeExtentList)
    {
        IEnumerable<IExtent> enclosigShapeExtentList = GetValidatedEnclosingShapeExtentList(innerShapeExtentList);

        return (ICuboid)ShapeFactory.GetRectangularShape(enclosigShapeExtentList.ToArray());
    }

    public override IExtent GetHeight() => Dimensions.Height;

    public IEnumerable<IExtent> GetInnerShapeExtentList()
    {
        return InnerTangentCuboidList.GetInnerShapeExtentList();
    }

    public int GetInnerTangentCuboidListCount() => InnerTangentCuboidList.Count();

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
        IEnumerable<IExtent> enclosingShapeExtentList = GetValidatedEnclosingShapeExtentList(innerShapeExtentList);

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

    public void ValidateCuboids(IEnumerable<ICuboid> innerTangentCuboidList, ICuboid? dimensions = null)
    {
        int count = innerTangentCuboidList?.Count() ?? throw new ArgumentNullException(nameof(innerTangentCuboidList));

        if (count == 0) throw new ArgumentOutOfRangeException(nameof(innerTangentCuboidList), count, null);

        IEnumerable<IExtent> innerShapeExtentist = innerTangentCuboidList.GetInnerShapeExtentList();

        IEnumerable<IExtent>? outerShapeExtentList = dimensions?.GetShapeExtentList();

        ValidateShapeExtentLists(innerShapeExtentist, outerShapeExtentList);
    }

    public void ValidateInnerShapeExtentList(IEnumerable<IExtent> innerShapeExtentList)
    {
        ValidateGeometrics.ValidateInnerShapeExtentList(innerShapeExtentList);
    }
}
