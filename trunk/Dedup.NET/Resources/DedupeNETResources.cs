using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.Resources.Data;

namespace DedupeNET.Resources
{
    public static partial class DedupeNETResources
    {
        public static DataResources Data
        {
            get { return DataResources.Instance; }
        }
    }
}
