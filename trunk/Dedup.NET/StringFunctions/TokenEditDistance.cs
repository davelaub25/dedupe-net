using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.Core;
using DedupeNET.Utils;

namespace DedupeNET.StringFunctions
{
    public class TokenEditDistance : DistanceFunction
    {
        private List<string> tokFirstString;
        private List<string> tokSecondString;

        private double[,] editMatrix;

        public TokenEditDistance(string firstString, string secondString)
            : base(firstString, secondString)
        {
            string[] separators = new string[] { " ", ".", "," };
            tokFirstString = Tokenizer.Tokens(firstString, separators);
            tokSecondString = Tokenizer.Tokens(firstString, separators);
        }

        public TokenEditDistance(string firstString, string secondString, string[] separators)
            : base(firstString, secondString)
        {
            tokFirstString = Tokenizer.Tokens(firstString, separators);
            tokSecondString = Tokenizer.Tokens(firstString, separators);
        }

        public double Distance(TokenIDFCostFunction costFunction)
        {
            editMatrix = new double[tokFirstString.Count + 1, tokSecondString.Count + 1];

            editMatrix[0, 0] = 0;

            for (int i = 1; i <= tokFirstString.Count; i++)
            {
                editMatrix[i, 0] = editMatrix[i - 1, 0] + costFunction.GetCost(tokFirstString[i-1], string.Empty);
            }

            for (int j = 1; j <= SecondEntity.Length; j++)
            {
                editMatrix[0, j] = editMatrix[0, j - 1] + costFunction.GetCost(string.Empty, tokSecondString[j-1]);
            }

            double m1, m2, m3;

            for (int i = 1; i <= FirstEntity.Length; i++)
            {
                for (int j = 1; j <= SecondEntity.Length; j++)
                {
                    m1 = editMatrix[i - 1, j - 1] + costFunction.GetCost(tokFirstString[i-1], tokSecondString[j-1]);
                    m2 = editMatrix[i - 1, j] + costFunction.GetCost(tokFirstString[i - 1], string.Empty);
                    m3 = editMatrix[i, j - 1] + costFunction.GetCost(string.Empty, tokSecondString[j - 1]);

                    editMatrix[i, j] = DeduplicationMath.Min(m1, m2, m3);
                }
            }

            BuildEditPath(costFunction);

            return editMatrix[tokFirstString.Count, tokSecondString.Count];
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

        public override double Distance()
        {
            throw new NotImplementedException();
        }
    }
}
