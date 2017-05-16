using System;
using DbManagementSystem.Core.Database;
using System.Text.RegularExpressions;

namespace DbManagementSystem.Core.Query.Executors.DatabaseQueryExecutors
{
    public class CreateDatabaseQueryExecutor : IQueryExecutor
    {
        private static readonly string MATCH = @"^CREATE DATABASE (?<databaseName>\w+)$";

        public IQueryResult Execute(IDatabaseConnection databaseConnection, IQuery query)
        {
            var sqlQuery = query.GetQuery();
            var match = Regex.Match(sqlQuery, MATCH, RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                return new SqlQueryResult(0, false, string.Format("Invalid query: --{0}--", sqlQuery), null);
            }

            var databaseName = match.Groups["databaseName"].Value;
            var databaseLocation = databaseConnection.GetServerLocation() + "/" + databaseName;
            if (databaseConnection.GetDatabaseConfiguration().DatabaseStorageService.ExistsDatabase(databaseLocation))
            {
                return new SqlQueryResult(0, false, string.Format("Database already exists: --{0}--", sqlQuery), null);
            }

            try
            {
                databaseConnection.GetDatabaseConfiguration().DatabaseStorageService.CreateDatabase(databaseLocation);
                return new SqlQueryResult(0, true, "Database successfully created", null);
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
