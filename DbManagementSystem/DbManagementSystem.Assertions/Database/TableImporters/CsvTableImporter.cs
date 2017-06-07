using DbManagementSystem.Assertions.Query;
using DbManagementSystem.Assertions.Query.Executors;
using System;
using System.Diagnostics.Contracts;

namespace DbManagementSystem.Assertions.Database.TableImporters
{
    public class CsvTableImporter : ITableImporter
    {
        public bool Import(IDatabaseConnection databaseConnection, string tableName, string data)
        {
            Contract.Requires(!string.IsNullOrEmpty(tableName));
            Contract.Requires(!string.IsNullOrEmpty(data));
            Contract.Requires(databaseConnection != null);

            var rows = data.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
           
            if (rows.Length < 1)
            {
                return false;
            }

            var columns = rows[0].Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (columns.Length < 1)
            {
                return false;
            }

            for (var i = 1; i < rows.Length; i++)
            {
                var columnValues = rows[i].Split(',');
                if (columns.Length == columnValues.Length)
                {
                    var rawQuery = string.Format("INSERT INTO {0}({1}) VALUES({2})", tableName, string.Join(",", columns), string.Join(",", columnValues));
                    IQuery query = new SqlQuery(rawQuery);
                    IQueryExecutor queryExecutor = new QueryExecutor();
                    queryExecutor.Execute(databaseConnection, query);
                }
            }

            return true;
        }
    }
}
