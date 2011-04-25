using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DedupeNET.Configuration
{
    public class IDFSettings : ConfigurationSection
    {
        private static IDFSettings _instance =
           ConfigurationManager.GetSection("DedupeNET/IDFSettings") as IDFSettings;

        private IDFSettings() { }

        public static IDFSettings Instance
        {
            get { return _instance; }
        }

        [ConfigurationProperty("providers", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(IDFProviderCollection), AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove")]
        public IDFProviderCollection IDFProviders
        {
            get
            {
                return (IDFProviderCollection)this["providers"];
            }
            set
            {
                this["providers"] = value;
            }
        }

        public IDFProvider DefaultProvider
        {
            get
            {
                return IDFProviders[0];
            }
        }
    }
}
