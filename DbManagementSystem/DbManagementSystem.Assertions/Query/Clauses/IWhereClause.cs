using System.Collections.Generic;

namespace DbManagementSystem.Assertions.Query.Clauses
{
    public interface IWhereClause
    {
        bool MatchesRow(Dictionary<string, object> row);
    }
}
