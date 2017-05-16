using DbManagementSystem.Core.Database;
using NSubstitute;
using Xunit;

namespace DbManagementSystem.Test.Database
{
    public class DatabaseConnectionTest
    {
        private string ServerLocation = "serverLocation";

        [Fact]
        public void CanGetDatabaseConfiguration()
        {
            var databaseConnection = GetSUT();
            var result = databaseConnection.GetDatabaseConfiguration();
            Assert.NotNull(result);
        }

        [Fact]
        public void GetDatabaseName_WhenNullDatabaseName_ShouldReturnEmptyString()
        {
            var databaseConnection = GetSUT();
            var result = databaseConnection.GetDatabaseName();
            Assert.Empty(result);
        }

        [Fact]
        public void GetDatabaseName_WhenNotNullDatabaseName_ShouldReturnName()
        {
            const string databaseName = "dbNameTest";
            var databaseConnection = GetSUT(databaseName);
            var result = databaseConnection.GetDatabaseName();
            Assert.Equal(databaseName, result);
        }

        [Fact]
        public void GetServerLocation_WhenNotNullServerLocation_ShouldReturnLocation()
        {
            var databaseConnection = GetSUT();
            var result = databaseConnection.GetServerLocation();
            Assert.Equal(ServerLocation, result);
        }

        [Fact]
        public void GetServerLocation_WhenNullServerLocation_ShouldReturnEmptyString()
        {
            ServerLocation = null;
            var databaseConnection = GetSUT();
            ServerLocation = "serverLocation";
            var result = databaseConnection.GetServerLocation();
            Assert.Empty(result);
        }

        private DatabaseConnection GetSUT(string databaseName = null)
        {
            var databaseConfiguration = Substitute.For<IDatabaseConfiguration>();
            return new DatabaseConnection(databaseConfiguration, ServerLocation, databaseName);
        }
    }
}
