using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DedupeNET.Core
{
    public class TokenOperation
    {
        private string _inputToken;
        public string InputToken
        {
            get { return _inputToken; }
            set { _inputToken = value; }
        }

        private string _referenceToken;
        public string ReferenceToken
        {
            get { return _referenceToken; }
            set { _referenceToken = value; }
        }

        private double _cost;
        public double Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }

        public TokenOperation(string inputToken, string referenceToken)
        {
            ValidateTokens(inputToken, referenceToken);
            _inputToken = inputToken;
            _referenceToken = referenceToken;
        }

        public TokenOperation(string inputToken, string referenceToken, double cost)
        {
            ValidateTokens(inputToken, referenceToken);
            _inputToken = inputToken;
            _referenceToken = referenceToken;
            _cost = cost;
        }

        private void ValidateTokens(string inputToken, string referenceToken)
        {
            if (inputToken == string.Empty && referenceToken == string.Empty)
            {
                throw new ArgumentException("Ambos tokens no pueden ser vacíos.");
            }
        }
    }
}
