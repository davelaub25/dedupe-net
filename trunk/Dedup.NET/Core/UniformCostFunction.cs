using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.Enum;

namespace DedupeNET.Core
{
    public class UniformCostFunction : CostFunction
    {
        private double matchCost;
        public double MatchCost
        {
            get { return matchCost; }
            set { matchCost = value; }
        }

        private double nonMatchCost;
        public double NonMatchCost
        {
            get { return nonMatchCost; }
            set { nonMatchCost = value; }
        }

        private double insertionCost;
        public double InsertionCost
        {
            get { return insertionCost; }
            set { insertionCost = value; }
        }

        private double deletionCost;
        public double DeletionCost
        {
            get { return deletionCost; }
            set { deletionCost = value; }
        }

        public UniformCostFunction(double matchCost, double nonMatchCost, double insertionCost, double deletionCost)
        {
            this.matchCost = matchCost;
            this.nonMatchCost = nonMatchCost;
            this.insertionCost = insertionCost;
            this.deletionCost = deletionCost;
        }

        public override double GetCost(char a, char b)
        {
            if (a != (char)CharEnum.Empty && b == (char)CharEnum.Empty)
            {
                return deletionCost + Offset;
            }
            else if (a == (char)CharEnum.Empty && b != (char)CharEnum.Empty)
            {
                return insertionCost + Offset;
            }
            else if(char.ToLower(a) == char.ToLower(b))
            {
                return matchCost + Offset;
            }
            else
            {
                return nonMatchCost + Offset;
            }
        }       
    }
}
