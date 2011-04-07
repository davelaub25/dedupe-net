using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DedupeNET.Core
{
    public abstract class DistanceFunction<T> : MeasurementFunction<T>
    {
        public DistanceFunction(T firstEntity, T secondEntity)
            : base(firstEntity, secondEntity)
        {
        }

        public abstract double Distance();
    }
}
