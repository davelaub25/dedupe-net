using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace DedupeNET.Resources.Data.SqlServer
{
    public class SqlServerResources
    {
        private static SqlServerResources _instance = new SqlServerResources();
        public static SqlServerResources Instance
        {
            get { return _instance; }
        }

        private SqlServerResources() { }

        private string _columnTokensCountCommand;
        public string ColumnTokensCountCommand
        {
            get
            {
                if (_columnTokensCountCommand == null)
                {
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    StreamReader streamReader = new StreamReader(assembly.GetManifestResourceStream("DedupeNET.Resources.Data.SqlServer.ColumnTokensCount.sql"));
                    _columnTokensCountCommand = streamReader.ReadToEnd();
                }
                return _columnTokensCountCommand;
            }
        }
    }
}
