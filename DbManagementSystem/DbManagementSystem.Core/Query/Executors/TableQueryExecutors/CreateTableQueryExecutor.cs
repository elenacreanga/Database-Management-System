using System;
using DbManagementSystem.Core.Database;
using System.Text.RegularExpressions;
using System.Linq;

namespace DbManagementSystem.Core.Query.Executors.TableQueryExecutors
{
    public class CreateTableQueryExecutor : IQueryExecutor
    {
        private static readonly string MATCH = @"^CREATE TABLE (?<tableName>\w+) \((?<columns>[\s\S]+)\)$";

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
            if (databaseConnection.GetDatabaseConfiguration().DatabaseStorageService.ExistsTable(tableLocation))
            {
                return new SqlQueryResult(0, false, string.Format("Table already exists: --{0}--", sqlQuery), null);
            }

            var rawColumns = match.Groups["columns"].Value;
            var columns = rawColumns.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if(!columns.Any())
            {
                return new SqlQueryResult(0, false, string.Format("Invalid columns: --{0}--", rawColumns), null);
            }

            foreach (var column in columns)
            {
                var columnMetadata = column.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (columnMetadata.Length != 2 || string.IsNullOrWhiteSpace(columnMetadata[0]) || string.IsNullOrWhiteSpace(columnMetadata[0]) 
                    || !databaseConnection.GetDatabaseConfiguration().IsDataTypeAllowed(columnMetadata[1]))
                {
                    return new SqlQueryResult(0, false, string.Format("Invalid column: --{0}--", column), null);
                }
            }

            try
            {
                databaseConnection.GetDatabaseConfiguration().DatabaseStorageService.WriteAllText(tableLocation, string.Format("{0}\n", rawColumns));
                return new SqlQueryResult(0, true, "Table successfully created", null);
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
