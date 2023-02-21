using CsabaDu.FooVar.Geometrics.DataTypes.Spread.SpreadTypes;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;
using CsabaDu.FooVar.Geometrics.Interfaces.Factories;

namespace CsabaDu.FooVar.Geometrics.Factories
{
    public sealed class SpreadFactory : ISpreadFactory
    {
        public IBody GetBody(IVolume volume)
        {
            return new BulkBody(volume);
        }

        public IBody GetBody(ISpread<IVolume, VolumeUnit> spread)
        {
            return new BulkBody(spread);
        }

        public IBody GetBody(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits)
        {
            return new BulkBody(shapeExtentList, shapeTraits);
        }

        public ISurface GetSurface(IArea area)
        {
            return new BulkSurface(area);
        }

        public ISurface GetSurface(ISpread<IArea, AreaUnit> spread)
        {
            return new BulkSurface(spread);
        }

        public ISurface GetSurface(IEnumerable<IExtent> shapeExtentList, ShapeTrait shapeTraits)
        {
            return new BulkSurface(shapeExtentList, shapeTraits);
        }

        public ISurface GetSurface(IPlaneShape planeShape)
        {
            return new BulkSurface(planeShape);
        }

        public ISurface GetSurface(IDryBody dryBody)
        {
            return CreateSurface(dryBody);
        }

        private static ISurface CreateSurface(IDryBody dryBody)
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
    }
}
