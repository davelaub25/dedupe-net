using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.Core;
using DedupeNET.Utils;
using DedupeNET.Enum;

namespace DedupeNET.StringFunctions
{
    public class EditDistance : DistanceFunction
    {
        private double[,] editMatrix;

        private EditPath editPath;
        public EditPath EditPath
        {
            get
            {
                return editPath;
            }
        }

        public EditDistance(string firstString, string secondString)
            : base(firstString, secondString)
        {

        }

        public override double Distance()
        {
            return Distance(new UniformCostFunction(0, 1, 1, 1));
        }

        public double Distance(CostFunction costFunction)
        {
            editMatrix = new double[FirstString.Length + 1, SecondString.Length + 1];
            editPath = new EditPath();

            editMatrix[0, 0] = 0;

            for (int i = 1; i <= FirstString.Length; i++)
            {
                editMatrix[i, 0] = editMatrix[i - 1, 0] + costFunction.GetCost(FirstString[i - 1], (char)CharEnum.Empty);
            }

            for (int j = 1; j <= SecondString.Length; j++)
            {
                editMatrix[0, j] = editMatrix[0, j - 1] + costFunction.GetCost((char)CharEnum.Empty, SecondString[j - 1]);
            }

            double m1, m2, m3;

            for (int i = 1; i <= FirstString.Length; i++)
            {
                for (int j = 1; j <= SecondString.Length; j++)
                {
                    m1 = editMatrix[i - 1, j - 1] + costFunction.GetCost(FirstString[i - 1], SecondString[j - 1]);
                    m2 = editMatrix[i - 1, j] + costFunction.GetCost(FirstString[i - 1], (char)CharEnum.Empty);
                    m3 = editMatrix[i, j - 1] + costFunction.GetCost((char)CharEnum.Empty, SecondString[j - 1]);

                    editMatrix[i, j] = DeduplicationMath.Min(m1, m2, m3);
                }
            }

            BuildEditPath(costFunction);

            return editMatrix[FirstString.Length, SecondString.Length];
        }

        public double NormalizedDistance(UniformCostFunction costFunction)
        {
            List<double> Q = RequiredλValues(costFunction);

            while (true)
            {
                double λmed = DeduplicationMath.Median(Q);
                double solution = 0;

                if (solution == 0)
                {
                    return λmed;
                }
                else if (solution < 0)
                {
                    Q = Q.Where(n => n > λmed).ToList();
                }
                else if (solution > 0)
                {
                    Q = Q.Where(n => n < λmed).ToList();
                }
            }
        }

        private void BuildEditPath(CostFunction costFunction)
        {
            int i = FirstString.Length;
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
            }
        }

        private List<double> RequiredλValues(UniformCostFunction costFunction)
        {
            List<double> Q = new List<double>();

            int m = editPath.Deletions + editPath.Matches + editPath.NonMatches;
            int n = editPath.Insertions + editPath.Matches + editPath.NonMatches;

            int min = Math.Min(m, n);
            double λ = 0;

            for (int r = 0; r <= min; r++)
            {
                for (int s = 0; s <= min - r; s++)
                {
                    λ = (m * costFunction.DeletionCost + n * costFunction.InsertionCost + (costFunction.MatchCost + costFunction.InsertionCost + costFunction.DeletionCost) * r + (costFunction.NonMatchCost - costFunction.InsertionCost - costFunction.DeletionCost) * s) / (m + n - r - s);
                    Q.Add(λ);
                }
            }

            return Q;
        }
    }
}
