namespace DbManagementSystem.Core.Database
{
    public interface IDatabaseConfiguration
    {
        bool IsDataTypeAllowed(string dataType);
        bool IsOperatorAllowedForDataType(string dataType, string op);
        object GetDefaultValueForDataType(string dataType);
    }
}
