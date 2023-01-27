using CsabaDu.Foo_Var.Geometrics.DataTypes.Shape.ShapeTypes;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;
using CsabaDu.Foo_Var.Geometrics.Interfaces.Factories.Shape;

namespace CsabaDu.Foo_Var.Geometrics.Factories;

public class ShapeFactory : IShapeFactory
{
    public ICuboid GetCuboid(IExtent length, IExtent width, IExtent height)
    {
        return new Cuboid(length, width, height);
    }

    public ICuboid GetCuboid(IRectangle baseShape, IExtent height)
    {
        return new Cuboid(baseShape, height);
    }

    public ICylinder GetCylinder(IExtent radius, IExtent height)
    {
        return new Cylinder(radius, height);
    }

    public ICylinder GetCylinder(ICircle baseShape, IExtent height)
    {
        return new Cylinder(baseShape, height);
    }

    public ICircle GetCircle(IExtent radius)
    {
        return new Circle(radius);
    }

    public IRectangle GetRectangle(IExtent length, IExtent width)
    {
        return new Rectangle(length, width);
    }

    public IGeometricBody GetGeometricBody(params IExtent[] shapeExtents)
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

    public IGeometricBody GetGeometricBody(IGeometricBody geometricBody)
    {
        _ = geometricBody ?? throw new ArgumentNullException(nameof(geometricBody));

        IExtent[] shapeExtents = geometricBody.GetShapeExtentList().ToArray();

        return GetGeometricBody(shapeExtents);
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

    public IPlaneShape GetPlaneShape(IPlaneShape planeShape)
    {
        _ = planeShape ?? throw new ArgumentNullException(nameof(planeShape));

        IExtent[] shapeExtents = planeShape.GetShapeExtentList().ToArray();

        return GetPlaneShape(shapeExtents);
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
            if (shapeTraits.HasFlag(ShapeTrait.Plane | ShapeTrait.Circular))
            {
                return new Circle(shapeExtentList);
            }

            if (shapeTraits.HasFlag(ShapeTrait.Plane))
            {
                return new Rectangle(shapeExtentList);
            }

            if (shapeTraits.HasFlag(ShapeTrait.Circular))
            {
                return new Cylinder(shapeExtentList);
            }
        }

        return new Cuboid(shapeExtentList);
    }
}
