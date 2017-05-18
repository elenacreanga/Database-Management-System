using System.Collections.Generic;
using DbManagementSystem.Core.Query;
using DbManagementSystem.Core.Query.QueryResultSerializers;
using Xunit;

namespace DbManagementSystem.Test.Query.QueryResultSerializers
{
    public class XmlQueryResultSerializerTest
    {
        [Fact]
        public void Serialize()
        {
            var xmlQueryResultSerializer = GetSUT();
            var queryResult = new List<Dictionary<string, object>>();
            const string rowData = "Gigel";
            const string column = "Nume";
            var record = new Dictionary<string, object> { [column] = rowData };

            queryResult.Add(record);

            IQueryResult sqlQueryResult = new SqlQueryResult(1, true, "Success", queryResult);
            var result = xmlQueryResultSerializer.Serialize(sqlQueryResult);
            var expectedResult = "<Data>\n<Row><Nume>Gigel</Nume></Row></Data>";
            Assert.Equal(expectedResult, result);
        }

        private XmlQueryResultSerializer GetSUT()
        {
            return new XmlQueryResultSerializer();
        }
    }
}
