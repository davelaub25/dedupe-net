using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DedupeNET.Providers
{
    public abstract class IDFProviderBase
    {        
        public abstract int Frecuency(string token, string columnName);
        public abstract double InverseDocumentFrequency(string token, string columnName);
    }
}
