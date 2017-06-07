using System.Diagnostics.Contracts;
using DbManagementSystem.Assertions.Database;

namespace DbManagementSystem.Assertions.Query
{
    [ContractClass(typeof(QueryExecutorContract))]
    public interface IQueryExecutor
    {
        IQueryResult Execute(IDatabaseConnection databaseConnection, IQuery query);
        bool MatchesQuery(IQuery query);
    }


    [ContractClassFor(typeof(IQueryExecutor))]
    public class QueryExecutorContract:IQueryExecutor
    {
        public IQueryResult Execute(IDatabaseConnection databaseConnection, IQuery query)
        {
            Contract.Ensures(databaseConnection != null);
            Contract.Ensures(query != null);
            return default(IQueryResult);
        }

        public bool MatchesQuery(IQuery query)
        {
            Contract.Ensures(query != null);
            return default(bool);
        }
    }
}
