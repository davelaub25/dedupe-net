using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DedupeNET.Providers
{
    public static class IDFProvider
    {
        public static double Frequency(string token, int columnIndex)
        {
            return 1;
        }

        public static double IDF(string token, int columnIndex)
        {
            return Math.Log10(1 / Frequency(token, columnIndex));
        }
    }
}
