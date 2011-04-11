using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DedupeNET.Core
{
    public class EditOperation : IEquatable<EditOperation>
    {
        private char _a;
        public char A
        {
            get { return _a; }
            set { _a = value; }
        }

        private char _b;
        public char B
        {
            get { return _b; }
            set { _b = value; }
        }

        private double _cost;
        public double Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }

        public EditOperation(char a, char b)
        {
            _a = char.ToLower(a); ;
            _b = char.ToLower(b);
        }

        public EditOperation(char a, char b, double cost)
        {
            _a = char.ToLower(a); ;
            _b = char.ToLower(b);
            _cost = cost;
        }

        public bool Equals(EditOperation other)
        {
            return (char.ToLower(A) == char.ToLower(other.A) && char.ToLower(B) == char.ToLower(other.B));
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
    }
}
