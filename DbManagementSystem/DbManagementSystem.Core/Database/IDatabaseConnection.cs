namespace DbManagementSystem.Core.Database
{
    interface IDatabaseConnection
    {
        string GetServerLocation();
        string GetDatabaseName();
        string GetTableName();
    }
}
