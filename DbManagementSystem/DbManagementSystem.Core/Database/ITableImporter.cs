namespace DbManagementSystem.Core.Database
{
    interface ITableImporter
    {
        bool Import(IDatabaseConnection databaseConnection, string data);
    }
}
