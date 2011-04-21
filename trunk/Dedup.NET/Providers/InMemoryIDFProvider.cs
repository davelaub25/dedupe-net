using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DedupeNET.Providers
{
    public class InMemoryIDFProvider : IDFProvider
    {
        public override double Frequency(string token, int columnIndex)
        {
            throw new NotImplementedException();
        }

        public override double Frecuency(string token, string columnName)
        {
            throw new NotImplementedException();
        }

        public override double IDF(string token, int columnIndex)
        {
            throw new NotImplementedException();
        }

        public override double IDF(string token, string columnName)
        {
            throw new NotImplementedException();
        }
    }
}
