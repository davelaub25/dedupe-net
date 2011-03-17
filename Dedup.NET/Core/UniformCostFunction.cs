using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DedupeNET.Core
{
    public class UniformCostFunction : CostFunction
    {
        public double MatchCost { get; set; }
        public double NonMatchCost { get; set; }
        public double InsertionCost { get; set; }
        public double DeletionCost { get; set; }

        public UniformCostFunction(double matchCost, double nonMatchCost, double insertionCost, double deletionCost)
        {
            MatchCost = matchCost;
            NonMatchCost = nonMatchCost;
            InsertionCost = insertionCost;
            DeletionCost = deletionCost;
        }

        public override double GetCost(char a, char b)
        {
            if (a == b)
            {
                return MatchCost;
            }
            else
            {
                return NonMatchCost;
            }
        }
    }
}
