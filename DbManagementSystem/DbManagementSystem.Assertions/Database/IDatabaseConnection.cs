namespace DbManagementSystem.Assertions.Database
{
    public interface IDatabaseConnection
    {
        IDatabaseConfiguration GetDatabaseConfiguration();
        string GetServerLocation();
        string GetDatabaseName();
    }

}
