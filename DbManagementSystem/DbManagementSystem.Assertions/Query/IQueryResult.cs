namespace DbManagementSystem.Assertions.Query
{
    public interface IQueryResult
    {
        int RowsAffected { get; }
        bool Success { get; }
        string Message { get; }
        bool Read();
        object GetValue(string name);
        object GetValue(int index);
        string[] GetColumnNames();
    }
}
