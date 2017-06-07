using System;
using System.Collections.Generic;
using DbManagementSystem.Core.Database;
using DbManagementSystem.Core.Query;
using DbManagementSystem.Core.Query.Executors;
using NSubstitute;
using Xunit;

namespace DbManagementSystem.Test.Query.Executors
{
    public class QueryExecutorTest
    {
        [Fact]
        public void Execute_WithInvalidDatabaseConnection_ShouldFail()
        {
            var queryExecutor = GetSUT();
            var databaseConnection = Substitute.For<IDatabaseConnection>();
            var databaseStorage = Substitute.For<IDatabaseStorageService>();
            var databaseConfiguration = Substitute.For<IDatabaseConfiguration>();

            databaseStorage.ExistsDatabase(Arg.Any<string>()).Returns(false);
            databaseConnection.GetServerLocation().Returns("not-empty-string");
            databaseConnection.GetDatabaseConfiguration().Returns(databaseConfiguration);
            databaseConfiguration.DatabaseStorageService.Returns(databaseStorage);
            IQuery query = new SqlQuery("Insert into");
            var result = queryExecutor.Execute(databaseConnection, query);
            Assert.False(result.Success);
            Assert.Equal("Invalid database connection!", result.Message);
        }

        [Fact]
        public void Execute_WithInvalidQuery_ShouldFail()
        {
            var queryExecutor = GetSUT();
            var databaseConnection = Substitute.For<IDatabaseConnection>();
            var databaseStorage = Substitute.For<IDatabaseStorageService>();
            var databaseConfiguration = Substitute.For<IDatabaseConfiguration>();

            databaseStorage.ExistsDatabase(Arg.Any<string>()).Returns(true);
            databaseConnection.GetServerLocation().Returns("not-empty-string");
            databaseConnection.GetDatabaseConfiguration().Returns(databaseConfiguration);
            databaseConfiguration.DatabaseStorageService.Returns(databaseStorage);
            IQuery query = new SqlQuery("Insert into");
            var result = queryExecutor.Execute(databaseConnection, query);
            Assert.False(result.Success);
            Assert.Equal("Invalid query!", result.Message);
        }

        [Fact]
        public void Execute_WithValidCreateDbQuery_ShouldSucceed()
        {
            var queryExecutor = GetSUT();
            var databaseConnection = Substitute.For<IDatabaseConnection>();
            var databaseStorage = Substitute.For<IDatabaseStorageService>();
            var databaseConfiguration = Substitute.For<IDatabaseConfiguration>();

            databaseStorage.ExistsDatabase(Arg.Any<string>()).Returns(true);
            const string serverLocation = "server-location";
            const string databaseName = "test";
            databaseStorage.ExistsDatabase(serverLocation + "/" + databaseName).Returns(false);
            databaseConnection.GetServerLocation().Returns(serverLocation);
            databaseConnection.GetDatabaseConfiguration().Returns(databaseConfiguration);
            databaseConfiguration.DatabaseStorageService.Returns(databaseStorage);
            IQuery query = new SqlQuery("CREATE DATABASE " + databaseName);
            var result = queryExecutor.Execute(databaseConnection, query);
            Assert.True(result.Success);
            Assert.Equal("Database successfully created", result.Message);
        }

        [Fact]
        public void Execute_WithValidSelectQuery_ShouldSucceed()
        {
            var queryExecutor = GetSUT();
            var databaseConnection = Substitute.For<IDatabaseConnection>();
            var databaseStorage = Substitute.For<IDatabaseStorageService>();
            var databaseConfiguration = Substitute.For<IDatabaseConfiguration>();

            databaseStorage.ExistsDatabase(Arg.Any<string>()).Returns(true);
            const string serverLocation = "server-location";
            const string databaseName = "test";
            const string tableName = "test";

            databaseStorage.ExistsDatabase(serverLocation + "/" + databaseName).Returns(true);
            databaseConnection.GetServerLocation().Returns(serverLocation);
            databaseConnection.GetDatabaseConfiguration().Returns(databaseConfiguration);
            databaseConfiguration.DatabaseStorageService.Returns(databaseStorage);
            databaseConnection.GetDatabaseName().Returns(databaseName);
            databaseStorage.ExistsTable(Arg.Any<string>()).Returns(true);
            databaseStorage.ReadAllLines(Arg.Any<string>()).Returns(new []{"nume:string/r/nIon/r/nVasile"});
            IQuery query = new SqlQuery("SELECT * FROM " + tableName);
            var result = queryExecutor.Execute(databaseConnection, query);
            Assert.True(result.Success);
            Assert.Equal("Query successfully executed", result.Message);
        }

        [Fact]
        public void Execute_WithInvalidSelectQuery_ShouldFail()
        {
            var queryExecutor = GetSUT();
            var databaseConnection = Substitute.For<IDatabaseConnection>();
            var databaseStorage = Substitute.For<IDatabaseStorageService>();
            var databaseConfiguration = Substitute.For<IDatabaseConfiguration>();

            databaseStorage.ExistsDatabase(Arg.Any<string>()).Returns(true);
            const string serverLocation = "server-location";
            const string databaseName = "test";
            const string tableName = "test";

            databaseStorage.ExistsDatabase(serverLocation + "/" + databaseName).Returns(true);
            databaseConnection.GetServerLocation().Returns(serverLocation);
            databaseConnection.GetDatabaseConfiguration().Returns(databaseConfiguration);
            databaseConfiguration.DatabaseStorageService.Returns(databaseStorage);
            databaseConnection.GetDatabaseName().Returns(databaseName);
            databaseStorage.ExistsTable(Arg.Any<string>()).Returns(true);
            databaseStorage.ReadAllLines(Arg.Any<string>()).Returns(new[] { "nume:string/r/nIon/r/nVasile" });
            IQuery query = new SqlQuery("SELECT** * FROM " + tableName);
            var result = queryExecutor.Execute(databaseConnection, query);
            Assert.False(result.Success);
            Assert.Equal("Invalid query!", result.Message);
        }

