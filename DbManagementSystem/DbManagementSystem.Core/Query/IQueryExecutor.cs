using DbManagementSystem.Core.Database;

namespace DbManagementSystem.Core.Query
{
    interface IQueryExecutor
    {
        IQueryResult Execute(IDatabaseConnection databaseConnection, IQuery query);
        bool MatchesQuery(IQuery query);
    }
}
