using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace DedupeNET.Resources.Data.Common
{
    public class CommonDataResources
    {
        private static CommonDataResources _instance = new CommonDataResources();
        public static CommonDataResources Instance
        {
            get { return _instance; }
        }

        private CommonDataResources() { }

        private string _recordCountCommand;
        public string RecordCountCommand
        {
            get
            {
                if (_recordCountCommand == null)
                {
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    StreamReader streamReader = new StreamReader(assembly.GetManifestResourceStream("DedupeNET.Resources.Data.Common.RecordCount.sql"));
                    _recordCountCommand = streamReader.ReadToEnd();
                }
                return _recordCountCommand;
            }
        }
    }
}
