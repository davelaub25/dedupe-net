using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DedupeNET.Configuration
{
    public class IDFProviderCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        public IDFProvider this[int index]
        {
            get
            {
                return (IDFProvider)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public void Add(IDFProvider element)
        {
            BaseAdd(element);
        }

        public void Remove(IDFProvider element)
        {
            BaseRemove(element.Name);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new IDFProvider();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((IDFProvider)element).Name;
        }

    }
}
