using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.StringFunctions;
using DedupeNET.Enum;
using DedupeNET.Core;
using DedupeNET.Utils;
using DedupeNET.TupleFunctions;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            FuzzyMatch fm = new FuzzyMatch(null, null);
            fm.TokenSet();

            //Console.WriteLine(ted.Distance());
            Console.ReadLine();
        }
    }
}
