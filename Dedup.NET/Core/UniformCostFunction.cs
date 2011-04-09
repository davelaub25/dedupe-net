using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.Enum;

namespace DedupeNET.Core
{
    public class UniformCostFunction : CostFunction
    {
        private double _matchCost;
        public double MatchCost
        {
            get { return _matchCost; }
            set { _matchCost = value; }
        }

        private double _nonMatchCost;
        public double NonMatchCost
        {
            get { return _nonMatchCost; }
            set { _nonMatchCost = value; }
        }

        private double _insertionCost;
        public double InsertionCost
        {
            get { return _insertionCost; }
            set { _insertionCost = value; }
        }

        private double _deletionCost;
        public double DeletionCost
        {
            get { return _deletionCost; }
            set { _deletionCost = value; }
        }

        public UniformCostFunction(double matchCost, double nonMatchCost, double insertionCost, double deletionCost)
        {
            _matchCost = matchCost;
            _nonMatchCost = nonMatchCost;
            _insertionCost = insertionCost;
            _deletionCost = deletionCost;
        }

        public override double GetCost(char a, char b)
        {
            if (a != (char)CharEnum.Empty && b == (char)CharEnum.Empty)
            {
                return _deletionCost + DeletionOffset;
            }
            else if (a == (char)CharEnum.Empty && b != (char)CharEnum.Empty)
            {
                return _insertionCost + InsertionOffset;
            }
            else if(char.ToLower(a) == char.ToLower(b))
            {
                return _matchCost + MatchOffset;
            }
            else
            {
                return _nonMatchCost + NonMatchOffset;
            }
        }

        public override double GetCost(string a, string b)
        {
            throw new NotImplementedException();
        }
    }
}
