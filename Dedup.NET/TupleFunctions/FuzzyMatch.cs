using System.Collections.Generic;
using System.Data;
using System.Linq;
using DedupeNET.Core;
using DedupeNET.StringFunctions;
using DedupeNET.Utils;
using System.Configuration;
using System;
using DedupeNET.Configuration;
using DedupeNET.Providers;

namespace DedupeNET.TupleFunctions
{
    public class FuzzyMatch : SimilarityFunction<DataRow>
    {
        public FuzzyMatch(DataRow inputTuple, DataRow referenceTuple)
            : base(inputTuple, referenceTuple)
        {
        }

        public override double Similarity()
        {
            IEnumerable<string> inputTokenSet = InputTokenSet();

            double minimumTotalCost = MinimumCostTransformationSequence();
            double weightsTotalCost = 0;

            foreach (string token in inputTokenSet)
            {
                weightsTotalCost += IDFProvider.IDF(token, 1);
            }

            return 1 - Math.Min(minimumTotalCost / weightsTotalCost, 1);
        }

        private double MinimumCostTransformationSequence()
        {
            double result=0;
            TokenEditDistance ted;

            foreach (DataColumn colum in InputEntity.Table.Columns)
            {
                ted = new TokenEditDistance(InputEntity[colum].ToString(), ReferenceEntity[colum].ToString());
                result += ted.Distance();
            }

            return result;
        }

        private IEnumerable<string> InputTokenSet()
        {
            IEnumerable<string> result = new List<string>();
            char[] separators = GeneralSettings.Settings.TokenSeparators.ToCharArray();

            foreach (DataColumn colum in InputEntity.Table.Columns)
            {
                result = result.Union(Tokenizer.Tokens(InputEntity[colum].ToString(), separators));
            }

            return result;
        }
    }
}
