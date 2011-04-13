using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.Core;
using System.Data;
using DedupeNET.Utils;
using DedupeNET.Providers;

namespace DedupeNET.TupleFunctions
{
    public class FuzzyMatchApproximate : SimilarityFunction<DataRow>
    {
        private int _qgramSize;
        public int QGramSize
        {
            get { return _qgramSize; }
            set { _qgramSize = value; }
        }

        private int _numHashFunctions;
        public int NumHashFunctions
        {
            get { return _numHashFunctions; }
            set { _numHashFunctions = value; }
        }

        public FuzzyMatchApproximate(DataRow inputTuple, DataRow referenceTuple)
            : base(inputTuple, referenceTuple)
        {
        }

        public override double Similarity()
        {
            double similarity = 0;

            foreach (DataColumn column in InputEntity.Table.Columns)
            {
                IEnumerable<string> inputColumnTokens = Tokenizer.Tokens(InputEntity[column].ToString());

                foreach (string inputToken in inputColumnTokens)
                {
                    List<string> referenceColumnTokens = Tokenizer.Tokens(ReferenceEntity[column].ToString());
                    double maxMinHash = 0;

                    foreach (string referenceToken in referenceColumnTokens)
                    {
                        int universeSize = inputToken.Length + referenceToken.Length - 2 * QGramSize + 2;
                        MinHash minHash = new MinHash(universeSize, NumHashFunctions);
                        double fmsApprox = (2 / QGramSize) * minHash.Similarity(Tokenizer.QGrams(QGramSize, inputToken), Tokenizer.QGrams(QGramSize, referenceToken)) * (1 - (1 / QGramSize));
                        maxMinHash = Math.Max(maxMinHash, fmsApprox);
                    }

                     double inputTokenWeight = IDFProvider.IDF(inputToken, 1);
                     similarity += inputTokenWeight * maxMinHash;
                }
            }

             double inputTokensTotalCost = GetInputTokenSet().Sum(e => IDFProvider.IDF(e, 1));
             similarity *= (1 / inputTokensTotalCost);

             return similarity;
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
