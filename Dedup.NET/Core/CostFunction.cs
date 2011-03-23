using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DedupeNET.Core
{
    public abstract class CostFunction
    {
        public double Offset { get; set; }

        public abstract double GetCost(char a, char b);
    }
}
