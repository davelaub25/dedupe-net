using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DedupeNET.Core
{
    public abstract class MeasurementFunction<T>
    {
        public T InputEntity
        {
            get;
            set;
        }

        public T ReferenceEntity
        {
            get;
            set;
        }

        public MeasurementFunction(T firstEntity, T secondEntity)
        {
            InputEntity = firstEntity;
            ReferenceEntity = secondEntity;
        }
    }
}
