using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.Core;
using DedupeNET.Utils;

namespace DedupeNET.StringFunctions
{
    public class EditDistance : DistanceFunction
    {
        private double[,] editMatrix;
        
        public EditDistance(string firstString, string secondString)
            : base(firstString, secondString)
        {
            editMatrix = new double[firstString.Length + 1, secondString.Length + 1];
        }

        public override double Distance(UniformCostFunction costFunction)
        {
            editMatrix[0, 0] = 0;
            
            for (int i = 1; i <= FirstString.Length; i++)
            {
                editMatrix[i, 0] = editMatrix[i - 1, 0] + costFunction.DeletionCost;
            }

            for (int j = 0; j <= SecondString.Length; j++)
            {
                editMatrix[0, j] = editMatrix[0, j - 1] + costFunction.InsertionCost;
            }

            double m1, m2, m3;

            for (int i = 1; i <= FirstString.Length; i++)
            {
                for (int j = 1; j <= SecondString.Length; j++)
                {
                    m1 = editMatrix[i - 1, j - 1] + costFunction.GetCost(FirstString[i], SecondString[j]);
                    m2 = editMatrix[i - 1, j] + costFunction.DeletionCost;
                    m3 = editMatrix[i, j - 1] + costFunction.InsertionCost;
                    
                    editMatrix[i, j] = DeduplicationMath.Min(m1, m2, m3);
                }
            }

            return editMatrix[FirstString.Length, SecondString.Length];
        }

        public double NormalizedDistance()
        {
            double 
        }
    }
}
