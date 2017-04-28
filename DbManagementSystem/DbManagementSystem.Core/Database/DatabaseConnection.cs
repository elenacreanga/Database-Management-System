using System;

namespace DbManagementSystem.Core.Database
{
    class DatabaseConnection : IDatabaseConnection
    {
        private readonly IDatabaseConfiguration databaseConfiguration;
        private readonly string serverLocation;
        private readonly string databaseName;
        private readonly string tableName;

        public DatabaseConnection(IDatabaseConfiguration databaseConfiguration, string serverLocation, string databaseName = null, string tableName = null)
        {
            this.databaseConfiguration = databaseConfiguration;
            this.serverLocation = serverLocation;
            this.databaseName = databaseName;
            this.tableName = tableName;
        }

        public IDatabaseConfiguration GetDatabaseConfiguration()
        {
            return this.databaseConfiguration;
        }

        public string GetServerLocation()
        {
            return this.serverLocation;
        }

        public string GetDatabaseName()
        {
            return this.databaseName;
        }

        public string GetTableName()
        {
            return this.tableName;
        }
    }
}
