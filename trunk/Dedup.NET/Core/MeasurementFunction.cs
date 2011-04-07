using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DedupeNET.Core
{
    public abstract class MeasurementFunction<T>
    {
        public T FirstEntity
        {
            get;
            set;
        }

        public T SecondEntity
        {
            get;
            set;
        }

        public MeasurementFunction(T firstEntity, T secondEntity)
        {
            FirstEntity = firstEntity;
            SecondEntity = secondEntity;
        }
    }
}
