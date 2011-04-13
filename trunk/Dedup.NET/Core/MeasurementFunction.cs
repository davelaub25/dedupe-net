using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DedupeNET.Core
{
    public abstract class MeasurementFunction<T>
    {
        protected T _inputEntity;
        public T InputEntity
        {
            get { return _inputEntity; }
            set { _inputEntity = value; }
        }

        protected T _referenceEntity;
        public T ReferenceEntity
        {
            get { return _referenceEntity; }
            set { _referenceEntity = value; }
        }

        public MeasurementFunction(T inputEntity, T referenceEntity)
        {
            _inputEntity = inputEntity;
            _referenceEntity = referenceEntity;
        }
    }
}
