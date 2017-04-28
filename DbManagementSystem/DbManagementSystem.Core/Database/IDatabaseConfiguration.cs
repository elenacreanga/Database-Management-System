namespace DbManagementSystem.Core.Database
{
    interface IDatabaseConfiguration
    {
        bool IsDataTypeAllowed(string dataType);
        bool IsOperatorAllowedForDataType(string dataType, string op);
        object GetDefaultValueForDataType(string dataType);
    }
}
