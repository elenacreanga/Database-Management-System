using System;

namespace DbManagementSystem.Core.Database
{
    public class DatabaseConnection : IDatabaseConnection
    {
        private readonly IDatabaseConfiguration databaseConfiguration;
        private readonly string serverLocation;
        private readonly string databaseName;

        public DatabaseConnection(IDatabaseConfiguration databaseConfiguration, string serverLocation, string databaseName = null)
        {
            this.databaseConfiguration = databaseConfiguration;
            this.serverLocation = serverLocation ?? string.Empty;
            this.databaseName = databaseName ?? string.Empty;            
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
    }
}
