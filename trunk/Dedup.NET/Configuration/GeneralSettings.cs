using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DedupeNET.Configuration
{
    public class GeneralSettings : ConfigurationSection
    {
        private static GeneralSettings _instance =
            ConfigurationManager.GetSection("DedupeNET/GeneralSettings") as GeneralSettings;

        private GeneralSettings() { }

        public static GeneralSettings Instance
        {
            get { return _instance; }
        }

        [ConfigurationProperty("tokenization", IsRequired = true)]
        public Tokenization Tokenization
        {
            get
            {
                return (Tokenization)this["tokenization"];
            }
        }
    }
}
