using System.Diagnostics.Contracts;

namespace DbManagementSystem.Assertions.Query
{
    [ContractClass(typeof(QueryContract))]
    public interface IQuery
    {
        string GetQuery();
        IQuery SetParameter(string name, object value);
    }

    [ContractClassFor(typeof(IQuery))]
    public class QueryContract: IQuery
    {
        public string GetQuery()
        {
            return default(string);
        }

        public IQuery SetParameter(string name, object value)
        {
            Contract.Requires(!string.IsNullOrEmpty(name));
            return default(IQuery);
        }
    }
}
