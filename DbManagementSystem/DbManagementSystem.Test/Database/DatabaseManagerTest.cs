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

            var databaseStorage = new DatabaseStorageService();
            var databaseConfiguration = new DatabaseCofiguration(databaseStorage);

            databaseConnection.GetDatabaseConfiguration().Returns(databaseConfiguration);

            var databaseManager = GetSUT(databaseConnection);
            var result = databaseManager.GetDatabase("nonExistingDb");
            Assert.Null(result);
        }

        private DatabaseManager GetSUT(IDatabaseConnection databaseConnection)
        {
            return new DatabaseManager(databaseConnection);
        }

    }
}
