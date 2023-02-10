﻿using CsabaDu.Foo_Var.Geometrics.Interfaces.DataTypes.Shape.ShapeTypes;

namespace CsabaDu.Foo_Var.Cargoes.Interfaces
{
    public interface IBulkContainer : ICargoContainer
    {
        IDry<ICuboid> BulkContainerCapacity { get; init; }
        ICuboid? GeometricBody { get; init; }

        IBulkContainer GetBulkContainer();
        IBulkContainer GetBulkContainer(IDry<ICuboid> bulkContainerCapacity);
    }
}
