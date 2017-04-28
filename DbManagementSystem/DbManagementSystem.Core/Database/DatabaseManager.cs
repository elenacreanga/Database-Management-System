using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DbManagementSystem.Core.Database
{
    class DatabaseManager : IDatabaseManager
    {
        private readonly IDatabaseConnection databaseConnection;

        public DatabaseManager(IDatabaseConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
        }

        public Database GetDatabase(string databaseName)
        {
            var databaseLocation = this.databaseConnection.GetServerLocation() + "/" + databaseName;
            if (Directory.Exists(databaseLocation))
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
            var directory = new DirectoryInfo(databaseConnection.GetServerLocation());
            var directories = directory.GetDirectories();
            return directories.Select(d => new Database
            {
                Location = databaseConnection.GetServerLocation() + "/" + d.Name,
                Name = d.Name
            });
        }

        public Table GetDatabaseTable(string databaseName, string tableName)
        {
            var databaseLocation = this.databaseConnection.GetServerLocation() + "/" + databaseName;
            if (Directory.Exists(databaseLocation))
            {
                var tableLocation = databaseLocation + "/" + tableName;
                if (File.Exists(tableLocation))
                {
                    var columns = File.ReadLines(tableLocation).FirstOrDefault();
                    if (columns != null)
                    {
                        return new Table
                        {
                            DatabaseName = databaseName,
                            Name = tableName,
                            Size = new FileInfo(tableLocation).Length / 1024.0d,
                            Columns = columns.Split(',').ToDictionary(k => k.Split(':')[0], v => v.Split(':')[0])
                        };
                    }
                }
            }

            return null;
        }

        public IEnumerable<Table> GetDatabaseTableList(string databaseName)
        {
            var databaseLocation = this.databaseConnection.GetServerLocation() + "/" + databaseName;
            if (Directory.Exists(databaseLocation))
            {
                return Directory.EnumerateFiles(databaseLocation).Select(tableName => GetDatabaseTable(databaseName, tableName));
            }

            return null;
        }
    }
}
