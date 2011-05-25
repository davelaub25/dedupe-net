using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.Configuration;
using System.Configuration;
using DedupeNET.Providers;
using DedupeNET.Resources;
using DedupeNET.Resources.Data.SqlServer;
using DedupeNET.DataAccess;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(IDF.InverseDocumentFrequency("cosa", "Name"));
            Console.ReadLine();
        }
    }
}
