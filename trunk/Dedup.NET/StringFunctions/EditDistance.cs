using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.Core;
using DedupeNET.Utils;
using DedupeNET.Enum;

namespace DedupeNET.StringFunctions
{
    public class EditDistance : DistanceFunction<string>
    {
        private double[,] editMatrix;

        private Alignment editPath;
        public Alignment EditPath
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
            editMatrix = new double[FirstEntity.Length + 1, SecondEntity.Length + 1];
            editPath = new Alignment();

            editMatrix[0, 0] = 0;

            for (int i = 1; i <= FirstEntity.Length; i++)
            {
                editMatrix[i, 0] = editMatrix[i - 1, 0] + costFunction.GetCost(FirstEntity[i - 1], (char)CharEnum.Empty);
            }

            for (int j = 1; j <= SecondEntity.Length; j++)
            {
                editMatrix[0, j] = editMatrix[0, j - 1] + costFunction.GetCost((char)CharEnum.Empty, SecondEntity[j - 1]);
            }

            double m1, m2, m3;

            for (int i = 1; i <= FirstEntity.Length; i++)
            {
                for (int j = 1; j <= SecondEntity.Length; j++)
                {
                    m1 = editMatrix[i - 1, j - 1] + costFunction.GetCost(FirstEntity[i - 1], SecondEntity[j - 1]);
                    m2 = editMatrix[i - 1, j] + costFunction.GetCost(FirstEntity[i - 1], (char)CharEnum.Empty);
                    m3 = editMatrix[i, j - 1] + costFunction.GetCost((char)CharEnum.Empty, SecondEntity[j - 1]);

                    editMatrix[i, j] = DeduplicationMath.Min(m1, m2, m3);
                }
            }

            BuildEditPath(costFunction);

            return editMatrix[FirstEntity.Length, SecondEntity.Length];
        }

        public double NormalizedDistance(UniformCostFunction costFunction)
        {
            List<double> Q = RequiredLambdaValues(costFunction);

            while (true)
            {
                double lambdaMed = DeduplicationMath.Median(Q);
                costFunction.MatchOffset = lambdaMed;
                costFunction.NonMatchOffset = lambdaMed;

                double solution = Distance(costFunction) - lambdaMed * (FirstEntity.Length + SecondEntity.Length);

                if (solution == 0)
                {
                    return lambdaMed;
                }
                else if (solution < 0)
                {
                    Q = Q.Where(n => n < lambdaMed).ToList();
                }
                else if (solution > 0)
                {
                    Q = Q.Where(n => n > lambdaMed).ToList();
                }
            }
        }

        private void BuildEditPath(CostFunction costFunction)
        {
            int i = FirstEntity.Length;
            int j = SecondEntity.Length;

            while (i != 0 && j != 0)
            {
                if (editMatrix[i, j] == editMatrix[i - 1, j - 1] + costFunction.GetCost(FirstEntity[i - 1], SecondEntity[j - 1]))
                {
                    EditPath.Prepend(new EditOperation(FirstEntity[i - 1], SecondEntity[j - 1]));
                    i--;
                    j--;
                }
                else if (editMatrix[i, j] == editMatrix[i - 1, j] + costFunction.GetCost(FirstEntity[i - 1], (char)CharEnum.Empty))
                {
                    EditPath.Prepend(new EditOperation(FirstEntity[i - 1], (char)CharEnum.Empty));
                    i--;
                }
                else
                {
                    EditPath.Prepend(new EditOperation((char)CharEnum.Empty, SecondEntity[j - 1]));
                    j--;
                }
            }

            while (i != 0)
            {
                EditPath.Prepend(new EditOperation(FirstEntity[i - 1], (char)CharEnum.Empty));
                i--;
            }

            while (j != 0)
            {
                EditPath.Prepend(new EditOperation((char)CharEnum.Empty, SecondEntity[j - 1]));
                j--;
            }
        }

        private List<double> RequiredLambdaValues(UniformCostFunction costFunction)
        {
            List<double> Q = new List<double>();

            int m = FirstEntity.Length;
            int n = SecondEntity.Length;
            int minLength = Math.Min(m, n);
            double lambda = 0;

            for (int r = 0; r <= minLength; r++)
            {
                for (int s = 0; s <= minLength - r; s++)
                {
                    lambda = (m * costFunction.DeletionCost + n * costFunction.InsertionCost + (costFunction.MatchCost - costFunction.InsertionCost - costFunction.DeletionCost) * r + (costFunction.NonMatchCost - costFunction.InsertionCost - costFunction.DeletionCost) * s) / (m + n - r - s);
                    Q.Add(lambda);
                }
            }

            return Q;
        }
    }
}
