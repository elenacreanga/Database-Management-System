using System;
using DbManagementSystem.Assertions.Database;
using System.Text.RegularExpressions;

namespace DbManagementSystem.Assertions.Query.Executors.TableQueryExecutors
{
    public class DeleteTableQueryExecutor : IQueryExecutor
    {
        private static readonly string MATCH = @"^DELETE TABLE (?<tableName>\w+)$";

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

            try
            {
                databaseConnection.GetDatabaseConfiguration().DatabaseStorageService.DeleteTable(tableLocation);
                return new SqlQueryResult(0, true, "Table successfully deleted", null);
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
