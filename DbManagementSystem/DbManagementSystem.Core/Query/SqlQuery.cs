using System;

namespace DbManagementSystem.Core.Query
{
    class SqlQuery : IQuery
    {
        public IQueryResult Execute()
        {
            throw new NotImplementedException();
        }

        public IQuery SetParameter(string name, object value)
        {
            throw new NotImplementedException();
        }
    }
}
