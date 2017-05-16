using System;
using System.Collections.Generic;
using System.Linq;
using DbManagementSystem.Core.Database;
using Xunit;

namespace DbManagementSystem.Test.Database
{
    public class DatabaseStorageServiceTest
    {
        public DatabaseStorageServiceTest()
        {
            tempDbServer = new TempDbServer();
        }

        private readonly TempDbServer tempDbServer;

        [Fact]
        public void CreateDatabase_WhenIllegalCharactersInPath_ShouldThrowException()
        {
            var databaseStorageService = GetSUT();
            const string newdb = "*$£$%&£$DHasd";
            Assert.ThrowsAny<Exception>(() => databaseStorageService.CreateDatabase(newdb));
        }

        [Fact]
        public void CreateDatabase_WhenPathAlreadyExists_ShouldThrowException()
        {
            var databaseStorageService = GetSUT();
            const string newdb = "C:\\NewDb";
            tempDbServer.ExecuteOperation(() =>
                {
                    databaseStorageService.CreateDatabase(newdb);
                    var result = databaseStorageService.ExistsDatabase(newdb);
                    Assert.True(result);

                    Assert.ThrowsAny<Exception>(() => databaseStorageService.CreateDatabase(newdb));
                },
                () => { RevertDatabasesCreation(databaseStorageService, new [] { newdb }); });
        }

        [Fact]
        public void CreateDatabase_WhenValidPath_ShouldSucceed()
        {
            var databaseStorageService = GetSUT();
            const string newdb = "C:\\NewDb";

            tempDbServer.ExecuteOperation(() =>
            {
                databaseStorageService.CreateDatabase(newdb);
                var result = databaseStorageService.ExistsDatabase(newdb);
                Assert.True(result);

            },
            () => { RevertDatabasesCreation(databaseStorageService, new[] { newdb }); });
        }

        [Fact]
        public void DeleteDatabase_WhenInvalidCharactersInPath_ShouldThrowError()
        {
            var databaseStorageService = GetSUT();
            const string databaseName = "*$£$%&£$DHasd";

            Assert.ThrowsAny<Exception>(() => databaseStorageService.DeleteDatabase(databaseName));
        }

        [Fact]
        public void DeleteDatabase_WhenInvalidPath_ShouldThrowError()
        {
            var databaseStorageService = GetSUT();
            const string databaseName = "C:\\database";

            Assert.ThrowsAny<Exception>(() => databaseStorageService.DeleteDatabase(databaseName));
        }

        [Fact]
        public void DeleteDatabase_WhenValidPath_ShouldSucceed()
        {
            var databaseStorageService = GetSUT();
            const string databaseName = "C:\\database";
            CreateTempDatabase(databaseStorageService, databaseName);

            databaseStorageService.DeleteDatabase(databaseName);
            var result = databaseStorageService.ExistsDatabase(databaseName);
            Assert.False(result);
        }

        [Fact]
        public void ExistsDatabase_WhenInvalidCharactersInPath_ShouldReturnFalse()
        {
            var databaseStorageService = GetSUT();
            var result = databaseStorageService.ExistsDatabase("\t*5492^#$543");
            Assert.False(result);
        }

        [Fact]
        public void ExistsDatabase_WhenInvalidDirectory_ShouldReturnFalse()
        {
            var databaseStorageService = GetSUT();
            var result = databaseStorageService.ExistsDatabase("C:\\ThisDirectoryDoesNotExist");
            Assert.False(result);
        }

        [Fact]
        public void ExistsDatabase_WhenNullPath_ShouldReturnFalse()
        {
            var databaseStorageService = GetSUT();
            var result = databaseStorageService.ExistsDatabase(null);
            Assert.False(result);
        }

        [Fact]
        public void ExistsDatabase_WhenValidDirectory_ShouldReturnTrue()
        {
            var databaseStorageService = GetSUT();
            var result = databaseStorageService.ExistsDatabase("C:\\Windows");
            Assert.True(result);
        }

        [Fact]
        public void GetDatabase_WhenDatabasePathExists_ShouldSucceed()
        {
            var databaseStorageService = GetSUT();
            const string serverlocationDatabase = "C:\\tempServerLocation";
            const string databaseone = "databaseOne";
            const string databaseNameOne = serverlocationDatabase + "\\" + databaseone;
            const string databasetwo = "databaseTwo";
            const string databaseNameTwo = serverlocationDatabase + "\\" + databasetwo;

            tempDbServer.ExecuteOperation(() =>
                {
                    CreateTempDatabase(databaseStorageService, databaseNameOne);
                    CreateTempDatabase(databaseStorageService, databaseNameTwo);

                    var result = databaseStorageService.GetDatabases(serverlocationDatabase);
                    Assert.NotNull(result);
                    Assert.NotEmpty(result);
                    Assert.Equal(2, result.Count());
                    Assert.Contains(databaseone, result);
                    Assert.Contains(databasetwo, result);
                },
                () => { RevertDatabasesCreation(databaseStorageService, new[] { serverlocationDatabase }); });
            
        }

