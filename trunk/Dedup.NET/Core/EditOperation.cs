using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.Enum;

namespace DedupeNET.Core
{
    public class EditOperation : IEquatable<EditOperation>
    {
        private char _inputChar;
        public char InputChar
        {
            get { return _inputChar; }
            set { _inputChar = value; }
        }

        private char _referenceChar;
        public char ReferenceChar
        {
            get { return _referenceChar; }
            set { _referenceChar = value; }
        }

        private double _cost;
        public double Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }

        public EditOperation(char inputChar, char referenceChar)
        {
            ValidateChars(inputChar, referenceChar);
            _inputChar = char.ToLower(inputChar); ;
            _referenceChar = char.ToLower(referenceChar);
        }

        public EditOperation(char inputChar, char referenceChar, double cost)
        {
            ValidateChars(inputChar, referenceChar);
            _inputChar = char.ToLower(inputChar); ;
            _referenceChar = char.ToLower(referenceChar);
            _cost = cost;
        }

        public bool Equals(EditOperation other)
        {
            return (char.ToLower(InputChar) == char.ToLower(other.InputChar) && char.ToLower(ReferenceChar) == char.ToLower(other.ReferenceChar));
        }

        public override bool Equals(object other)
        {
            bool equal = false;

            try
            {
                equal = Equals((EditOperation)other);
            }
            catch { }

            return equal;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(EditOperation edOp1, EditOperation edOp2)
        {
            return edOp1.Equals(edOp2);
        }

        public static bool operator !=(EditOperation edOp1, EditOperation edOp2)
        {
            return !edOp1.Equals(edOp2);
        }

        private void ValidateChars(char inputChar, char referenceChar)
        {
            if (inputChar == CharConstants.Empty && referenceChar == CharConstants.Empty)
            {
                throw new ArgumentException("Ambos caracteres no pueden ser vacíos.");
            }
        }
    }
}
