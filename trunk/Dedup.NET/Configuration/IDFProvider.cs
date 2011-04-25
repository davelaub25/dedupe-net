using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DedupeNET.Configuration
{
    public class IDFProvider : ConfigurationElement
    {
        public IDFProvider()
        {
        }
        
        public IDFProvider(string name, string type)
        {
            this.Name = name;
            this.Type = type;
        }
        
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get
            {
                return (string)this["type"];
            }
            set
            {
                this["type"] = value;
            }
        }

        [ConfigurationProperty("connectionStringName", IsRequired = true)]
        public string ConnectionStringName
        {
            get
            {
                return (string)this["connectionStringName"];
            }
            set
            {
                this["connectionStringName"] = value;
            }
        }

        [ConfigurationProperty("relationName", IsRequired = true)]
        public string RelationName
        {
            get
            {
                return (string)this["relationName"];
            }
            set
            {
                this["relationName"] = value;
            }
        }

        [ConfigurationProperty("dataProvider", IsRequired = true)]
        public string DataProvider
        {
            get
            {
                return (string)this["dataProvider"];
            }
            set
            {
                this["dataProvider"] = value;
            }
        }
    }
}
