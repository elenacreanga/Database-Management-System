namespace DbManagementSystem.Core.Query
{
    interface IQuery
    {
        string GetQuery();
        IQuery SetParameter(string name, object value);
    }
}
