using System.Collections.Generic;

namespace DbManagementSystem.Core.Database
{
    public interface IDatabaseConfiguration
    {
        IDatabaseStorageService DatabaseStorageService { get; }
        bool IsDataTypeAllowed(string dataType);
        bool IsOperatorAllowedForDataType(string dataType, string op);
        object GetDefaultValueForDataType(string dataType);
        List<string> GetAllowedOperators();
        object ParseDataTypeValue(string dataType, string value);
        bool PerformOperation(string dataType, object leftOperand, object rightOperand, string op);
    }
}
