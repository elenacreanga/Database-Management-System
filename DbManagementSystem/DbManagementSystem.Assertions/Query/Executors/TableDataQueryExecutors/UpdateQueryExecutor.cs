using System;
using DbManagementSystem.Assertions.Database;
using System.Text.RegularExpressions;
using System.Linq;
using DbManagementSystem.Assertions.Query.Clauses;
using System.Collections.Generic;

namespace DbManagementSystem.Assertions.Query.Executors.TableDataQueryExecutors
{
    public class UpdateQueryExecutor : IQueryExecutor
    {
        private static readonly string MATCH = @"^UPDATE (?<tableName>\w+) SET (?<set>[\s\S]+)$";

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

            var setExpression = match.Groups["set"].Value;
            var idx = setExpression.ToUpper().IndexOf(" WHERE ");
            var rawColumnsToUpdate = string.Empty;
            var whereExpression = string.Empty;
            if (idx >= 0)
            {
                rawColumnsToUpdate = setExpression.Substring(0, idx);
                whereExpression = setExpression.Substring(idx + 7);
            }
            else
            {
                rawColumnsToUpdate = setExpression;
            }

            var columnsToUpdate = rawColumnsToUpdate.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToDictionary(k => k.Split('=')[0].Trim(), v => v.Split('=')[1].Trim());
            var tableData = databaseConnection.GetDatabaseConfiguration().DatabaseStorageService.ReadAllLines(tableLocation);
            var orderedTableColumns = tableData.FirstOrDefault().Split(',').ToList();
            var tableColumns = orderedTableColumns.ToDictionary(k => k.Split(':')[0], v => v.Split(':')[1]);
            foreach (var columnName in columnsToUpdate.Keys)
            {
                if (!tableColumns.ContainsKey(columnName))
                {
                    return new SqlQueryResult(0, false, string.Format("Column does not exist: --{0}--", columnName), null);
                }
            }

            var whereClause = new WhereClause(databaseConnection.GetDatabaseConfiguration());
            if (!string.IsNullOrWhiteSpace(whereExpression))
            {
                whereClause.Build(whereExpression, tableColumns);
            }

            var affectedRows = 0;
            for (var i = 1; i < tableData.Length; i++)
            {
                var row = tableData[i];
                var rowData = GetRowData(databaseConnection.GetDatabaseConfiguration(), row, orderedTableColumns);
                if (whereClause.MatchesRow(rowData))
                {
                    foreach (var column in columnsToUpdate.Keys)
                    {
                        var columnDataType = tableColumns[column];
                        var rawValue = columnsToUpdate[column];
                        if (rawValue.StartsWith("'"))
                        {
                            rawValue = rawValue.Substring(1);
                        }

                        if (rawValue.EndsWith("'"))
                        {
                            rawValue = rawValue.Substring(0, rawValue.Length - 1);
                        }

                        var newValue = databaseConnection.GetDatabaseConfiguration().ParseDataTypeValue(columnDataType, rawValue);
                        rowData[column] = newValue;
                    }

                    row = string.Empty;
                    foreach (var column in orderedTableColumns)
                    {
                        row = string.Format("{0},{1}", row, rowData[column.Split(':')[0]]);
                    }

                    tableData[i] = row.Substring(1);
                    affectedRows++;
                }
            }

            databaseConnection.GetDatabaseConfiguration().DatabaseStorageService.WriteAllLines(tableLocation, tableData);
            return new SqlQueryResult(affectedRows, true, "Query successfully executed", null);
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
