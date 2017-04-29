using System.Collections.Generic;

namespace DbManagementSystem.Core.Database
{
    public interface IDatabaseManager
    {
        Database GetDatabase(string databaseName);
        IEnumerable<Database> GetDatabaseList();
        Table GetDatabaseTable(string databaseName, string tableName);
        IEnumerable<Table> GetDatabaseTableList(string databaseName);
    }
}
