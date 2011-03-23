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
            List<EditOperation> costMatrix = new List<EditOperation>();

            costMatrix.Add(new EditOperation('a', 'a', 0));
            costMatrix.Add(new EditOperation('b', 'b', 0));
            costMatrix.Add(new EditOperation('a', 'b', 3));
            costMatrix.Add(new EditOperation('b', 'a', 3));

            costMatrix.Add(new EditOperation('a', (char)CharEnum.Empty, 2));
            costMatrix.Add(new EditOperation('b', (char)CharEnum.Empty, 2));

            costMatrix.Add(new EditOperation((char)CharEnum.Empty, 'a', 2));
            costMatrix.Add(new EditOperation((char)CharEnum.Empty, 'b', 2));

            VariableCostFunction vcf = new VariableCostFunction(costMatrix, 0, 1, 1, 1);

            EditDistance ed = new EditDistance("abbb", "aaab");

            //Console.WriteLine(ed.Distance(vcf));
            Console.WriteLine(ed.NormalizedDistance(vcf));
            Console.WriteLine(ed.EditPath.Length);

            foreach (EditOperation item in ed.EditPath.editPath)
            {
                Console.WriteLine("(" + item.A + ", " + item.B + ")");
            }

            Console.ReadLine();
        }
    }
}
