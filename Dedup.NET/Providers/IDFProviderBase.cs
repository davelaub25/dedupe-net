using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DedupeNET.Providers
{
    public abstract class IDFProviderBase
    {        
        public abstract double Frequency(string token, int columnIndex);
        public abstract double Frecuency(string token, string columnName);

        public abstract double InverseDocumentFrequency(string token, int columnIndex);
        public abstract double InverseDocumentFrequency(string token, string columnName);
    }
}
