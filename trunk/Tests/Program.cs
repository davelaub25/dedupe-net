using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.Configuration;
using System.Configuration;
using DedupeNET.Providers;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            InMemoryIDFProvider iip = new InMemoryIDFProvider();
            Console.WriteLine(iip.ColumnTokenSetCommand);
        }
    }
}
