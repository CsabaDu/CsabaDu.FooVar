using CsabaDu.FooVar.Geometrics.DataTypes.Shape.ShapeTypes;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;
using CsabaDu.FooVar.Geometrics.Interfaces.Factories.Shape;

namespace CsabaDu.FooVar.Geometrics.Factories;

public sealed class ShapeFactory : IShapeFactory
{
    public ICuboid GetCuboid(IExtent length, IExtent width, IExtent height)
    {
        return new Cuboid(length, width, height);
    }

    public ICuboid GetCuboid(IRectangle baseFace, IExtent height)
    {
        return new Cuboid(baseFace, height);
    }

    public ICylinder GetCylinder(IExtent radius, IExtent height)
    {
        return new Cylinder(radius, height);
    }

    public ICylinder GetCylinder(ICircle baseFace, IExtent height)
    {
        return new Cylinder(baseFace, height);
    }

    public ICircle GetCircle(IExtent radius)
    {
        return new Circle(radius);
    }

    public IRectangle GetRectangle(IExtent length, IExtent width)
    {
        return new Rectangle(length, width);
    }

    public IDryBody GetDryBody(params IExtent[] shapeExtents)
    {
        _ = shapeExtents ?? throw new ArgumentNullException(nameof(shapeExtents));

        int count = shapeExtents.Length;

        return count switch
        {
            2 => GetCylinder(shapeExtents[0], shapeExtents[1]),
            3 => GetCuboid(shapeExtents[0], shapeExtents[1], shapeExtents[2]),

            _ => throw new ArgumentOutOfRangeException(nameof(shapeExtents), count, null),
        };
    }

    public IDryBody GetDryBody(IDryBody dryBody)
    {
        _ = dryBody ?? throw new ArgumentNullException(nameof(dryBody));

        IExtent[] shapeExtents = dryBody.GetShapeExtentList().ToArray();

        return GetDryBody(shapeExtents);
    }

    public IDryBody GetDryBody(IPlaneShape baseFace, IExtent height)
    {
        _ = baseFace ?? throw new ArgumentNullException(nameof(baseFace));

        if (baseFace is ISection section)
        {
            baseFace = section.PlaneSectionShape;
        }

        return baseFace.ShapeTraits.HasFlag(ShapeTrait.Circular) ?
            GetCylinder((ICircle)baseFace, height)
            : GetCuboid((IRectangle)baseFace, height);
    }

    public IPlaneShape GetPlaneShape(params IExtent[] shapeExtents)
    {
        _ = shapeExtents ?? throw new ArgumentNullException(nameof(shapeExtents));

        int count = shapeExtents.Length;

        return count switch
        {
            1 => GetCircle(shapeExtents[0]),
            2 => GetRectangle(shapeExtents[0], shapeExtents[1]),

            _ => throw new ArgumentOutOfRangeException(nameof(shapeExtents), count, null),
        };
    }

    public IPlaneShape GetPlaneShape(IPlaneShape planeShape, IRectangle? cornerPadding = null, ShapeExtentType? perpendicular = null)
    {
        _ = planeShape ?? throw new ArgumentNullException(nameof(planeShape));

        if (cornerPadding == null)
        {
            IExtent[] shapeExtents = planeShape.GetShapeExtentList().ToArray();

            return GetPlaneShape(shapeExtents);
        }

        if (planeShape is ISection section)
        {
            planeShape = section.PlaneSectionShape;
        }

        return GetSection(planeShape, cornerPadding, perpendicular);
    }

    public IRectangularShape GetRectangularShape(params IExtent[] shapeExtents)
    {
        _ = shapeExtents ?? throw new ArgumentNullException(nameof(shapeExtents));

        int count = shapeExtents.Length;

        return count switch
        {
            2 => GetRectangle(shapeExtents[0], shapeExtents[1]),
            3 => GetCuboid(shapeExtents[0], shapeExtents[1], shapeExtents[2]),

            _ => throw new ArgumentOutOfRangeException(nameof(shapeExtents), count, null),
        };
    }

    public ICircularShape GetCircularShape(params IExtent[] shapeExtents)
    {
        _ = shapeExtents ?? throw new ArgumentNullException(nameof(shapeExtents));

        int count = shapeExtents.Length;

        return count switch
        {
            1 => GetCircle(shapeExtents[0]),
            2 => GetCylinder(shapeExtents[0], shapeExtents[1]),

            _ => throw new ArgumentOutOfRangeException(nameof(shapeExtents), count, null),
        };
    }

    public IShape GetShape(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits)
    {
        return CreateShape(shapeExtentList, shapeTraits);
    }

    private static IShape CreateShape(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits)
    {
        shapeTraits.ValidateShapeTraits();
        shapeTraits.ValidateShapeExtentList(shapeExtentList);

        if (!shapeTraits.Equals(ShapeTrait.None))
        {
            if (shapeTraits.HasFlag(ShapeTrait.Plane))
            {
                return new Rectangle(shapeExtentList);
            }

            if (shapeTraits.HasFlag(ShapeTrait.Circular))
            {
                return new Cylinder(shapeExtentList);
            }

            if (shapeTraits.HasFlag(ShapeTrait.Plane | ShapeTrait.Circular))
            {
                return new Circle(shapeExtentList);
            }
        }

        if (shapeExtentList.Count() == shapeTraits.GetShapeExtentCount()) return new Cuboid(shapeExtentList);

        return new ComplexDryBody(shapeExtentList, null);
    }

    public IComplexDryBody GetComplexDryBody(IEnumerable<ICuboid> innerTangentCuboidList, ICuboid? dimensions = null)
    {
        return new ComplexDryBody(innerTangentCuboidList, dimensions);
    }

    public IComplexDryBody GetComplexDryBody(IEnumerable<IExtent> innerShapeExtentList, IEnumerable<IExtent>? outerShapeExtentList = null)
    {
        return new ComplexDryBody(innerShapeExtentList, outerShapeExtentList);
    }

    public ISection GetSection(IPlaneShape planeSectionShape, IRectangle cornerPadding, ShapeExtentType? perpendicular = null)
    {
        return perpendicular is not ShapeExtentType notNullPerpendicular ?
            GetPlaneSection(planeSectionShape, cornerPadding)
            : GetCrossSection(planeSectionShape, cornerPadding, notNullPerpendicular);
    }

    public ISection GetSection(ISection section, ShapeExtentType? perpendicular = null)
    {
        return perpendicular is not ShapeExtentType notNullPerpendicular ?
            GetPlaneSection(section)
            : GetCrossSection(section, notNullPerpendicular);
    }

    public IPlaneSection GetPlaneSection(IPlaneShape planeSectionShape, IRectangle cornerPadding)
    {
        return new PlaneSection(planeSectionShape, cornerPadding);
    }

    public IPlaneSection GetPlaneSection(ISection section)
    {
        return new PlaneSection(section);
    }

    public ICrossSection GetCrossSection(IPlaneShape planeSectionShape, IRectangle cornerPadding, ShapeExtentType perpendicular)
    {
        return new CrossSection(planeSectionShape, cornerPadding, perpendicular);
    }

    public ICrossSection GetCrossSection(ISection section, ShapeExtentType perpendicular)
    {
        return new CrossSection(section, perpendicular);
    }
}
