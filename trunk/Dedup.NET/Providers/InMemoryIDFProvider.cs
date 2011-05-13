using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.Core;
using DedupeNET.DataAccess;
using System.Data.Common;
using DedupeNET.Configuration;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace DedupeNET.Providers
{
    public class InMemoryIDFProvider : IDFProviderBase
    {
        private string _connectionString;

        private Dictionary<ColumnToken, int> _columnTokenCount;

        private int _recordCount;

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

        private string _columnTokenSetCommand;
        public string ColumnTokenSetCommand
        {
            get
            {
                if (_columnTokenSetCommand == null)
                {
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    StreamReader streamReader = new StreamReader(assembly.GetManifestResourceStream("DedupeNET.Resources.Data.ColumnTokensCount.sql"));
                    _columnTokenSetCommand = streamReader.ReadToEnd();
                }
                return _columnTokenSetCommand;
            }
        }

        public InMemoryIDFProvider(string connectionString, string relationName)
        {
            _connectionString = connectionString;
            _columnTokenCount = new Dictionary<ColumnToken, int>();
            _recordCount = IDFDataAccess.GetRecordCount();
        }

        public InMemoryIDFProvider()
        {
            // TODO: Complete member initialization
        }

        public override int Frecuency(string token, string columnName)
        {
            int result = 0;

            if (_columnTokenCount.TryGetValue(new ColumnToken(token, columnName), out result) == false)
            {
                _columnTokenCount[new ColumnToken(token, columnName)] = IDFDataAccess.GetTokenCount(token, columnName);
            }

            return _columnTokenCount[new ColumnToken(token, columnName)];
        }

        public override double InverseDocumentFrequency(string token, string columnName)
        {
            int tokenFrequency = Frecuency(token, columnName);

            if (tokenFrequency == 0)
            {
                return 0;
            }
            else
            {
                return Math.Log10(IDFDataAccess.GetRecordCount() / tokenFrequency);
            }
        }

        private double AverageIDF(string columnName)
        {
            //IEnumerable<string> columnTokenSet = ColumnTokenSet(columnName);
            return 0;
        }

        private IEnumerable<string> ColumnTokenSet(string columnName)
        {
            List<string> tokenSet = new List<string>();

            using (DbConnection cn = DbProviderFactory.CreateConnection())
            {
                IDFProvider defaultProvider = DedupeNETSettings.IDFSettings.DefaultProvider;

                cn.ConnectionString = ConfigurationManager.ConnectionStrings[defaultProvider.ConnectionStringName].ConnectionString;
                cn.Open();

                DbCommand cmd = DbProviderFactory.CreateCommand();
                cmd.Connection = cn;
                cmd.CommandText = string.Format("SELECT dbo.Tokenize({0}.{1}, '{2}') as Token from {0}", defaultProvider.RelationName, columnName, DedupeNETSettings.GeneralSettings.Tokenization.StopCharacters);

                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string tokenString = reader["Token"].ToString();
                        tokenSet.AddRange(tokenString.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).AsEnumerable());
                    }
                }
            }

            return tokenSet.Distinct().ToList();
        }
    }
}
