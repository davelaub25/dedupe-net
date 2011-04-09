using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DedupeNET.Core
{
    public abstract class DistanceFunction<T> : MeasurementFunction<T>
    {
        protected DistanceFunction(T inputEntity, T referenceEntity)
            : base(inputEntity, referenceEntity)
        {
        }

        public abstract double Distance();
    }
}
