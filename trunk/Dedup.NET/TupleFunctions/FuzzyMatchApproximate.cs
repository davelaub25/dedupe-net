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
        private short _qgramSize = 2;
        public short QgramSize
        {
            get { return _qgramSize; }
            set { _qgramSize = value; }
        }

        private short _numHashFunctions = 3;
        public short NumHashFunctions
        {
            get { return _numHashFunctions; }
            set { _numHashFunctions = value; }
        }

        public FuzzyMatchApproximate(DataRow inputTuple, DataRow referenceTuple)
            : base(inputTuple, referenceTuple)
        {
        }

        public FuzzyMatchApproximate(DataRow inputTuple, DataRow referenceTuple, short qgramSize, short numHashFunctions)
            : base(inputTuple, referenceTuple)
        {
            if (numHashFunctions <= 0)
            {
                throw new ArgumentOutOfRangeException("El número de funciones hash debe ser mayor que cero.");
            }

            _qgramSize = qgramSize;
            _numHashFunctions = numHashFunctions;
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
                        int universeSize = inputToken.Length + referenceToken.Length - 2 * QgramSize + 2;
                        MinHash minHash = new MinHash(universeSize, NumHashFunctions);
                        double fmsApprox = (2 / QgramSize) * minHash.Similarity(Tokenizer.QGrams(QgramSize, inputToken), Tokenizer.QGrams(QgramSize, referenceToken)) * (1 - (1 / QgramSize));
                        maxMinHash = Math.Max(maxMinHash, fmsApprox);
                    }

                     double inputTokenWeight = 1/*IDFProvider.IDF(inputToken, 1)*/;
                     similarity += inputTokenWeight * maxMinHash;
                }
            }

             double inputTokensTotalCost = GetInputTokenSet().Sum(e => 1/*IDFProvider.IDF(e, 1)*/);
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
