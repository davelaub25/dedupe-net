using System.Collections.Generic;
using System.Data;
using System.Linq;
using DedupeNET.Core;
using DedupeNET.StringFunctions;
using DedupeNET.Utils;
using System.Configuration;
using System;

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
            throw new NotImplementedException();
        }

        public double MinimumCostTransformationSequence()
        {
            double result=0;
            TokenEditDistance ted;

            foreach (DataColumn colum in FirstEntity.Table.Columns)
            {
                ted = new TokenEditDistance(FirstEntity[colum].ToString(), SecondEntity[colum].ToString());
                result += ted.Distance();
            }

            return result;
        }

        public List<string> TokenSet()
        {
            List<string> result = new List<string>();
            string separators = (string)ConfigurationManager.GetSection("general");

            /*foreach (DataColumn colum in FirstEntity.Table.Columns)
            {
                //result = result.Union(Tokenizer.Tokens(FirstEntity[colum].ToString(), ""));
            }*/

            return result;
        }
    }
}
