using System.Linq;
using DbManagementSystem.Core.Database;
using NSubstitute;
using Xunit;

namespace DbManagementSystem.Test.Database
{
    public class DatabaseManagerTest
    {
        [Fact]
        public void GetDatabase_WhenNoDatabase_ShouldReturnNull()
        {
            var databaseConnection = Substitute.For<IDatabaseConnection>();
            var databaseStorage = Substitute.For<IDatabaseStorageService>();
            var databaseConfiguration = Substitute.For<IDatabaseConfiguration>();

            databaseConnection.GetDatabaseConfiguration().Returns(databaseConfiguration);
            databaseConfiguration.DatabaseStorageService.Returns(databaseStorage);

            var databaseManager = GetSUT(databaseConnection);
            var result = databaseManager.GetDatabase("nonExistingDb");
            Assert.Null(result);
        }

        [Fact]
        public void GetDatabase_WhenExistingDatabase_ShouldReturnDatabase()
        {
            var databaseConnection = Substitute.For<IDatabaseConnection>();
            var databaseStorage = Substitute.For<IDatabaseStorageService>();
            var databaseConfiguration = Substitute.For<IDatabaseConfiguration>();

            databaseStorage.ExistsDatabase(Arg.Any<string>()).Returns(true);
            databaseConnection.GetDatabaseConfiguration().Returns(databaseConfiguration);
            databaseConfiguration.DatabaseStorageService.Returns(databaseStorage);

            var databaseManager = GetSUT(databaseConnection);
            const string databaseName = "existingDb";
            var result = databaseManager.GetDatabase(databaseName);
            Assert.NotNull(result);
            Assert.Equal(databaseName, result.Name);
        }

        [Fact]
        public void GetDatabaseList_WhenNoDatabases_ShouldReturnNull()
        {
            var databaseConnection = Substitute.For<IDatabaseConnection>();
            var databaseStorage = Substitute.For<IDatabaseStorageService>();
            var databaseConfiguration = Substitute.For<IDatabaseConfiguration>();

            databaseConnection.GetDatabaseConfiguration().Returns(databaseConfiguration);
            databaseConfiguration.DatabaseStorageService.Returns(databaseStorage);

            var databaseManager = GetSUT(databaseConnection);
            var result = databaseManager.GetDatabaseList();
            Assert.Empty(result);
        }

        [Fact]
        public void GetDatabaseList_WhenExistingDatabase_ShouldReturnDatabase()
        {
            var databaseConnection = Substitute.For<IDatabaseConnection>();
            var databaseStorage = Substitute.For<IDatabaseStorageService>();
            var databaseConfiguration = Substitute.For<IDatabaseConfiguration>();

            databaseStorage.ExistsDatabase(Arg.Any<string>()).Returns(true);
            databaseConnection.GetDatabaseConfiguration().Returns(databaseConfiguration);
            databaseConfiguration.DatabaseStorageService.Returns(databaseStorage);
            const string databaseone = "databaseOne";
            const string databasetwo = "databaseTwo";
            databaseStorage.GetDatabases(Arg.Any<string>()).Returns(new[] {databaseone, databasetwo});

            var databaseManager = GetSUT(databaseConnection);
            var result = databaseManager.GetDatabaseList();
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetDatabaseTable_WhenNoTable_ShouldReturnNull()
        {
             var databaseConnection = Substitute.For<IDatabaseConnection>();
            var databaseStorage = Substitute.For<IDatabaseStorageService>();
            var databaseConfiguration = Substitute.For<IDatabaseConfiguration>();

            databaseStorage.ExistsDatabase(Arg.Any<string>()).Returns(true);
            databaseConnection.GetDatabaseConfiguration().Returns(databaseConfiguration);
            databaseConfiguration.DatabaseStorageService.Returns(databaseStorage);

            var databaseManager = GetSUT(databaseConnection);
            const string tableName = "tableName";
            const string databaseName = "existingDb";
            var result = databaseManager.GetDatabaseTable(databaseName, tableName);
            Assert.Null(result);
        }

        [Fact]
        public void GetDatabaseTable_WhenExistingTable_ShouldReturnTable()
        {
            var databaseConnection = Substitute.For<IDatabaseConnection>();
            var databaseStorage = Substitute.For<IDatabaseStorageService>();
            var databaseConfiguration = Substitute.For<IDatabaseConfiguration>();

            databaseStorage.ExistsDatabase(Arg.Any<string>()).Returns(true);
            databaseStorage.ExistsTable(Arg.Any<string>()).Returns(true);
            databaseStorage.ReadLines(Arg.Any<string>()).Returns(new [] {"RandomLine1:Text2","RandomLine2:Text24"});
            databaseStorage.GetTableSize(Arg.Any<string>()).Returns(124,3);
            databaseConnection.GetDatabaseConfiguration().Returns(databaseConfiguration);
            databaseConfiguration.DatabaseStorageService.Returns(databaseStorage);

            var databaseManager = GetSUT(databaseConnection);
            const string tableName = "tableName";
            const string databaseName = "existingDb";
            var result = databaseManager.GetDatabaseTable(databaseName, tableName);
            Assert.NotNull(result);
            Assert.Equal(tableName, result.Name);
        }

        [Fact]
        public void GetDatabaseTableList_WhenNoTables_ShouldReturnEmptyList()
        {
             var databaseConnection = Substitute.For<IDatabaseConnection>();
            var databaseStorage = Substitute.For<IDatabaseStorageService>();
            var databaseConfiguration = Substitute.For<IDatabaseConfiguration>();

            databaseStorage.ExistsDatabase(Arg.Any<string>()).Returns(true);
            databaseConnection.GetDatabaseConfiguration().Returns(databaseConfiguration);
            databaseConfiguration.DatabaseStorageService.Returns(databaseStorage);

            var databaseManager = GetSUT(databaseConnection);
            const string databaseName = "existingDb";
            var result = databaseManager.GetDatabaseTableList(databaseName);
            Assert.Empty(result);
        }

        [Fact]
        public void GetDatabaseTableList_WhenExistingTables_ShouldReturnTables()
        {
            var databaseConnection = Substitute.For<IDatabaseConnection>();
            var databaseStorage = Substitute.For<IDatabaseStorageService>();
            var databaseConfiguration = Substitute.For<IDatabaseConfiguration>();
            const string tableNameOne = "tableNameOne";
            const string tableNameTwo = "tableNameTwo";

            databaseStorage.ExistsDatabase(Arg.Any<string>()).Returns(true);
            databaseStorage.ExistsTable(Arg.Any<string>()).Returns(true);
            databaseStorage.ReadLines(Arg.Any<string>()).Returns(new [] {"RandomLine1:Text2","RandomLine2:Text24"});
            databaseStorage.GetTableSize(Arg.Any<string>()).Returns(124,3);
            databaseStorage.EnumerateTables(Arg.Any<string>()).Returns(new[] {tableNameOne, tableNameTwo});
            databaseConnection.GetDatabaseConfiguration().Returns(databaseConfiguration);
            databaseConfiguration.DatabaseStorageService.Returns(databaseStorage);

            var databaseManager = GetSUT(databaseConnection);
            const string databaseName = "existingDb";
            var result = databaseManager.GetDatabaseTableList(databaseName);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

        private DatabaseManager GetSUT(IDatabaseConnection databaseConnection)
        {
            return new DatabaseManager(databaseConnection);
        }

    }
}
