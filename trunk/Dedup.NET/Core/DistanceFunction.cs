using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DedupeNET.Core
{
    public abstract class DistanceFunction : MeasurementFunction
    {
        public DistanceFunction(string firstString, string secondString)
            : base(firstString, secondString)
        {
        }

        public abstract double Distance();
    }
}
