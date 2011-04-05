using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.StringFunctions;
using DedupeNET.Enum;
using DedupeNET.Core;
using DedupeNET.Utils;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<string> set1 = Tokenizer.QGrams(2, "asdf");
            HashSet<string> set2 = Tokenizer.QGrams(2, "holaa");

            MinHash mh = new MinHash(set1.Union(set2).Count(), 3);

            Console.WriteLine(mh.Similarity<string>(set1, set2));
            Console.ReadLine();
        }
    }
}
