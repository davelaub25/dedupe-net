using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using DedupeNET.Providers;
using DedupeNET.Configuration;
using System.Configuration;

namespace DedupeNET.DataAccess
{
    public static class IDFDataAccess
    {
        private static DbProviderFactory _dbProviderFactory;
        private static DbProviderFactory DbProviderFactory
        {
            get
            {
                if (_dbProviderFactory == null)
                {
                    _dbProviderFactory = DbProviderFactories.GetFactory(DedupeNETSettings.IDFSettings.DefaultProvider.DataProvider);
                }
                return _dbProviderFactory;
            }
        }

        public static int GetRecordCount()
        {
            using (DbConnection cn = DbProviderFactory.CreateConnection())
            {
                IDFProvider defaultProvider = DedupeNETSettings.IDFSettings.DefaultProvider;

                cn.ConnectionString = ConfigurationManager.ConnectionStrings[defaultProvider.ConnectionStringName].ConnectionString;
                cn.Open();

                DbCommand cmd = DbProviderFactory.CreateCommand();
                cmd.Connection = cn;
                cmd.CommandText = string.Format("SELECT COUNT(*) FROM {0}", defaultProvider.RelationName);

                return (int)cmd.ExecuteScalar();
            }
        }

        public static int GetTokenCount(string token, string columnName)
        {
            using (DbConnection cn = DbProviderFactory.CreateConnection())
            {
                IDFProvider defaultProvider = DedupeNETSettings.IDFSettings.DefaultProvider;

                cn.ConnectionString = ConfigurationManager.ConnectionStrings[DedupeNETSettings.IDFSettings.DefaultProvider.ConnectionStringName].ConnectionString;
                cn.Open();

                DbCommand cmd = DbProviderFactory.CreateCommand();
                cmd.Connection = cn;

                cmd.CommandText = string.Format("SELECT COUNT(*) FROM {0} WHERE (SELECT COUNT(*) FROM dbo.TokenizeString({1}, '{2}') WHERE Token = '{3}') > 0",
                    defaultProvider.RelationName, defaultProvider.RelationName + columnName, DedupeNETSettings.GeneralSettings.Tokenization.StopCharacters, token);

                return (int)cmd.ExecuteScalar();
            }
        }
    }
}
