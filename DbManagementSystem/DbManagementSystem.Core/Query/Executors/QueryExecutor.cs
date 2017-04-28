using System;
using DbManagementSystem.Core.Database;
using System.Collections.Generic;
using DbManagementSystem.Core.Query.Executors.DatabaseQueryExecutors;
using DbManagementSystem.Core.Query.Executors.TableQueryExecutors;
using DbManagementSystem.Core.Query.Executors.TableDataQueryExecutors;
using System.IO;

namespace DbManagementSystem.Core.Query.Executors
{
    class QueryExecutor : IQueryExecutor
    {
        private readonly List<IQueryExecutor> queryExecutors;

        public QueryExecutor(List<IQueryExecutor> queryExecutors)
        {
            this.queryExecutors = queryExecutors;
        }

        public QueryExecutor()
        {
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
                        catch(Exception exception)
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
                || !Directory.Exists(databaseConnection.GetServerLocation()))
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(databaseConnection.GetDatabaseName())
                && !Directory.Exists(databaseConnection.GetServerLocation() + "/" + databaseConnection.GetDatabaseName()))
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(databaseConnection.GetDatabaseName())
                && !string.IsNullOrWhiteSpace(databaseConnection.GetTableName())
                && !File.Exists(databaseConnection.GetServerLocation() + "/" + databaseConnection.GetDatabaseName() + "/" + databaseConnection.GetTableName()))
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
