using CsabaDu.FooVar.Geometrics.DataTypes.Spread.SpreadTypes;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread;
using CsabaDu.FooVar.Geometrics.Interfaces.DataTypes.Spread.SpreadTypes;
using CsabaDu.FooVar.Geometrics.Interfaces.Factories;

namespace CsabaDu.FooVar.Geometrics.Factories
{
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

        public ISpread? GetSpread(params object[] args) // TODO
        {
            throw new NotImplementedException();
        }
    }
}
