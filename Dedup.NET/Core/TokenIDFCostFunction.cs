using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.StringFunctions;

namespace DedupeNET.Core
{
    public class TokenIDFCostFunction:CostFunction
    {
        public double InsertionFactor
        {
            get;
            set
            {
                if (value < 0 || value > 1)
                {
                    throw new ArgumentException();
                }
            }
        }
        
        public override double GetCost(char a, char b)
        {
            throw new NotImplementedException();
        }

        public override double GetCost(string A, string B, int indexA, int indexB)
        {
            if (indexA != indexB)
            {
                return double.PositiveInfinity;
            }
            else if(A!=string.Empty&&B!=string.Empty)
            {
                return new EditDistance(A, B).Distance();
            }
            else if (A == string.Empty)
            {
                return InsertionFactor + InsertionOffset;
            }
            else
            {
                return 1;
            }
        }
    }
}
