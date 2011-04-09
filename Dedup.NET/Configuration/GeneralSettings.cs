using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DedupeNET.Configuration
{
    public class GeneralSettings : ConfigurationSection
    {
        private static GeneralSettings _settings =
            ConfigurationManager.GetSection("GeneralSettings") as GeneralSettings;

        private GeneralSettings() { }

        public static GeneralSettings Settings
        {
            get { return _settings; }
        }

        [ConfigurationProperty("tokenSeparators", DefaultValue = "", IsRequired = true)]
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
