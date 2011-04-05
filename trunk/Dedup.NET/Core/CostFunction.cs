using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DedupeNET.Core
{
    public abstract class CostFunction
    {
        public double MatchOffset { get; set; }
        public double NonMatchOffset { get; set; }
        public double DeletionOffset { get; set; }
        public double InsertionOffset { get; set; }

        public abstract double GetCost(char a, char b);
        public abstract double GetCost(string a, string b);
    }
}
