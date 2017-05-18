using System.Collections.Generic;
using DbManagementSystem.Core.Query;
using DbManagementSystem.Core.Query.QueryResultSerializers;
using Xunit;

namespace DbManagementSystem.Test.Query.QueryResultSerializers
{
    public class CsvQueryResultSerializerTest
    {
        [Fact]
        public void Serialize_WhenValidData_ShouldSucceed()
        {
            var csvQueryResultSerializer = GetSUT();

            var queryResult = new List<Dictionary<string, object>>();
            const string rowData = "Gigel";
            const string column = "Nume";
            var record = new Dictionary<string, object> {[column] = rowData};

            queryResult.Add(record);

            var sqlQueryResult = new SqlQueryResult(1, true, "Success", queryResult);

            var result = csvQueryResultSerializer.Serialize(sqlQueryResult);
            Assert.Equal(column + "\n" + rowData, result);
        }

        [Fact]
        public void Serialize_WhenDataWithSpecialCharacters_ShouldSucceed()
        {
            var csvQueryResultSerializer = GetSUT();

            var queryResult = new List<Dictionary<string, object>>();
            const string rowData = "\tGigel,,,@#$@%$@!#\n\n";
            const string column = "Nume,,,,";
            var record = new Dictionary<string, object> {[column] = rowData};

            queryResult.Add(record);

            var sqlQueryResult = new SqlQueryResult(1, true, "Success", queryResult);

            var result = csvQueryResultSerializer.Serialize(sqlQueryResult);
            Assert.Equal(column + "\n" + rowData, result);
        }

        [Fact]
        public void Serialize_WhenEmptyData_ShouldSucceed()
        {
            var csvQueryResultSerializer = GetSUT();
            var sqlQueryResult = new SqlQueryResult(1, true, "Success", null);

            var result = csvQueryResultSerializer.Serialize(sqlQueryResult);

            Assert.Empty(result);
        }

        private CsvQueryResultSerializer GetSUT()
        {
            return new CsvQueryResultSerializer();
        }
    }
}
