namespace DbManagementSystem.Core.Database
{
    public interface ITableImporter
    {
        bool Import(IDatabaseConnection databaseConnection, string data);
    }
}
