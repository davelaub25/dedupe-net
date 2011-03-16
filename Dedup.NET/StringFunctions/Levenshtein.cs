using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.Core;

namespace DedupeNET.StringFunctions
{
    public class Levenshtein : DistanceFunction
    {
        public Levenshtein(string firstString, string secondString)
            : base(firstString, secondString)
        {
        }
        
        public override double Distance()
        {
            
        }
    }
}
