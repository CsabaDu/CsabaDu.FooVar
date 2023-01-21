using CsabaDu.Foo_Var.Geometrics.DataTypes.Spread.SpreadTypes;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeAspects;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;
using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Spread;
using CsabaDu.Foo_Var.Geometrics.Interfaces.Factories;

namespace CsabaDu.Foo_Var.Geometrics.Factories
{
    public class SpreadFactory : ISpreadFactory
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

        public IBody GetBody(IGeometricBody geometricBody)
        {
            return new BulkBody(geometricBody);
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

        public ISurface GetSurface(IGeometricBody geometricBody)
        {
            return CreateSurface(geometricBody);
        }

        private static ISurface CreateSurface(IGeometricBody geometricBody)
        {
            IArea baseArea = geometricBody.GetBaseShape().Area;
            IExtent height = geometricBody.Height;
            IMeasure basePerimeter = height;

            if (geometricBody is ICuboid cuboid)
            {
                basePerimeter = cuboid.Length.SumWith(cuboid.Width).MultipliedBy(2);
            }

            if (geometricBody is ICylinder cylinder)
            {
                basePerimeter = cylinder.BaseShape.GetDiagonal().MultipliedBy(Convert.ToDecimal(Math.PI));
            }

            IExtent mantleBaseExtent = height.GetExtent(basePerimeter);
            IArea mantleArea = GetRectangleArea(mantleBaseExtent, height);

            IArea fullSurfaceArea = baseArea.GetArea(baseArea.MultipliedBy(2).SumWith(mantleArea));

            return new BulkSurface(fullSurfaceArea);
        }
    }
}
