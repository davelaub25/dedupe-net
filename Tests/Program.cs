using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.Configuration;
using System.Configuration;
using DedupeNET.Providers;
using DedupeNET.Resources;
using DedupeNET.DataAccess;
using DedupeNET.Resources.Data;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DedupeNETResources.GetStringResource(DataResources.RecordCountCommand));
        }
    }
}
