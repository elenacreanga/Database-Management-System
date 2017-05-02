namespace DbManagementSystem.Core.Database
{
    public interface IDatabaseConnection
    {
        IDatabaseConfiguration GetDatabaseConfiguration();
        string GetServerLocation();
        string GetDatabaseName();
    }
}
