using System.Diagnostics.Contracts;

namespace DbManagementSystem.Assertions.Database
{
    [ContractClass(typeof(TableImportersContract))]
    public interface ITableImporter
    {
        bool Import(IDatabaseConnection databaseConnection, string tableName, string data);
    }

    [ContractClassFor(typeof(ITableImporter))]
    public class TableImportersContract:ITableImporter
    {
        public bool Import(IDatabaseConnection databaseConnection, string tableName, string data)
        {
            Contract.Requires(databaseConnection != null);
            Contract.Requires(!string.IsNullOrEmpty(tableName));
            Contract.Requires(!string.IsNullOrEmpty(data));

            return default(bool);
        }
    }
}
