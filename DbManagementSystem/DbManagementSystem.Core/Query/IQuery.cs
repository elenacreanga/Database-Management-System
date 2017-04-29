namespace DbManagementSystem.Core.Query
{
    public interface IQuery
    {
        string GetQuery();
        IQuery SetParameter(string name, object value);
    }
}
