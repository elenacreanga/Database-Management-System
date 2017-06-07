using System;
using System.Data;
using System.Diagnostics.Contracts;

namespace DbManagementSystem.Assertions.Database
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
            Contract.Ensures(this.databaseConfiguration != null);

            return this.databaseConfiguration;
        }

        public string GetServerLocation()
        {
            Contract.Ensures(!string.IsNullOrEmpty(this.serverLocation));

            return this.serverLocation;
        }

        public string GetDatabaseName()
        {
            return this.databaseName;
        }        
    }
}
