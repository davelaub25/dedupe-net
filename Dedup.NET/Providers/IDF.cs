using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.Configuration;
using System.Reflection;
using System.Configuration;
using System.Web.Security;

namespace DedupeNET.Providers
{
    public static class IDF
    {
        private static IDFProviderBase _provider;
        public static IDFProviderBase Provider
        {
            get
            {
                if (_provider == null)
                {
                    IDFProvider defaultProvider = DedupeNETSettings.IDFSettings.DefaultProvider;
                    Type type = Type.GetType(defaultProvider.Type);

                    Type[] paramTypes = new Type[2];
                    paramTypes[0] = typeof(string);
                    paramTypes[1] = typeof(string);
                    ConstructorInfo constructorInfo = type.GetConstructor(paramTypes);

                    object[] paramArray = new object[2];
                    paramArray[0] = ConfigurationManager.ConnectionStrings[defaultProvider.ConnectionStringName].ConnectionString;
                    paramArray[1] = DedupeNETSettings.IDFSettings.DefaultProvider.RelationName;

                    _provider = (IDFProviderBase)(constructorInfo.Invoke(paramArray));
                }
                return _provider;
            }
        }

        private static IEnumerable<IDFProviderBase> Providers
        {
            get
            {
                return null;
            }
        }

        public static int Frequency(string token, string columnName)
        {
            return Provider.Frequency(token, columnName);
        }

        public static double InverseDocumentFrequency(string token, string columnName)
        {
            return Provider.InverseDocumentFrequency(token, columnName);
        }
    }
}
