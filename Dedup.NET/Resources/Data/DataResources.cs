using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.Resources.Data.SqlServer;
using DedupeNET.Resources.Data.Common;

namespace DedupeNET.Resources.Data
{
    public class DataResources
    {
        private static DataResources _instance = new DataResources();
        public static DataResources Instance
        {
            get { return _instance; }
        }

        private DataResources() { }

        public SqlServerResources SqlServer
        {
            get { return SqlServerResources.Instance; }
        }

        public CommonDataResources Common
        {
            get { return CommonDataResources.Instance; }
        }
    }
}
