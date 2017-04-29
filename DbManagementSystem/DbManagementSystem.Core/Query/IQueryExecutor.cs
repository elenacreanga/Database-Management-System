using DbManagementSystem.Core.Database;

namespace DbManagementSystem.Core.Query
{
    public interface IQueryExecutor
    {
        IQueryResult Execute(IDatabaseConnection databaseConnection, IQuery query);
        bool MatchesQuery(IQuery query);
    }
}
