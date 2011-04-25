using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DedupeNET.Core
{
    public class ColumnToken
    {
        private string _token;
        public string Token
        {
            get { return _token; }
            set { _token = value; }
        }

        private string _columnName;
        public string ColumnName
        {
            get { return _columnName; }
            set { _columnName = value; }
        }

        public ColumnToken(string token, string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
            {
                throw new ArgumentNullException("El parámetro columnName no puede ser nulo.");
            }

            _token = token;
            _columnName = columnName;
        }
    }
}
