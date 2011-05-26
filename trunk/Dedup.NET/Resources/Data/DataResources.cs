using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DedupeNET.Resources.Data
{
    public static class DataResources
    {
        public static string ColumnTokensCountCommand
        {
            get { return "Data|ColumnTokensCount.sql"; }
        }

        public static string RecordCountCommand
        {
            get { return "Data|RecordCount.sql"; }
        }
    }
}
