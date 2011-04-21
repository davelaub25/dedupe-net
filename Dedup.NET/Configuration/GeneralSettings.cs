using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DedupeNET.Configuration
{
    internal class GeneralSettings : ConfigurationSection
    {
        private static GeneralSettings _instance =
            ConfigurationManager.GetSection("DedupeNET/GeneralSettings") as GeneralSettings;

        private GeneralSettings() { }

        public static GeneralSettings Instance
        {
            get { return _instance; }
        }

        [ConfigurationProperty("stopCharacters", DefaultValue = "", IsRequired = true)]
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
