﻿using System;
using DbManagementSystem.Core.Database;

namespace DbManagementSystem.Core.Query.Executors.TableDataQueryExecutors
{
    public class SelectQueryExecutor : IQueryExecutor
    {
        public IQueryResult Execute(IDatabaseConnection databaseConnection, IQuery query)
        {
            throw new NotImplementedException();
        }

        public bool MatchesQuery(IQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