        [Fact]
        public void Execute_WithValidDeleteQuery_ShouldSucceed()
        {
            var queryExecutor = GetSUT();
            var databaseConnection = Substitute.For<IDatabaseConnection>();
            var databaseStorage = Substitute.For<IDatabaseStorageService>();
            var databaseConfiguration = Substitute.For<IDatabaseConfiguration>();

            databaseStorage.ExistsDatabase(Arg.Any<string>()).Returns(true);
            const string serverLocation = "server-location";
            const string databaseName = "test";
            const string tableName = "test";

            databaseStorage.ExistsDatabase(serverLocation + "/" + databaseName).Returns(true);
            databaseConnection.GetServerLocation().Returns(serverLocation);
            databaseConnection.GetDatabaseConfiguration().Returns(databaseConfiguration);
            databaseConfiguration.DatabaseStorageService.Returns(databaseStorage);
            databaseConnection.GetDatabaseName().Returns(databaseName);
            databaseStorage.ExistsTable(Arg.Any<string>()).Returns(true);
            databaseStorage.ReadAllLines(Arg.Any<string>()).Returns(new[] { "nume:string","Ion" });
            IQuery query = new SqlQuery("DELETE FROM " + tableName + " WHERE nume='Ion'");
            var result = queryExecutor.Execute(databaseConnection, query);
            Assert.True(result.Success);
            Assert.Equal("Successfully deleted 1 records.", result.Message);
        }

        [Fact]
        public void Execute_WithInvalidDeleteQuery_ShouldFail()
        {
            var queryExecutor = GetSUT();
            var databaseConnection = Substitute.For<IDatabaseConnection>();
            var databaseStorage = Substitute.For<IDatabaseStorageService>();
            var databaseConfiguration = Substitute.For<IDatabaseConfiguration>();

            databaseStorage.ExistsDatabase(Arg.Any<string>()).Returns(true);
            const string serverLocation = "server-location";
            const string databaseName = "test";
            const string tableName = "test";

            databaseStorage.ExistsDatabase(serverLocation + "/" + databaseName).Returns(true);
            databaseConnection.GetServerLocation().Returns(serverLocation);
            databaseConnection.GetDatabaseConfiguration().Returns(databaseConfiguration);
            databaseConfiguration.DatabaseStorageService.Returns(databaseStorage);
            databaseConnection.GetDatabaseName().Returns(databaseName);
            databaseStorage.ExistsTable(Arg.Any<string>()).Returns(true);
            databaseStorage.ReadAllLines(Arg.Any<string>()).Returns(new[] { "nume:string","Ion" });
            databaseConfiguration.ParseDataTypeValue("string", "Ion").Returns("Ion");
            databaseConfiguration.ParseDataTypeValue("string", "Vasile").Returns("Vasile");

            var allowedDataTypes = new List<string> { "=" };
            databaseConfiguration.GetAllowedOperators().Returns(allowedDataTypes);
            databaseConfiguration.IsOperatorAllowedForDataType("string", "=").Returns(true);
            databaseConfiguration.PerformOperation(Arg.Any<string>(), Arg.Any<object>(), Arg.Any<object>(), Arg.Any<string>()).Returns(false);
            IQuery query = new SqlQuery("DELETE FROM " + tableName + " WHERE nume='Vasile'");
            var result = queryExecutor.Execute(databaseConnection, query);
            Assert.True(result.Success);
            Assert.Equal("Successfully deleted 0 records.", result.Message);
        }

        [Fact]
        public void Execute_WithValidUpdateQuery_ShouldSucceed()
        {
            var queryExecutor = GetSUT();
            var databaseConnection = Substitute.For<IDatabaseConnection>();
            var databaseStorage = Substitute.For<IDatabaseStorageService>();
            var databaseConfiguration = Substitute.For<IDatabaseConfiguration>();

            databaseStorage.ExistsDatabase(Arg.Any<string>()).Returns(true);
            const string serverLocation = "server-location";
            const string databaseName = "testdb";
            const string tableName = "test";

            databaseStorage.ExistsDatabase(serverLocation + "/" + databaseName).Returns(true);
            databaseConnection.GetServerLocation().Returns(serverLocation);
            databaseConnection.GetDatabaseConfiguration().Returns(databaseConfiguration);
            databaseConfiguration.DatabaseStorageService.Returns(databaseStorage);
            databaseConnection.GetDatabaseName().Returns(databaseName);
            databaseStorage.ExistsTable(Arg.Any<string>()).Returns(true);
            databaseStorage.ReadAllLines(Arg.Any<string>()).Returns(new[] { "nume:string","Ion" });
            IQuery query = new SqlQuery("UPDATE " + tableName + " SET nume='Vasile' WHERE nume='Ion'");
            var result = queryExecutor.Execute(databaseConnection, query);
            Assert.True(result.Success);
            Assert.Equal("Query successfully executed", result.Message);
        }

        [Fact]
        public void MatchesQuery_ShouldThrowNotImplementedException()
        {
            var queryExecutor = GetSUT();
            Assert.ThrowsAny<NotImplementedException>(() => queryExecutor.MatchesQuery(new SqlQuery("Any Query")));
        }

        private QueryExecutor GetSUT()
        {
            return new QueryExecutor();
        }
    }
}
