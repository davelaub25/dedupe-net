﻿using System;
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

            if (index != -1)
            {
                if (a != CharConstants.Empty && b != CharConstants.Empty)
                {
                    if (a == b)
                    {
                        return costMatrix.ElementAt(index).Cost + MatchOffset;
                    }
                    else
                    {
                        return costMatrix.ElementAt(index).Cost + NonMatchOffset;
                    }
                }
                else if (a != CharConstants.Empty && b == CharConstants.Empty)
                {
                    return costMatrix.ElementAt(index).Cost + DeletionOffset;
                }
                else
                {
                    return costMatrix.ElementAt(index).Cost + InsertionOffset;
                }
            }
            else
            {
                if (a != CharConstants.Empty && b != CharConstants.Empty)
                {
                    if (a == b)
                    {
                        return defaultMatchCost + MatchOffset;
                    }
                    else
                    {
                        return defaultNonMatchCost + NonMatchOffset;
                    }
                }
                else if (a != CharConstants.Empty && b == CharConstants.Empty)
                {
                    return defaultDeletionCost + DeletionOffset;
                }
                else
                {
                    return defaultInsertionCost + InsertionOffset;
                }
            }
        }

        public override double GetCost(string a, string b)
        {
            throw new NotImplementedException("Esta función de costo sólo opera al nivel de caracteres.");
        }
    }
}
