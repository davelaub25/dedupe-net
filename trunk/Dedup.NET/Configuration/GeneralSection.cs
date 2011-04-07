using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DedupeNET.Configuration
{
    public class GeneralSection : ConfigurationSection
    {
        [ConfigurationProperty("tokenSeparators", DefaultValue = " .,;", IsRequired = true)]
        public string TokenSeparators
        {
            get
            {
                return (string)this["tokenSeparators"];
            }
            set
            {
                this["tokenSeparators"] = value;
            }
        }
    }
}
