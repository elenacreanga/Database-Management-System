using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;

namespace DbManagementSystem.Assertions.Database
{
    public class DatabaseManager : IDatabaseManager
    {
        private readonly IDatabaseConnection databaseConnection;

        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(databaseConnection != null);
        }
        public DatabaseManager(IDatabaseConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
        }

        public Database GetDatabase(string databaseName)
        {
            var databaseLocation = this.databaseConnection.GetServerLocation() + "/" + databaseName;
            if (this.databaseConnection.GetDatabaseConfiguration().DatabaseStorageService.ExistsDatabase(databaseLocation))
            {
                return new Database
                {
                    Location = databaseLocation,
                    Name = databaseName
                };
            }

            return null;
        }

        public IEnumerable<Database> GetDatabaseList()
        {
            return this.databaseConnection.GetDatabaseConfiguration().DatabaseStorageService.GetDatabases(databaseConnection.GetServerLocation())
                                    .Select(database => new Database
                                    {
                                        Location = databaseConnection.GetServerLocation() + "/" + database,
                                        Name = database
                                    });
        }

        public Table GetDatabaseTable(string databaseName, string tableName)
        {
            var databaseLocation = this.databaseConnection.GetServerLocation() + "/" + databaseName;
            if (this.databaseConnection.GetDatabaseConfiguration().DatabaseStorageService.ExistsDatabase(databaseLocation))
            {
                var tableLocation = databaseLocation + "/" + tableName;
                if (this.databaseConnection.GetDatabaseConfiguration().DatabaseStorageService.ExistsTable(tableLocation))
                {
                    var columns = this.databaseConnection.GetDatabaseConfiguration().DatabaseStorageService.ReadLines(tableLocation).FirstOrDefault();
                    if (columns != null)
                    {
                        return new Table
                        {
                            DatabaseName = databaseName,
                            Name = tableName,
                            Size = this.databaseConnection.GetDatabaseConfiguration().DatabaseStorageService.GetTableSize(tableLocation),
                            Columns = columns.Split(',').ToDictionary(k => k.Split(':')[0], v => v.Split(':')[1])
                        };
                    }
                }
            }

            return null;
        }

        

        public IEnumerable<Table> GetDatabaseTableList(string databaseName)
        {
            var databaseLocation = this.databaseConnection.GetServerLocation() + "/" + databaseName;
            if (this.databaseConnection.GetDatabaseConfiguration().DatabaseStorageService.ExistsDatabase(databaseLocation))
            {
                return this.databaseConnection.GetDatabaseConfiguration().DatabaseStorageService.EnumerateTables(databaseLocation).Select(tablePath => GetDatabaseTable(databaseName, new FileInfo(tablePath).Name));
            }

            return null;
        }
    }
}
