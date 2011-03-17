using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DedupeNET.Core
{
    public abstract class MeasurementFunction
    {
        public string FirstString { get; set; }
        public string SecondString { get; set; }

        public MeasurementFunction(string firstString, string secondString)
        {
            FirstString = firstString;
            SecondString = secondString;
        }
    }
}