        [Fact]
        public void GetDatabase_WhenInvalidDatabasePath_ShouldReturnNull()
        {
            var databaseStorageService = GetSUT();
            const string serverlocationDatabase = "C:\\inexistentServerLocation";

            var result = databaseStorageService.GetDatabases(serverlocationDatabase);
            Assert.Null(result);
        }

        [Fact]
        public void GetDatabase_WhenInvalidCharactersInDatabasePath_ShouldThrowException()
        {
            var databaseStorageService = GetSUT();
            const string serverlocationDatabase = "C:\\inexistentServerLocation";

            Assert.ThrowsAny<Exception>(() => databaseStorageService.GetDatabases(serverlocationDatabase));
        }

        [Fact]
        public void MoveDatabase_WhenValidPaths_ShouldSucceed()
        {
            var databaseStorageService = GetSUT();
            const string serverlocationDatabase = "C:\\serverLocation";
            const string inexistentServerlocationDatabase = "C:\\inexistentServerLocation";
            tempDbServer.ExecuteOperation(() =>
            {
                CreateTempDatabase(databaseStorageService, serverlocationDatabase);

                databaseStorageService.MoveDatabase(serverlocationDatabase, inexistentServerlocationDatabase);
                var result = databaseStorageService.ExistsDatabase(inexistentServerlocationDatabase);
                Assert.True(result);
            },
            () => { RevertDatabasesCreation(databaseStorageService, new [] { inexistentServerlocationDatabase }); });
        }

        [Fact]
        public void MoveDatabase_WhenNewPathAlreadyExists_ShouldThrowError()
        {
            var databaseStorageService = GetSUT();
            const string serverlocationDatabase = "C:\\serverLocation";
            const string alreadyExistingServerLocationDatabase = "C:\\alreadyExistingServerLocation";
            tempDbServer.ExecuteOperation(() =>
                {
                    CreateTempDatabase(databaseStorageService, serverlocationDatabase);
                    CreateTempDatabase(databaseStorageService, alreadyExistingServerLocationDatabase);

                    Assert.ThrowsAny<Exception>(() => databaseStorageService.MoveDatabase(serverlocationDatabase,
                        alreadyExistingServerLocationDatabase));
                },
                () =>
                {
                    var newDatabases = new[] {serverlocationDatabase, alreadyExistingServerLocationDatabase};
                    RevertDatabasesCreation(databaseStorageService, newDatabases);
                });
        }

        [Fact]
        public void MoveDatabase_WhenOldPathInvalid_ShouldThrowError()
        {
            var databaseStorageService = GetSUT();
            const string serverlocationDatabase = "C:\\serverLocation";
            const string newServerLocationDatabase = "C:\\newServerLocation";
            Assert.ThrowsAny<Exception>(() => databaseStorageService.MoveDatabase(serverlocationDatabase,
                newServerLocationDatabase));
        }

        [Fact]
        public void MoveDatabase_WhenNewPathOnAnotherDrive_ShouldThrowError()
        {
            var databaseStorageService = GetSUT();
            const string serverlocationDatabase = "C:\\serverLocation";
            const string newServerLocationDatabase = "D:\\newServerLocation";
            tempDbServer.ExecuteOperation(() =>
                {
                    CreateTempDatabase(databaseStorageService, serverlocationDatabase);
                    Assert.ThrowsAny<Exception>(() => databaseStorageService.MoveDatabase(serverlocationDatabase,
                        newServerLocationDatabase));
                },
                () =>
                {
                    var newDatabases = new[] { serverlocationDatabase };
                    RevertDatabasesCreation(databaseStorageService, newDatabases);
                });
        }

        [Fact]
        public void MoveDatabase_WhenNewPathIsInvalid_ShouldThrowError()
        {
            var databaseStorageService = GetSUT();
            const string serverlocationDatabase = "C:\\serverLocation";
            const string newServerLocationDatabase = "C:\\*$#@#$$%newServerLocation";
            tempDbServer.ExecuteOperation(() =>
                {
                    CreateTempDatabase(databaseStorageService, serverlocationDatabase);
                    Assert.ThrowsAny<Exception>(() => databaseStorageService.MoveDatabase(serverlocationDatabase,
                        newServerLocationDatabase));
                },
                () =>
                {
                    var newDatabases = new[] { serverlocationDatabase };
                    RevertDatabasesCreation(databaseStorageService, newDatabases);
                });
        }

        private static void CreateTempDatabase(DatabaseStorageService databaseStorageService, string serverlocationDatabase)
        {
            databaseStorageService.CreateDatabase(serverlocationDatabase);
            var isDatabaseSuccessfullyCreated = databaseStorageService.ExistsDatabase(serverlocationDatabase);
            Assert.True(isDatabaseSuccessfullyCreated);
        }

        private static void RevertDatabasesCreation(DatabaseStorageService databaseStorageService,
            IEnumerable<string> newDatabases)
        {
            foreach (var newDatabase in newDatabases)
                databaseStorageService.DeleteDatabase(newDatabase);
        }

        private DatabaseStorageService GetSUT()
        {
            return new DatabaseStorageService();
        }
    }
}