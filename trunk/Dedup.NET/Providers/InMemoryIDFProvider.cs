using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DedupeNET.Core;
using DedupeNET.DataAccess;

namespace DedupeNET.Providers
{
    public class InMemoryIDFProvider : IDFProviderBase
    {
        private string _connectionString;

        private Dictionary<ColumnToken, int> _columnTokenCount;

        private int _recordCount;

        public InMemoryIDFProvider(string connectionString, string relationName)
        {
            _connectionString = connectionString;
            _columnTokenCount = new Dictionary<ColumnToken, int>();
            _recordCount = IDFDataAccess.GetRecordCount();
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
            throw new NotImplementedException();
        }
    }
}
