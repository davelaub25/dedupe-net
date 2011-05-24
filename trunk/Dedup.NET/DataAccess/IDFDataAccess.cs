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

        private static string _columnTokensCountCommand;
        private static string ColumnTokensCountCommand
        {
            get
            {
                if (_columnTokensCountCommand == null)
                {
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    StreamReader streamReader = new StreamReader(assembly.GetManifestResourceStream("DedupeNET.Resources.Data.SQLServer.ColumnTokensCount.sql"));
                    _columnTokensCountCommand = streamReader.ReadToEnd();
                }
                return _columnTokensCountCommand;
            }
            set
            {
                _columnTokensCountCommand = value;
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

                ColumnTokensCountCommand = ColumnTokensCountCommand.Replace("###relationName###", defaultProvider.RelationName);
                ColumnTokensCountCommand = ColumnTokensCountCommand.Replace("###columnName###", columnName);

                cmd.CommandText = ColumnTokensCountCommand;

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
