using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.Core;
using DedupeNET.Utils;
using DedupeNET.Configuration;

namespace DedupeNET.StringFunctions
{
    public class TokenEditDistance : DistanceFunction<string>
    {
        private List<string> tokInputString;
        private List<string> tokReferenceString;

        private double[,] editMatrix;

        public TokenEditDistance(string inputString, string referenceString)
            : base(inputString, referenceString)
        {
            char[] separators = GeneralSettings.Settings.TokenSeparators.ToCharArray();
            tokInputString = Tokenizer.Tokens(inputString, separators);
            tokReferenceString = Tokenizer.Tokens(referenceString, separators);
        }

        public TokenEditDistance(string inputString, string referenceString, char[] separators)
            : base(inputString, referenceString)
        {
            tokInputString = Tokenizer.Tokens(inputString, separators);
            tokReferenceString = Tokenizer.Tokens(inputString, separators);
        }

        public override double Distance()
        {
            return Distance(new TokenIDFCostFunction(1));
        }

        public double Distance(TokenIDFCostFunction costFunction)
        {
            editMatrix = new double[tokInputString.Count + 1, tokReferenceString.Count + 1];

            editMatrix[0, 0] = 0;

            for (int i = 1; i <= tokInputString.Count; i++)
            {
                editMatrix[i, 0] = editMatrix[i - 1, 0] + costFunction.GetCost(tokInputString[i - 1], string.Empty);
            }

            for (int j = 1; j <= tokReferenceString.Count; j++)
            {
                editMatrix[0, j] = editMatrix[0, j - 1] + costFunction.GetCost(string.Empty, tokReferenceString[j - 1]);
            }

            double m1, m2, m3;

            for (int i = 1; i <= tokInputString.Count; i++)
            {
                for (int j = 1; j <= tokReferenceString.Count; j++)
                {
                    m1 = editMatrix[i - 1, j - 1] + costFunction.GetCost(tokInputString[i - 1], tokReferenceString[j - 1]);
                    m2 = editMatrix[i - 1, j] + costFunction.GetCost(tokInputString[i - 1], string.Empty);
                    m3 = editMatrix[i, j - 1] + costFunction.GetCost(string.Empty, tokReferenceString[j - 1]);

                    editMatrix[i, j] = DeduplicationMath.Min(m1, m2, m3);
                }
            }

            BuildEditPath(costFunction);

            return editMatrix[tokInputString.Count, tokReferenceString.Count];
        }

        private void BuildEditPath(CostFunction costFunction)
        {
            /*int i = FirstString.Length;
            int j = SecondString.Length;

            while (i != 0 && j != 0)
            {
                if (editMatrix[i, j] == editMatrix[i - 1, j - 1] + costFunction.GetCost(FirstString[i - 1], SecondString[j - 1]))
                {
                    EditPath.Prepend(new EditOperation(FirstString[i - 1], SecondString[j - 1]));
                    i--;
                    j--;
                }
                else if (editMatrix[i, j] == editMatrix[i - 1, j] + costFunction.GetCost(FirstString[i - 1], (char)CharEnum.Empty))
                {
                    EditPath.Prepend(new EditOperation(FirstString[i - 1], (char)CharEnum.Empty));
                    i--;
                }
                else
                {
                    EditPath.Prepend(new EditOperation((char)CharEnum.Empty, SecondString[j - 1]));
                    j--;
                }
            }

            while (i != 0)
            {
                EditPath.Prepend(new EditOperation(FirstString[i - 1], (char)CharEnum.Empty));
                i--;
            }

            while (j != 0)
            {
                EditPath.Prepend(new EditOperation((char)CharEnum.Empty, SecondString[j - 1]));
                j--;
            }*/
        }
    }
}
