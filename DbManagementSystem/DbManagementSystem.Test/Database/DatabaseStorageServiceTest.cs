using System;
using System.Collections.Generic;
using System.IO;
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
            Assert.ThrowsAny<ArgumentException>(() => databaseStorageService.CreateDatabase(newdb));
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

            Assert.ThrowsAny<ArgumentException>(() => databaseStorageService.DeleteDatabase(databaseName));
        }

        [Fact]
        public void DeleteDatabase_WhenInvalidPath_ShouldThrowError()
        {
            var databaseStorageService = GetSUT();
            const string databaseName = "C:\\database";

            Assert.ThrowsAny<DirectoryNotFoundException>(() => databaseStorageService.DeleteDatabase(databaseName));
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

            Assert.ThrowsAny<DirectoryNotFoundException>(() => databaseStorageService.GetDatabases(serverlocationDatabase));
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

                    Assert.ThrowsAny<IOException>(() => databaseStorageService.MoveDatabase(serverlocationDatabase,
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
            Assert.ThrowsAny<DirectoryNotFoundException>(() => databaseStorageService.MoveDatabase(serverlocationDatabase,
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
                    Assert.ThrowsAny<IOException>(() => databaseStorageService.MoveDatabase(serverlocationDatabase,
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
                    Assert.ThrowsAny<ArgumentException>(() => databaseStorageService.MoveDatabase(serverlocationDatabase,
                        newServerLocationDatabase));
                },
                () =>
                {
                    var newDatabases = new[] { serverlocationDatabase };
                    RevertDatabasesCreation(databaseStorageService, newDatabases);
                });
        }

        [Fact]
        public void CreateTable_WhenTableDoesNotExist_ShouldCreate()
        {
            var databaseStorageService = GetSUT();
            const string serverlocationDatabase = "C:\\serverLocation";
            tempDbServer.ExecuteOperation(() =>
            {
                CreateTempDatabase(databaseStorageService, serverlocationDatabase);
                const string tablePath = serverlocationDatabase + "\\" + "table";
                databaseStorageService.WriteAllText(tablePath, "");
                var result = databaseStorageService.ExistsTable(tablePath);
                Assert.True(result);
            },
            () => { RevertDatabasesCreation(databaseStorageService, new []{ serverlocationDatabase });});
        }

        [Fact]
        public void CreateTable_WhenDatabaseDoesNotExist_ShouldFail()
        {
            var databaseStorageService = GetSUT();
            const string serverlocationDatabase = "C:\\serverLocation";
            const string tablePath = serverlocationDatabase + "\\" + "table";
            Assert.ThrowsAny<Exception>(() => databaseStorageService.WriteAllText(tablePath, string.Empty));
        }

        [Fact]
        public void CreateTable_WhenTableAlreadyExists_ShouldThrowException()
        {
            var databaseStorageService = GetSUT();
            const string serverlocationDatabase = "C:\\serverLocation";
            tempDbServer.ExecuteOperation(() =>
                {
                    CreateTempDatabase(databaseStorageService, serverlocationDatabase);
                    const string tablePath = serverlocationDatabase + "\\" + "table";
                    databaseStorageService.WriteAllText(tablePath, string.Empty);
                    var result = databaseStorageService.ExistsTable(tablePath);
                    Assert.True(result);
                    Assert.ThrowsAny<Exception>(() => databaseStorageService.WriteAllText(tablePath, string.Empty));
                },
                () => { RevertDatabasesCreation(databaseStorageService, new[] { serverlocationDatabase }); });
        }

        [Fact]
        public void AlterTable_WhenTableExists_ShouldSucceed()
        {
            var databaseStorageService = GetSUT();
            const string serverlocationDatabase = "C:\\serverLocation";
            const string columns = "nume, prenume";
            tempDbServer.ExecuteOperation(() =>
                {
                    CreateTempDatabase(databaseStorageService, serverlocationDatabase);
                    const string tablePath = serverlocationDatabase + "\\" + "table";
                    databaseStorageService.WriteAllText(tablePath, string.Empty);
                    var isTableCreated = databaseStorageService.ExistsTable(tablePath);
                    Assert.True(isTableCreated);
                    databaseStorageService.WriteAllLines(tablePath, new [] {columns});
                    var result = databaseStorageService.ReadAllLines(tablePath);
                    Assert.Equal(columns, result.FirstOrDefault());

                },
                () => { RevertDatabasesCreation(databaseStorageService, new[] { serverlocationDatabase }); });
        }

        [Fact]
        public void AlterTable_WhenTableDoesNotExist_ShouldCreateIt()
        {
            var databaseStorageService = GetSUT();
            const string serverlocationDatabase = "C:\\serverLocation";
            const string columns = "nume, prenume";
            tempDbServer.ExecuteOperation(() =>
                {
                    CreateTempDatabase(databaseStorageService, serverlocationDatabase);
                    const string tablePath = serverlocationDatabase + "\\" + "table";
                    var isTableCreated = databaseStorageService.ExistsTable(tablePath);
                    Assert.False(isTableCreated);
                    databaseStorageService.WriteAllLines(tablePath, new [] {columns});
                    var result = databaseStorageService.ReadAllLines(tablePath);
                    Assert.Equal(columns, result.FirstOrDefault());

                },
                () => { RevertDatabasesCreation(databaseStorageService, new[] { serverlocationDatabase }); });
        }

        [Fact]
        public void DeleteTable_WhenTableExists_ShouldSucceed()
        {
            var databaseStorageService = GetSUT();
            const string serverlocationDatabase = "C:\\serverLocation";
            tempDbServer.ExecuteOperation(() =>
                {
                    CreateTempDatabase(databaseStorageService, serverlocationDatabase);
                    const string tablePath = serverlocationDatabase + "\\" + "table";
                    databaseStorageService.WriteAllText(tablePath, string.Empty);
                    var isTableCreated = databaseStorageService.ExistsTable(tablePath);
                    Assert.True(isTableCreated);

                    databaseStorageService.DeleteTable(tablePath);
                    var result = databaseStorageService.ExistsTable(tablePath);

                    Assert.False(result);

                },
                () => { RevertDatabasesCreation(databaseStorageService, new[] { serverlocationDatabase }); });
        }

        [Fact]
        public void DeleteTable_WhenTableDoesNotExist_ShouldThrowError()
        {
            var databaseStorageService = GetSUT();
            const string serverlocationDatabase = "C:\\serverLocation";
            tempDbServer.ExecuteOperation(() =>
                {
                    CreateTempDatabase(databaseStorageService, serverlocationDatabase);
                    const string tablePath = serverlocationDatabase + "\\" + "table";
                    var isTableCreated = databaseStorageService.ExistsTable(tablePath);
                    Assert.False(isTableCreated);

                    Assert.ThrowsAny<Exception>(() => databaseStorageService.DeleteTable(tablePath));
                },
                () => { RevertDatabasesCreation(databaseStorageService, new[] { serverlocationDatabase }); });
        }

        [Fact]
        public void DeleteTable_WhenInvalidPathForTable_ShouldThrowError()
        {
            var databaseStorageService = GetSUT();
            const string serverlocationDatabase = "C:\\serverLocation";
            tempDbServer.ExecuteOperation(() =>
                {
                    CreateTempDatabase(databaseStorageService, serverlocationDatabase);
                    const string tablePath = serverlocationDatabase + "\\" + "*342#@$%table";
                    var isTableCreated = databaseStorageService.ExistsTable(tablePath);
                    Assert.False(isTableCreated);

                    Assert.ThrowsAny<ArgumentException>(() => databaseStorageService.DeleteTable(tablePath));
                },
                () => { RevertDatabasesCreation(databaseStorageService, new[] { serverlocationDatabase }); });
        }

        [Fact]
        public void ReadAllLines_WhenPathCorrect_ShouldSucceed()
        {
            var databaseStorageService = GetSUT();
            const string serverlocationDatabase = "C:\\serverLocation";
            tempDbServer.ExecuteOperation(() =>
                {
                    CreateTempDatabase(databaseStorageService, serverlocationDatabase);
                    const string tablePath = serverlocationDatabase + "\\" + "table";
                    const string randomtext = "RandomText";
                    CreateTempTable(databaseStorageService, tablePath, randomtext);

                    var result = databaseStorageService.ReadAllLines(tablePath);
                    Assert.NotNull(result);
                    Assert.NotEmpty(result);
                    Assert.Equal(1, result.Length);
                    Assert.Equal(randomtext, result.FirstOrDefault());
                },
                () => { RevertDatabasesCreation(databaseStorageService, new[] { serverlocationDatabase }); });
        }

        [Fact]
        public void ReadAllLines_WhenInvalidPath_ShouldThrowError()
        {
            var databaseStorageService = GetSUT();
            const string serverlocationDatabase = "C:\\serverLocation";
            tempDbServer.ExecuteOperation(() =>
                {
                    CreateTempDatabase(databaseStorageService, serverlocationDatabase);
                    const string tablePath = serverlocationDatabase + "\\" + "table";
                    var isTableCreated = databaseStorageService.ExistsTable(tablePath);
                    Assert.False(isTableCreated);

                    Assert.ThrowsAny<FileNotFoundException>(() => databaseStorageService.ReadAllLines(tablePath));
                },
                () => { RevertDatabasesCreation(databaseStorageService, new[] { serverlocationDatabase }); });
        }

        [Fact]
        public void EnumerateTables_WhenValidPath_ShouldReturnTables()
        {
            var databaseStorageService = GetSUT();
            const string databaseName = "C:\\tempServerLocation";
            const string tableOne = "tableOne";
            const string tableNameOne = databaseName + "\\" + tableOne;
            const string tableTwo = "tableTwo";
            const string tableNameTwo = databaseName + "\\" + tableTwo;

            tempDbServer.ExecuteOperation(() =>
                {
                    CreateTempDatabase(databaseStorageService, databaseName);
                    CreateTempTable(databaseStorageService, tableNameOne);
                    CreateTempTable(databaseStorageService, tableNameTwo);
                    var result = databaseStorageService.EnumerateTables(databaseName);
                    Assert.NotNull(result);
                    Assert.NotEmpty(result);
                    Assert.Equal(2, result.Count());
                    Assert.Contains(tableNameOne, result);
                    Assert.Contains(tableNameTwo, result);
                },
                () => { RevertDatabasesCreation(databaseStorageService, new[] { databaseName }); });
        }

        [Fact]
        public void EnumerateTables_WhenInvalidPath_ShouldThrowError()
        {
            var databaseStorageService = GetSUT();
            const string databaseName = "C:\\tempServerLocation";
            Assert.ThrowsAny<DirectoryNotFoundException>(() => databaseStorageService.EnumerateTables(databaseName));
        }

        private static void CreateTempDatabase(DatabaseStorageService databaseStorageService, string serverlocationDatabase)
        {
            databaseStorageService.CreateDatabase(serverlocationDatabase);
            var isDatabaseSuccessfullyCreated = databaseStorageService.ExistsDatabase(serverlocationDatabase);
            Assert.True(isDatabaseSuccessfullyCreated);
        }

        private static void CreateTempTable(DatabaseStorageService databaseStorageService, string tablePath, string tableContent = null)
        {
            databaseStorageService.WriteAllText(tablePath, tableContent);
            var isTableCreated = databaseStorageService.ExistsTable(tablePath);
            Assert.True(isTableCreated);
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