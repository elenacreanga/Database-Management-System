namespace DbManagementSystem.Core.Database
{
    interface IDatabaseConnection
    {
        IDatabaseConfiguration GetDatabaseConfiguration();
        string GetServerLocation();
        string GetDatabaseName();
        string GetTableName();
    }
}
