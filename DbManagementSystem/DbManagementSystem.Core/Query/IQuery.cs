namespace DbManagementSystem.Core.Query
{
    interface IQuery
    {
        IQuery SetParameter(string name, object value);
        IQueryResult Execute();
    }
}
