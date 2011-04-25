using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.Configuration;
using System.Configuration;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DedupeNETSettings.IDFSettings.IDFProviders[0].Name);
            Console.ReadLine();
        }
    }
}
