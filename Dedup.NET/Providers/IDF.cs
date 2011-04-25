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

                    Type[] paramTypes = new Type[1];
                    paramTypes[0] = typeof(string);
                    ConstructorInfo constructorInfo = type.GetConstructor(paramTypes);

                    object[] paramArray = new object[1];
                    paramArray[0] = ConfigurationManager.ConnectionStrings[defaultProvider.ConnectionStringName].ConnectionString;

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

        public static double Frequency(string token, int columnIndex)
        {
            return 0;
        }

        public static double Frecuency(string token, string columnName)
        {
            return 0;
        }

        public static double InverseDocumentFrequency(string token, int columnIndex)
        {
            return 0;
        }

        public static double InverseDocumentFrequency(string token, string columnName)
        {
            return 0;
        }
    }
}
