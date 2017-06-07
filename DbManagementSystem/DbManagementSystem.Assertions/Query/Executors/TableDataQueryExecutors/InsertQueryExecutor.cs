using System;
using DbManagementSystem.Assertions.Database;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace DbManagementSystem.Assertions.Query.Executors.TableDataQueryExecutors
{
    public class InsertQueryExecutor : IQueryExecutor
    {
        private static readonly string MATCH = @"^INSERT INTO (?<tableName>\w+)\((?<columns>[\s\S]+)\) VALUES\((?<values>[\s\S]+)\)$";

        public IQueryResult Execute(IDatabaseConnection databaseConnection, IQuery query)
        {
            var sqlQuery = query.GetQuery();
            var match = Regex.Match(sqlQuery, MATCH, RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                return new SqlQueryResult(0, false, string.Format("Invalid query: --{0}--", sqlQuery), null);
            }

            var tableName = match.Groups["tableName"].Value;
            var tableLocation = databaseConnection.GetServerLocation() + "/" + databaseConnection.GetDatabaseName() + "/" + tableName;
            if (!databaseConnection.GetDatabaseConfiguration().DatabaseStorageService.ExistsTable(tableLocation))
            {
                return new SqlQueryResult(0, false, string.Format("Table does not exist: --{0}--", sqlQuery), null);
            }

            var columns = match.Groups["columns"].Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (!columns.Any())
            {
                return new SqlQueryResult(0, false, string.Format("Invalid columns: --{0}--", sqlQuery), null);
            }

            var orderedTableColumns = databaseConnection.GetDatabaseConfiguration().DatabaseStorageService.ReadLines(tableLocation).FirstOrDefault().Split(',').ToList();
            var tableColumns = orderedTableColumns.ToDictionary(k => k.Split(':')[0], v => v.Split(':')[1]);
            foreach (var columnName in columns)
            {
                if (!tableColumns.ContainsKey(columnName))
                {
                    return new SqlQueryResult(0, false, string.Format("Column does not exist: --{0}--", columnName), null);
                }
            }

            var values = match.Groups["values"].Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (!columns.Any())
            {
                return new SqlQueryResult(0, false, string.Format("Invalid values: --{0}--", sqlQuery), null);
            }

            if (columns.Count != values.Count)
            {
                return new SqlQueryResult(0, false, string.Format("Values does not match columns: --{0}--", sqlQuery), null);
            }

            var row = new Dictionary<string, object>();
            for (var i = 0; i < columns.Count; i++)
            {
                var column = columns[i];
                var columnDataType = tableColumns[column];
                var rawValue = values[i];
                if (rawValue.StartsWith("'"))
                {
                    rawValue = rawValue.Substring(1);
                }

                if (rawValue.EndsWith("'"))
                {
                    rawValue = rawValue.Substring(0, rawValue.Length - 1);
                }

                var value = databaseConnection.GetDatabaseConfiguration().ParseDataTypeValue(columnDataType, rawValue);
                row[column] = value;
            }

            foreach (var column in tableColumns.Keys)
            {
                if (!row.ContainsKey(column))
                {
                    row[column] = databaseConnection.GetDatabaseConfiguration().GetDefaultValueForDataType(tableColumns[column]);
                }
            }

            var rowData = string.Empty;
            foreach (var column in orderedTableColumns)
            {
                rowData = string.Format("{0},{1}", rowData, row[column.Split(':')[0]]);
            }

            rowData = string.Format("{0}\n", rowData.Substring(1));
            databaseConnection.GetDatabaseConfiguration().DatabaseStorageService.AppendAllText(tableLocation, rowData);
            return new SqlQueryResult(1, true, "Record successfully inserted", null);
        }

        public bool MatchesQuery(IQuery query)
        {
            return Regex.Match(query.GetQuery(), MATCH, RegexOptions.IgnoreCase).Success;
        }
    }
}
