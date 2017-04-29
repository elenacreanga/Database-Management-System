namespace DbManagementSystem.Core.Query
{
    public interface IQueryResultSerializer
    {
        string Serialize(IQueryResult queryResult);
    }
}
