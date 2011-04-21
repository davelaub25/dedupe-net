using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DedupeNET.Configuration
{
    public class IDFProviderCollection//: ConfigurationElementCollection
    {
        /*public class ArticleCollection : ConfigurationElementCollection
        {
            public override ConfigurationElementCollectionType CollectionType
            {
                get
                {
                    return ConfigurationElementCollectionType.AddRemoveClearMap;
                }
            }

            public ArticleElement this[int index]
            {
                get { return (ArticleElement)BaseGet(index); }
                set
                {
                    if (BaseGet(index) != null)
                        BaseRemoveAt(index);
                    BaseAdd(index, value);
                }
            }

            public void Add(ArticleElement element)
            {
                BaseAdd(element);
            }

            public void Clear()
            {
                BaseClear();
            }

            protected override ConfigurationElement CreateNewElement()
            {
                return new ArticleElement();
            }

            protected override object GetElementKey(ConfigurationElement element)
            {
                return ((ArticleElement)element).Title;
            }

            public void Remove(ArticleElement element)
            {
                BaseRemove(element.Title);
            }

            public void Remove(string name)
            {
                BaseRemove(name);
            }

            public void RemoveAt(int index)
            {
                BaseRemoveAt(index);
            }
        }*/
    }
}
