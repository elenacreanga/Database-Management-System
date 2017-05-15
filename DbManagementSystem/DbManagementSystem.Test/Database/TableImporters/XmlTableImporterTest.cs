using DbManagementSystem.Core.Database;
using DbManagementSystem.Core.Database.TableImporters;
using NSubstitute;
using Xunit;

namespace DbManagementSystem.Test.Database.TableImporters
{
    public class XmlTableImporterTest
    {
        [Fact]
        public void Import_WhenValidData_ShouldReturnTrue()
        {
            var xmlTableImporter = GetSUT();
            var databaseConnection = Substitute.For<IDatabaseConnection>();
            var data = "<Data><Row>Line1</Row><Row>Line2</Row></Data>";
            var table = "testTable";
            var result = xmlTableImporter.Import(databaseConnection, table, data);
            Assert.True(result);
        }

        [Fact]
        public void Import_WhenInvalidFormatData_ShouldReturnFalse()
        {
            var xmlTableImporter = GetSUT();
            var databaseConnection = Substitute.For<IDatabaseConnection>();
            var data = "<Data><Row>Line1<Row>Line2</Row></Data>";
            var table = "testTable";
            var result = xmlTableImporter.Import(databaseConnection, table, data);
            Assert.False(result);
        }
        private XmlTableImporter GetSUT()
        {
            return new XmlTableImporter();
        }
    }
}
