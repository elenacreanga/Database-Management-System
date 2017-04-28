namespace DbManagementSystem.Core.Query
{
    interface IQueryResultSerializer
    {
        string Serialize(IQueryResult queryResult);
    }
}
