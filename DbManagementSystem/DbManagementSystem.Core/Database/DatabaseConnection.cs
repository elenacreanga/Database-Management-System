using System;

namespace DbManagementSystem.Core.Database
{
    class DatabaseConnection : IDatabaseConnection
    {
        private readonly string serverLocation;
        private readonly string databaseName;
        private readonly string tableName;

        public DatabaseConnection(string serverLocation, string databaseName = null, string tableName = null)
        {
            this.serverLocation = serverLocation;
            this.databaseName = databaseName;
            this.tableName = tableName;
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
