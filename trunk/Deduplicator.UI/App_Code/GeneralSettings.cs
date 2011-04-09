using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Aspnet.Configuration
{
    public class GeneralSettingsa : ConfigurationSection
    {
        private static GeneralSettingsa settings =
            ConfigurationManager.GetSection("GeneralSettings") as GeneralSettingsa;

        public static GeneralSettingsa Settings
        {
            get
            {
                return settings;
            } 
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
