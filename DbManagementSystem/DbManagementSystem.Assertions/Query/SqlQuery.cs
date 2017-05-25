using System.Diagnostics.Contracts;

namespace DbManagementSystem.Assertions.Query
{
    public class SqlQuery : IQuery
    {
        private string query;

        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(query != null);
        }
        public SqlQuery(string query)
        {
            this.query = query ?? string.Empty;
        }

        public string GetQuery()
        {
            return this.query.Trim();
        }

        public IQuery SetParameter(string name, object value)
        {
            this.query = this.query.Replace(":" + name, (value ?? string.Empty).ToString());
            return this;
        }
    }
}
