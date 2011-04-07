using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.StringFunctions;
using DedupeNET.Providers;

namespace DedupeNET.Core
{
    public class TokenIDFCostFunction : CostFunction
    {
        public double InsertionFactor
        {
            get;
            set;
        }

        public TokenIDFCostFunction(double insertionFactor)
        {
            InsertionFactor = insertionFactor;
        }

        public override double GetCost(char a, char b)
        {
            throw new NotImplementedException();
        }

        public override double GetCost(string tokenA, string tokenB)
        {
            if (tokenA != string.Empty && tokenB != string.Empty)
            {
                var editDistance = new EditDistance(tokenA, tokenB).Distance();
                
                if (editDistance == 1)
                {
                    return editDistance * IDFProvider.Frequency(tokenA, 0) + MatchOffset;
                }
                else
                {
                    return editDistance * IDFProvider.Frequency(tokenA, 0) + NonMatchOffset;
                }
            }
            else if (tokenA == string.Empty)
            {
                return InsertionFactor * IDFProvider.Frequency(tokenB, 0) + InsertionOffset;
            }
            else
            {
                return IDFProvider.Frequency(tokenA, 0) + DeletionOffset;
            }
        }
    }
}
