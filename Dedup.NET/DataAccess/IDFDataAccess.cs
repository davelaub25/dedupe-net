using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using DedupeNET.Providers;
using DedupeNET.Configuration;
using System.Configuration;
using System.Reflection;
using System.IO;
using DedupeNET.Core;
using DedupeNET.Resources;
using DedupeNET.Resources.Data;

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

                cmd.CommandText = string.Format(DedupeNETResources.GetStringResource(DataResources.RecordCountCommand),
                    defaultProvider.RelationName);

                return (int)cmd.ExecuteScalar();
            }
        }

        public static Dictionary<string, int> ColumnTokensCount(string columnName)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            using (DbConnection cn = DbProviderFactory.CreateConnection())
            {
                IDFProvider defaultProvider = DedupeNETSettings.IDFSettings.DefaultProvider;

                cn.ConnectionString = ConfigurationManager.ConnectionStrings[defaultProvider.ConnectionStringName].ConnectionString;
                cn.Open();

                DbCommand cmd = DbProviderFactory.CreateCommand();
                cmd.Connection = cn;

                cmd.CommandText = string.Format(DedupeNETResources.GetStringResource(DataResources.ColumnTokensCountCommand),
                        defaultProvider.RelationName,
                        columnName,
                        DedupeNETSettings.GeneralSettings.Tokenization.StopCharacters);

                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(reader["Token"].ToString(), (int)reader["Count"]);
                    }
                }
            }

            return result;
        }
    }
}
