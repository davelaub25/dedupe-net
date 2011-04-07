using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DedupeNET.Core
{
    public abstract class SimilarityFunction<T> : MeasurementFunction<T>
    {
        public SimilarityFunction(T firstEntity, T secondEntity)
            : base(firstEntity, secondEntity)
        {

        }

        public abstract double Similarity();
    }
}
