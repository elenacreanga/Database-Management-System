using System.Diagnostics.Contracts;
using DbManagementSystem.Assertions.Database;

namespace DbManagementSystem.Assertions.Query
{
    [ContractClass(typeof(QueryResultSerializerContract))]
    public interface IQueryResultSerializer
    {
        string Serialize(IQueryResult queryResult);
    }

    [ContractClassFor(typeof(IQueryResultSerializer))]
    public class QueryResultSerializerContract:IQueryResultSerializer
    {
        public string Serialize(IQueryResult queryResult)
        {
            Contract.Requires(queryResult != null);
            return default(string);
        }
    }
}
