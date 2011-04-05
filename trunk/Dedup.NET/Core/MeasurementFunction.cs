using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DedupeNET.Core
{
    public abstract class MeasurementFunction
    {
        private T _firstEntity;
        public T FirstEntity
        { 
            get
            {
 
            } 
        }

        private T _secondEntity;
        
        public T SecondEntity { get; set; }

        public MeasurementFunction(T firstEntity, T secondEntity)
        {
            FirstEntity = firstString;
            SecondEntity = secondString;
        }
    }
}
