using System;
using DbManagementSystem.Assertions.Database;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using DbManagementSystem.Assertions.Query.Executors.DatabaseQueryExecutors;
using DbManagementSystem.Assertions.Query.Executors.TableQueryExecutors;
using DbManagementSystem.Assertions.Query.Executors.TableDataQueryExecutors;

namespace DbManagementSystem.Assertions.Query.Executors
{
    public class QueryExecutor : IQueryExecutor
    {
        private readonly List<IQueryExecutor> queryExecutors;

        public QueryExecutor(List<IQueryExecutor> queryExecutors)
        {
            Contract.Ensures(this.queryExecutors != null);

            this.queryExecutors = queryExecutors;
        }

        public QueryExecutor()
        {
            Contract.Ensures(this.queryExecutors != null);

            this.queryExecutors = new List<IQueryExecutor>();
            InitializeExecutors();
        }

        private void InitializeExecutors()
        {
            this.queryExecutors.Add(new AlterDatabaseQueryExecutor());
            this.queryExecutors.Add(new CreateDatabaseQueryExecutor());
            this.queryExecutors.Add(new DeleteDatabaseQueryExecutor());
            this.queryExecutors.Add(new AlterTableQueryExecutor());
            this.queryExecutors.Add(new CreateTableQueryExecutor());
            this.queryExecutors.Add(new DeleteTableQueryExecutor());
            this.queryExecutors.Add(new DeleteQueryExecutor());
            this.queryExecutors.Add(new InsertQueryExecutor());
            this.queryExecutors.Add(new SelectQueryExecutor());
            this.queryExecutors.Add(new UpdateQueryExecutor());
        }

        public IQueryResult Execute(IDatabaseConnection databaseConnection, IQuery query)
        {
            if (IsValidDatabaseConnection(databaseConnection))
            {
                foreach (var queryExecutor in this.queryExecutors)
                {
                    if (queryExecutor.MatchesQuery(query))
                    {
                        try
                        {
                            return queryExecutor.Execute(databaseConnection, query);
                        }
                        catch (Exception exception)
                        {
                            return new SqlQueryResult(0, false, string.Format("An error occured: --{0}--", exception.Message), null);
                        }
                    }
                }
            }
            else
            {
                return new SqlQueryResult(0, false, "Invalid database connection!", null);
            }

            return new SqlQueryResult(0, false, "Invalid query!", null);
        }

        private bool IsValidDatabaseConnection(IDatabaseConnection databaseConnection)
        {
            if (string.IsNullOrWhiteSpace(databaseConnection.GetServerLocation())
                || !databaseConnection.GetDatabaseConfiguration().DatabaseStorageService.ExistsDatabase(databaseConnection.GetServerLocation()))
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(databaseConnection.GetDatabaseName())
                && !databaseConnection.GetDatabaseConfiguration().DatabaseStorageService.ExistsDatabase(databaseConnection.GetServerLocation() + "/" + databaseConnection.GetDatabaseName()))
            {
                return false;
            }

            return true;
        }

        public bool MatchesQuery(IQuery query)
        {
            return true;
        }
    }
}
