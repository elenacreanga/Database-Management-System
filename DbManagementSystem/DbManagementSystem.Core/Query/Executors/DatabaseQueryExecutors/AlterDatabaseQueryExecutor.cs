using System;
using DbManagementSystem.Core.Database;
using System.Text.RegularExpressions;

namespace DbManagementSystem.Core.Query.Executors.DatabaseQueryExecutors
{
    public class AlterDatabaseQueryExecutor : IQueryExecutor
    {
        private static readonly string MATCH = @"^ALTER DATABASE (?<databaseName>\w+) RENAME (?<newDatabaseName>\w+)$";

        public IQueryResult Execute(IDatabaseConnection databaseConnection, IQuery query)
        {
            var sqlQuery = query.GetQuery();
            var match = Regex.Match(sqlQuery, MATCH, RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                return new SqlQueryResult(0, false, string.Format("Invalid query: --{0}--", sqlQuery), null);
            }

            var databaseName = match.Groups["databaseName"].Value;
            var newDatabaseName = match.Groups["newDatabaseName"].Value;
            var databaseLocation = databaseConnection.GetServerLocation() + "/" + databaseName;
            if (!databaseConnection.GetDatabaseConfiguration().DatabaseStorageService.ExistsDatabase(databaseLocation))
            {
                return new SqlQueryResult(0, false, string.Format("Database does not exist: --{0}--", sqlQuery), null);
            }

            var newDatabaseLocation = databaseConnection.GetServerLocation() + "/" + newDatabaseName;
            if (databaseConnection.GetDatabaseConfiguration().DatabaseStorageService.ExistsDatabase(newDatabaseLocation))
            {
                return new SqlQueryResult(0, false, string.Format("Database already exists: --{0}--", sqlQuery), null);
            }

            try
            {
                databaseConnection.GetDatabaseConfiguration().DatabaseStorageService.MoveDatabase(databaseLocation, newDatabaseLocation);
                return new SqlQueryResult(0, true, "Database successfully renamed", null);
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
