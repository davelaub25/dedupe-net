using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DedupeNET.Configuration
{
    public static class DedupeNETSettings
    {
        public static GeneralSettings GeneralSettings
        {
            get { return GeneralSettings.Instance; }
        }
    }
}
