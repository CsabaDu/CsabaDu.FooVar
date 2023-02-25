using CsabaDu.FooVar.Geometrics.DataTypes.Spread.SpreadTypes;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread.SpreadTypes;
using CsabaDu.FooVar.Geometrics.Interfaces.Factories;

namespace CsabaDu.FooVar.Geometrics.Factories;

public sealed class SpreadFactory : ISpreadFactory
{
    public IBulkBody GetBulkBody(IVolume volume)
    {
        return new BulkBody(volume);
    }

    public IBulkBody GetBulkBody(ISpread<IVolume, VolumeUnit> spread)
    {
        return new BulkBody(spread);
    }

    public IBulkBody GetBulkBody(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits)
    {
        return new BulkBody(shapeExtentList, shapeTraits);
    }

    public IBulkBody GetBulkBody(IDryBody dryBody)
    {
        return new BulkBody(dryBody);
    }

    public IBulkSurface GetBulkSurface(IArea area)
    {
        return new BulkSurface(area);
    }

    public IBulkSurface GetBulkSurface(ISpread<IArea, AreaUnit> spread)
    {
        return new BulkSurface(spread);
    }

    public IBulkSurface GetBulkSurface(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits)
    {
        return new BulkSurface(shapeExtentList, shapeTraits);
    }

    public IBulkSurface GetBulkSurface(IPlaneShape planeShape)
    {
        return new BulkSurface(planeShape);
    }

    public IBulkSurface GetBulkSurface(IDryBody dryBody)
    {
        return CreateBulkSurface(dryBody);
    }

    private static IBulkSurface CreateBulkSurface(IDryBody dryBody)
    {
        IArea baseArea = dryBody.GetBaseFace().Area;
        IExtent height = dryBody.GetHeight();
        IMeasure basePerimeter = height;

        if (dryBody is ICuboid cuboid)
        {
            basePerimeter = cuboid.Length.SumWith(cuboid.Width).MultipliedBy(2);
        }

        if (dryBody is ICylinder cylinder)
        {
            basePerimeter = cylinder.BaseFace.GetDiagonal().MultipliedBy(Convert.ToDecimal(Math.PI));
        }

        IExtent mantleBaseExtent = height.GetExtent(basePerimeter);
        IArea mantleArea = GetRectangleArea(mantleBaseExtent, height);

        IArea fullSurfaceArea = baseArea.GetArea(baseArea.MultipliedBy(2).SumWith(mantleArea));

        return new BulkSurface(fullSurfaceArea);
    }

    public ISpread GetSpread(ISpread spread)
    {
        if (spread is ISpread<IArea, AreaUnit> surface) return GetBulkSurface(surface);

        if (spread is ISpread<IVolume, VolumeUnit> body) return GetBulkBody(body);

        throw new NotImplementedException();
    }

    public ISpread GetSpread(IMeasure spreadMeasure)
    {
        if (spreadMeasure is IArea area) return GetBulkSurface(area);

        if (spreadMeasure is IVolume volume) return GetBulkBody(volume);

        throw new ArgumentOutOfRangeException(nameof(spreadMeasure), spreadMeasure, null);
    }

    public ISpread GetSpread(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits)
    {
        return shapeTraits.HasFlag(ShapeTrait.Plane) ?
            GetBulkSurface(shapeExtentList, shapeTraits)
            : GetBulkBody(shapeExtentList, shapeTraits);
    }

    public ISpread GetSpread(IShape shape)
    {
        return shape.ShapeTraits.HasFlag(ShapeTrait.Plane) ?
            GetBulkSurface((IPlaneShape)shape)
            : GetBulkBody((IDryBody)shape);
    }
}
