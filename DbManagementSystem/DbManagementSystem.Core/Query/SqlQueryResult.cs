using System;

namespace DbManagementSystem.Core.Query
{
    class SqlQueryResult : IQueryResult
    {
        public int RowsAffected => throw new NotImplementedException();

        public bool Success => throw new NotImplementedException();

        public string Message => throw new NotImplementedException();

        public object GetValue(string name)
        {
            throw new NotImplementedException();
        }

        public object GetValue(int index)
        {
            throw new NotImplementedException();
        }

        public bool Read()
        {
            throw new NotImplementedException();
        }
    }
}
