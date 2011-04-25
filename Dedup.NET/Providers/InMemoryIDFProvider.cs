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

        private Dictionary<ColumnToken, long> _columnTokenCount;

        private long _recordCount;
        
        public InMemoryIDFProvider(string connectionString, string relationName)
        {
            _connectionString = connectionString;
            _columnTokenCount = new Dictionary<ColumnToken, long>();
            _recordCount = IDFDataAccess.GetRecordCount();
        }

        public override double Frecuency(string token, string columnName)
        {
            long result =0;

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
