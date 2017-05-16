using System;
using DbManagementSystem.Core.Database;
using System.Text.RegularExpressions;
using System.Linq;

namespace DbManagementSystem.Core.Query.Executors.TableQueryExecutors
{
    public class AlterTableQueryExecutor : IQueryExecutor
    {
        private static readonly string MATCH = @"^ALTER TABLE (?<tableName>\w+) (?<action>(ADD|REMOVE)) COLUMNS \((?<columns>[\s\S]+)\)$";

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

            var rawColumns = match.Groups["columns"].Value;
            var columns = rawColumns.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (!columns.Any())
            {
                return new SqlQueryResult(0, false, string.Format("Invalid columns: --{0}--", rawColumns), null);
            }

            var tableData = databaseConnection.GetDatabaseConfiguration().DatabaseStorageService.ReadAllLines(tableLocation);
            var orderedTableColumns = tableData.FirstOrDefault().Split(',').ToList();
            var tableColumns = orderedTableColumns.ToDictionary(k => k.Split(':')[0], v => v.Split(':')[1]);
            var action = match.Groups["action"].Value;

            if (action.Equals("ADD", StringComparison.OrdinalIgnoreCase))
            {
                var affectedColumns = columns.ToDictionary(k => k.Split(':')[0], v => v.Split(':')[1]);
                foreach (var columnName in affectedColumns.Keys)
                {
                    if (string.IsNullOrWhiteSpace(columnName) || string.IsNullOrWhiteSpace(affectedColumns[columnName])
                        || !databaseConnection.GetDatabaseConfiguration().IsDataTypeAllowed(affectedColumns[columnName]))
                    {
                        return new SqlQueryResult(0, false, string.Format("Invalid column: --{0}:{1}--", columnName, affectedColumns[columnName]), null);
                    }

                    if (tableColumns.ContainsKey(columnName))
                    {
                        return new SqlQueryResult(0, false, string.Format("Column already exists: --{0}--", columnName), null);
                    }
                }

                foreach (var columnName in affectedColumns.Keys)
                {
                    orderedTableColumns.Add(string.Format("{0}:{1}", columnName, affectedColumns[columnName]));
                    for (var rowNumber = 1; rowNumber < tableData.Length; rowNumber++)
                    {
                        tableData[rowNumber] = string.Format("{0},{1}", tableData[rowNumber], databaseConnection.GetDatabaseConfiguration().GetDefaultValueForDataType(affectedColumns[columnName].ToString()));
                    }
                }

                tableData[0] = string.Join(",", orderedTableColumns);
            }
            else
            {
                var affectedColumns = columns;
                foreach (var columnName in affectedColumns)
                {
                    if (!tableColumns.ContainsKey(columnName))
                    {
                        return new SqlQueryResult(0, false, string.Format("Column does not exist: --{0}--", columnName), null);
                    }
                }

                foreach (var columnName in affectedColumns)
                {
                    var columnIndex = orderedTableColumns.IndexOf(string.Format("{0}:{1}", columnName, tableColumns[columnName]));
                    orderedTableColumns.RemoveAt(columnIndex);
                    for (var rowNumber = 1; rowNumber < tableData.Length; rowNumber++)
                    {
                        var parts = tableData[rowNumber].Split(',').ToList();
                        parts.RemoveAt(columnIndex);
                        tableData[rowNumber] = string.Join(",", parts);
                    }
                }

                tableData[0] = string.Join(",", orderedTableColumns);
            }

            try
            {
                databaseConnection.GetDatabaseConfiguration().DatabaseStorageService.WriteAllLines(tableLocation, tableData);
                return new SqlQueryResult(0, true, "Table successfully modified", null);
            }
            catch (Exception exception)
            {
                return new SqlQueryResult(0, false, string.Format("An error occured: --{0}--", exception.Message), null);
            }
        }

        public bool MatchesQuery(IQuery query)
        {
            return Regex.Match(query.GetQuery(), MATCH, RegexOptions.IgnoreCase).Success;
        }
    }
}
