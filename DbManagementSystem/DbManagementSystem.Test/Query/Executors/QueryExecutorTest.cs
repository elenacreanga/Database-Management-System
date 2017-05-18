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
        public void Execute_WithValidQuery_ShouldSucceed()
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


        private QueryExecutor GetSUT()
        {
            return new QueryExecutor();
        }
    }
}
