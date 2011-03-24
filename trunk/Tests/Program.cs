using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.StringFunctions;
using DedupeNET.Enum;
using DedupeNET.Core;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            UniformCostFunction ucf = new UniformCostFunction(0, 5, 9, 7);
            EditDistance ed = new EditDistance("aba", "bab");

            Console.WriteLine(ed.NormalizedDistance(ucf));
            Console.WriteLine(ed.EditPath.Length);

            /*foreach (EditOperation item in ed.EditPath.editPath)
            {
                Console.WriteLine("(" + item.A + ", " + item.B + ")");
            }*/

            Console.ReadLine();
        }
    }
}
