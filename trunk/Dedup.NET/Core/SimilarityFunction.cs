using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DedupeNET.Core
{
    public abstract class SimilarityFunction<T> : MeasurementFunction<T>
    {
        public SimilarityFunction(T inputEntity, T referenceEntity)
            : base(inputEntity, referenceEntity)
        {

        }

        public abstract double Similarity();
    }
}
