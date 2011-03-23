using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.Enum;

namespace DedupeNET.Core
{
    public class VariableCostFunction : CostFunction
    {
        private List<EditOperation> costMatrix;
        private double defaultMatchCost;
        private double defaultNonMatchCost;
        private double defaultInsertionCost;
        private double defaultDeletionCost;

        public VariableCostFunction(List<EditOperation> costMatrix, double defaultMatchCost, double defaultNonMatchCost, double defaultInsertionCost, double defaultDeletionCost)
            : base()
        {
            this.costMatrix = costMatrix;
            this.defaultMatchCost = defaultMatchCost;
            this.defaultNonMatchCost = defaultNonMatchCost;
            this.defaultInsertionCost = defaultInsertionCost;
            this.defaultDeletionCost = defaultDeletionCost;
        }

        public override double GetCost(char a, char b)
        {
            int index = costMatrix.IndexOf(new EditOperation(a, b));
            double cost;

            if (index != -1)
            {
                 cost = costMatrix.ElementAt(index).Cost;
            }
            else
            {
                if (a != (char)CharEnum.Empty && b != (char)CharEnum.Empty)
                {
                    if (a == b)
                    {
                        cost = defaultMatchCost;
                    }
                    else
                    {
                        cost = defaultNonMatchCost;
                    }
                }
                else if (a != (char)CharEnum.Empty && b == (char)CharEnum.Empty)
                {
                    cost = defaultDeletionCost;
                }
                else
                {
                    cost = defaultInsertionCost;
                }
            }

            return cost + Offset;
        }
    }
}
