using System;
using DbManagementSystem.Assertions.Database;
using System.Text.RegularExpressions;
using System.Linq;
using DbManagementSystem.Assertions.Query.Clauses;
using System.Collections.Generic;

namespace DbManagementSystem.Assertions.Query.Executors.TableDataQueryExecutors
{
    public class SelectQueryExecutor : IQueryExecutor
    {
        private static readonly string MATCH = @"^SELECT (?<columns>[\s\S]+) FROM (?<tableName>\w+)(?<whereClause> WHERE (?<whereExpression>[\s\S]+))?$";

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

            var tableData = databaseConnection.GetDatabaseConfiguration().DatabaseStorageService.ReadAllLines(tableLocation);
            var orderedTableColumns = tableData.FirstOrDefault().Split(',').ToList();
            var tableColumns = orderedTableColumns.ToDictionary(k => k.Split(':')[0], v => v.Split(':')[1]);
            if (columns.Count == 1 && columns[0].Equals("*"))
            {
                columns = tableColumns.Keys.ToList();
            }

            foreach (var columnName in columns)
            {
                if (!tableColumns.ContainsKey(columnName))
                {
                    return new SqlQueryResult(0, false, string.Format("Column does not exist: --{0}--", columnName), null);
                }
            }

            var whereClause = new WhereClause(databaseConnection.GetDatabaseConfiguration());
            if (match.Groups["whereClause"].Success)
            {
                var whereExpression = match.Groups["whereClause"].Success ? match.Groups["whereExpression"].Value : string.Empty;
                whereClause.Build(whereExpression, tableColumns);
            }

            var result = new List<Dictionary<string, object>>();
            for (var i = 1; i < tableData.Length; i++)
            {
                var row = tableData[i];
                var rowData = GetRowData(databaseConnection.GetDatabaseConfiguration(), row, orderedTableColumns);
                if (whereClause.MatchesRow(rowData))
                {
                    var record = new Dictionary<string, object>();
                    foreach (var column in columns)
                    {
                        record[column] = rowData[column];
                    }

                    result.Add(record);
                }
            }

            return new SqlQueryResult(0, true, "Query successfully executed", result);
        }

        public bool MatchesQuery(IQuery query)
        {
            return Regex.Match(query.GetQuery(), MATCH, RegexOptions.IgnoreCase).Success;
        }

        private Dictionary<string, object> GetRowData(IDatabaseConfiguration databaseConfiguration, string row, List<string> orderedTableColumns)
        {
            var rowData = new Dictionary<string, object>();
            var rowValues = row.Split(',');
            for (var i = 0; i < orderedTableColumns.Count; i++)
            {
                var column = orderedTableColumns[i].Split(':');
                var value = databaseConfiguration.ParseDataTypeValue(column[1], rowValues[i]);
                rowData[column[0]] = value;
            }

            return rowData;
        }
    }
}
