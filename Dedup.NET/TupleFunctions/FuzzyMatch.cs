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
            IEnumerable<string> inputTokenSet = GetInputTokenSet();

            double minimumTotalCost = MinimumCostTransformationSequence();
            double inputTokensTotalCost = inputTokenSet.Sum(e => IDFProvider.IDF(e, 1));

            return 1 - Math.Min(minimumTotalCost / inputTokensTotalCost, 1);
        }

        private double MinimumCostTransformationSequence()
        {
            double result = 0;
            TokenEditDistance ted;

            foreach (DataColumn column in InputEntity.Table.Columns)
            {
                ted = new TokenEditDistance(InputEntity[column].ToString(), ReferenceEntity[column].ToString());
                result += ted.Distance();
            }

            return result;
        }

        private IEnumerable<string> GetInputTokenSet()
        {
            IEnumerable<string> result = new List<string>();

            foreach (DataColumn colum in InputEntity.Table.Columns)
            {
                result = result.Union(Tokenizer.Tokens(InputEntity[colum].ToString()));
            }

            return result;
        }
    }
}
