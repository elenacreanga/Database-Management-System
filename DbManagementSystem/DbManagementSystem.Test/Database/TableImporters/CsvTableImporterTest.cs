using DbManagementSystem.Core.Database;
using DbManagementSystem.Core.Database.TableImporters;
using NSubstitute;
using Xunit;

namespace DbManagementSystem.Test.Database.TableImporters
{
    public class CsvTableImporterTest
    {
        [Fact]
        public void Import_WhenDataWithEmptyColumnHeaderButSameColumnDataForRows_ShouldReturnTrue()
        {
            var csvTableImporter = GetSUT();
            var data = "Nume,,,,\nPopescu\n\n";
            var tableName = "testTable";
            var databaseConnection = Substitute.For<IDatabaseConnection>();
            var result = csvTableImporter.Import(databaseConnection, tableName, data);
            Assert.True(result);
        }

        [Fact]
        public void Import_WhenDataWithTableHeaderAndLessColumnDataForRows_ShouldReturnFalse()
        {
            var csvTableImporter = GetSUT();
            var data = "Nume, Prenume\nPopescu\n\n";
            var tableName = "testTable";
            var databaseConnection = Substitute.For<IDatabaseConnection>();
            var result = csvTableImporter.Import(databaseConnection, tableName, data);
            Assert.False(result);
        }

        [Fact]
        public void Import_WhenDataWithTableHeaderAndMoreColumnDataForRows_ShouldReturnFalse()
        {
            var csvTableImporter = GetSUT();
            var data = "Nume\nMaria,Popescu\n\n";
            var tableName = "testTable";
            var databaseConnection = Substitute.For<IDatabaseConnection>();
            var result = csvTableImporter.Import(databaseConnection, tableName, data);
            Assert.False(result);
        }

        [Fact]
        public void Import_WhenDataWithTableHeaderButNoColumn_ShouldReturnFalse()
        {
            var csvTableImporter = GetSUT();
            var data = "as\n\n\n";
            var tableName = "testTable";
            var databaseConnection = Substitute.For<IDatabaseConnection>();
            var result = csvTableImporter.Import(databaseConnection, tableName, data);
            Assert.False(result);
        }

        [Fact]
        public void Import_WhenInvalidTableName_ShouldReturnFalse()
        {
            var csvTableImporter = GetSUT();
            var data = "Nume\nPopescu\n\n";
            var tableName = "";
            var databaseConnection = Substitute.For<IDatabaseConnection>();
            var result = csvTableImporter.Import(databaseConnection, tableName, data);
            Assert.False(result);
        }

        [Fact]
        public void Import_WhenSuccessful_ShouldReturnTrue()
        {
            var csvTableImporter = GetSUT();
            var data = "Nume,Prenume\nPopescu, Maria\nBinculescu, Vasilica\n";
            var tableName = "testTable";
            var databaseConnection = Substitute.For<IDatabaseConnection>();

            var result = csvTableImporter.Import(databaseConnection, tableName, data);
            Assert.True(result);
        }

        private CsvTableImporter GetSUT()
        {
            return new CsvTableImporter();
        }
    }
}