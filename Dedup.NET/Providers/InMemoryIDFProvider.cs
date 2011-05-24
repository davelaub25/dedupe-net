using System;
using System.Collections.Generic;
using System.Data.Common;
using DedupeNET.Configuration;
using DedupeNET.Core;
using DedupeNET.DataAccess;
using System.Linq;

namespace DedupeNET.Providers
{
    public class InMemoryIDFProvider : IDFProviderBase
    {
        #region PROPERTIES/VARIABLES

        private string _connectionString;
        private int _recordCount;

        private Dictionary<string, Dictionary<string, int>> _columnTokensCount;

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

        #endregion

        public InMemoryIDFProvider(string connectionString, string relationName)
        {
            _connectionString = connectionString;
            _columnTokensCount = new Dictionary<string, Dictionary<string, int>>();
            _recordCount = IDFDataAccess.GetRecordCount();
        }

        public InMemoryIDFProvider()
        {
            // TODO: Complete member initialization
        }

        public override int Frecuency(string token, string columnName)
        {
            Dictionary<string, int> _tokensCount = null;

            if (_columnTokensCount.TryGetValue(columnName, out _tokensCount) == false)
            {
                _tokensCount = IDFDataAccess.ColumnTokensCount(columnName);
                _columnTokensCount.Add(columnName, _tokensCount);
            }

            return _columnTokensCount[columnName][token];
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

            return 0;
        }


    }
}
