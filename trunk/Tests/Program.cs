using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.Configuration;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DedupeNETSettings.GeneralSettings.StopCharacters);
            Console.ReadLine();
        }
    }
}
