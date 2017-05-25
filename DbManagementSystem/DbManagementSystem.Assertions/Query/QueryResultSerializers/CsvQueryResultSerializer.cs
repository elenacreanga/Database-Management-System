namespace DbManagementSystem.Assertions.Query.QueryResultSerializers
{
    public class CsvQueryResultSerializer : IQueryResultSerializer
    {
        public string Serialize(IQueryResult queryResult)
        {
            var columns = queryResult.GetColumnNames();

            var csv = string.Join(",", columns);

            while (queryResult.Read())
            {
                var row = string.Empty;
                foreach (var column in columns)
                {
                    row = string.Format("{0},{1}", row, queryResult.GetValue(column));
                }

                csv = string.Format("{0}\n{1}", csv, row.Substring(1));
            }

            return csv;
        }
    }
}
