using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace DbManagementSystem.Assertions.Database
{
    [ContractClass(typeof(DatabaseManagerContract))]
    public interface IDatabaseManager
    {
        Database GetDatabase(string databaseName);
        IEnumerable<Database> GetDatabaseList();
        Table GetDatabaseTable(string databaseName, string tableName);
        IEnumerable<Table> GetDatabaseTableList(string databaseName);
    }

    [ContractClassFor(typeof(IDatabaseManager))]
    public class DatabaseManagerContract:IDatabaseManager
    {
        public Database GetDatabase(string databaseName)
        {
            Contract.Requires(!string.IsNullOrEmpty(databaseName));
            return default(Database);
        }

        public IEnumerable<Database> GetDatabaseList()
        {
            return default(IEnumerable<Database>);
        }

        public Table GetDatabaseTable(string databaseName, string tableName)
        {
            Contract.Requires(!string.IsNullOrEmpty(databaseName));
            Contract.Requires(!string.IsNullOrEmpty(tableName));
            return default(Table);
        }

        public IEnumerable<Table> GetDatabaseTableList(string databaseName)
        {
            Contract.Requires(!string.IsNullOrEmpty(databaseName));
            return default(IEnumerable<Table>);
        }
    }
}
