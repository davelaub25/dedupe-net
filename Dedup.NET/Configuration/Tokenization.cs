using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DedupeNET.Configuration
{
    public class Tokenization : ConfigurationElement
    {
        [ConfigurationProperty("stopCharacters", IsRequired = true)]
        [StringValidator(MinLength = 1)]
        public string StopCharacters
        {
            get
            {
                return (string)this["stopCharacters"];
            }
            set
            {
                this["stopCharacters"] = value;
            }
        }
    }
}
