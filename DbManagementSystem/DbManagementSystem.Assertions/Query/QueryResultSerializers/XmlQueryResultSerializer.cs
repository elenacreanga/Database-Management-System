namespace DbManagementSystem.Assertions.Query.QueryResultSerializers
{
    public class XmlQueryResultSerializer : IQueryResultSerializer
    {
        public string Serialize(IQueryResult queryResult)
        {
            var columns = queryResult.GetColumnNames();
            var xml = string.Empty;

            while (queryResult.Read())
            {
                var row = string.Empty;
                foreach (var column in columns)
                {
                    row = string.Format("{0}\n<{1}>{2}</{1}>", row, column, queryResult.GetValue(column));
                }

                xml = string.Format("{0}\n<Row>{1}</Row>", xml, row.Substring(1));
            }

            return string.Format("<Data>{0}</Data>", xml);
        }
    }
}
