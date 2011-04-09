using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DedupeNET.Core
{
    public struct EditOperation:IEquatable<EditOperation>
    {
        public char A { get; set; }
        public char B { get; set; }
        public double Cost { get; set; }
        
        public EditOperation(char a, char b)
        {
            A = char.ToLower(a); ;
            B = char.ToLower(b);
        }

        public EditOperation(char a, char b, double cost)
        {
            A = char.ToLower(a); ;
            B = char.ToLower(b);
            Cost = cost;
        }

        public bool Equals(EditOperation other)
        {
            return (char.ToLower(A) == char.ToLower(other.A) && char.ToLower(B) == char.ToLower(other.B));
        }

        public override bool Equals(object other)
        {
            return Equals((EditOperation)other);
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
