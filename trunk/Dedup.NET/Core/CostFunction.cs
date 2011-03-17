using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DedupeNET.Core
{
    public abstract class CostFunction
    {
        public abstract double GetCost(char a, char b);
    }
}
