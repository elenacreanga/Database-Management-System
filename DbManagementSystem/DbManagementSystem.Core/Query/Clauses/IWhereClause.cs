using System.Collections.Generic;

namespace DbManagementSystem.Core.Query.Clauses
{
    public interface IWhereClause
    {
        bool MatchesRow(Dictionary<string, object> row);
    }
}
