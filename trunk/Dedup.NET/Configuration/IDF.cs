using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DedupeNET.Configuration
{
    public class IDF : ConfigurationSection
    {
        private static IDF _instance =
           ConfigurationManager.GetSection("DedupeNET/IDF") as IDF;

        private IDF() { }

        public static IDF Instance
        {
            get { return _instance; }
        }
    }
}
