namespace DbManagementSystem.Core.Database
{
    interface IDatabaseConnection
    {
        string GetDatabaseLocation();
        string GetTableName();
    }
}
