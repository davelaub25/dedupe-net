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
                    _dbProviderFactory = DbProviderFactories.GetFactory(DedupeNETSettings.IDFSettings.DefaultProvider.Type);
                }
                return _dbProviderFactory;
            }
        }

        public static long GetRecordCount()
        {
            using (DbConnection cn = DbProviderFactory.CreateConnection())
            {
                IDFProvider defaultProvider = DedupeNETSettings.IDFSettings.DefaultProvider;

                cn.Open();
                cn.ConnectionString = ConfigurationManager.ConnectionStrings[defaultProvider.ConnectionStringName].ConnectionString;

                DbCommand cmd = DbProviderFactory.CreateCommand();
                cmd.Connection = cn;
                cmd.CommandText = string.Format("SELECT COUNT(*) FROM {0};", defaultProvider.RelationName);

                return (long)cmd.ExecuteScalar();
            }
        }

        public static long GetTokenCount(string token, string columnName)
        {
            using (DbConnection cn = DbProviderFactory.CreateConnection())
            {
                IDFProvider defaultProvider = DedupeNETSettings.IDFSettings.DefaultProvider;

                cn.Open();
                cn.ConnectionString = ConfigurationManager.ConnectionStrings[DedupeNETSettings.IDFSettings.DefaultProvider.ConnectionStringName].ConnectionString;

                DbCommand cmd = DbProviderFactory.CreateCommand();
                cmd.Connection = cn;
                cmd.CommandText = string.Format("SELECT COUNT(*) FROM {0} WHERE {0}.{1} LIKE '%{2}%'", defaultProvider.RelationName, columnName, token);

                return (long)cmd.ExecuteScalar();
            }
        }
    }
}
