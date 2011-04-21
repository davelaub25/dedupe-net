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

        public EditDistance(string inputString, string referenceString)
            : base(inputString, referenceString)
        {

        }

        public override double Distance()
        {
            return Distance(new UniformCostFunction(0, 1, 1, 1));
        }

        public double Distance(CostFunction costFunction)
        {
            editMatrix = new double[InputEntity.Length + 1, ReferenceEntity.Length + 1];
            editPath = new Alignment();

            editMatrix[0, 0] = 0;

            for (int i = 1; i <= InputEntity.Length; i++)
            {
                editMatrix[i, 0] = editMatrix[i - 1, 0] + costFunction.GetCost(InputEntity[i - 1], CharConstants.Empty);
            }

            for (int j = 1; j <= ReferenceEntity.Length; j++)
            {
                editMatrix[0, j] = editMatrix[0, j - 1] + costFunction.GetCost(CharConstants.Empty, ReferenceEntity[j - 1]);
            }

            double m1, m2, m3;

            for (int i = 1; i <= InputEntity.Length; i++)
            {
                for (int j = 1; j <= ReferenceEntity.Length; j++)
                {
                    m1 = editMatrix[i - 1, j - 1] + costFunction.GetCost(InputEntity[i - 1], ReferenceEntity[j - 1]);
                    m2 = editMatrix[i - 1, j] + costFunction.GetCost(InputEntity[i - 1], CharConstants.Empty);
                    m3 = editMatrix[i, j - 1] + costFunction.GetCost(CharConstants.Empty, ReferenceEntity[j - 1]);

                    editMatrix[i, j] = DeduplicationMath.Min(m1, m2, m3);
                }
            }

            BuildEditPath(costFunction);

            return editMatrix[InputEntity.Length, ReferenceEntity.Length];
        }

        public double NormalizedDistance(UniformCostFunction costFunction)
        {
            ValidateUniformCostFunction(costFunction);

            List<double> Q = RequiredLambdaValues(costFunction);

            while (true)
            {
                double lambdaMed = DeduplicationMath.Median(Q);
                costFunction.MatchOffset = lambdaMed;
                costFunction.NonMatchOffset = lambdaMed;

                double solution = Distance(costFunction) - lambdaMed * (InputEntity.Length + ReferenceEntity.Length);

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

        private void ValidateUniformCostFunction(UniformCostFunction costFunction)
        {
            if (costFunction.MatchCost < 0 || costFunction.NonMatchCost < 0 || costFunction.InsertionCost < 0 || costFunction.DeletionCost < 0)
            {
                throw new ArgumentOutOfRangeException("Todos los valores de la función de costo deben ser mayores o iguales a cero.");
            }
        }

        private void BuildEditPath(CostFunction costFunction)
        {
            int i = InputEntity.Length;
            int j = ReferenceEntity.Length;

            while (i != 0 && j != 0)
            {
                if (editMatrix[i, j] == editMatrix[i - 1, j - 1] + costFunction.GetCost(InputEntity[i - 1], ReferenceEntity[j - 1]))
                {
                    EditPath.Prepend(new EditOperation(InputEntity[i - 1], ReferenceEntity[j - 1]));
                    i--;
                    j--;
                }
                else if (editMatrix[i, j] == editMatrix[i - 1, j] + costFunction.GetCost(InputEntity[i - 1], CharConstants.Empty))
                {
                    EditPath.Prepend(new EditOperation(InputEntity[i - 1], CharConstants.Empty));
                    i--;
                }
                else
                {
                    EditPath.Prepend(new EditOperation(CharConstants.Empty, ReferenceEntity[j - 1]));
                    j--;
                }
            }

            while (i != 0)
            {
                EditPath.Prepend(new EditOperation(InputEntity[i - 1], CharConstants.Empty));
                i--;
            }

            while (j != 0)
            {
                EditPath.Prepend(new EditOperation(CharConstants.Empty, ReferenceEntity[j - 1]));
                j--;
            }
        }

        private List<double> RequiredLambdaValues(UniformCostFunction costFunction)
        {
            List<double> Q = new List<double>();

            int m = InputEntity.Length;
            int n = ReferenceEntity.Length;
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
