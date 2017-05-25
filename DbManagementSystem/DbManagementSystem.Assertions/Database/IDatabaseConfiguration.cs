using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace DbManagementSystem.Assertions.Database
{
    [ContractClass(typeof(DatabaseCofigurationContract))]
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

    [ContractClassFor(typeof(IDatabaseConfiguration))]
    public class DatabaseCofigurationContract:IDatabaseConfiguration
    {
        public IDatabaseStorageService DatabaseStorageService { get; }

        public bool IsDataTypeAllowed(string dataType)
        {
            Contract.Requires(!string.IsNullOrEmpty(dataType));
            return default(bool);
        }

        public bool IsOperatorAllowedForDataType(string dataType, string op)
        {
            Contract.Requires(!string.IsNullOrEmpty(dataType));
            Contract.Requires(!string.IsNullOrEmpty(op));
            return default(bool);
        }

        public object GetDefaultValueForDataType(string dataType)
        {
            Contract.Requires(!string.IsNullOrEmpty(dataType));
            return default(object);
        }

        public List<string> GetAllowedOperators()
        {
            return default(List<string>);
        }

        public object ParseDataTypeValue(string dataType, string value)
        {
            Contract.Requires(!string.IsNullOrEmpty(dataType));
            Contract.Requires(!string.IsNullOrEmpty(value));
            return default(object);
        }

        public bool PerformOperation(string dataType, object leftOperand, object rightOperand, string op)
        {
            Contract.Requires(!string.IsNullOrEmpty(dataType));
            Contract.Requires(!string.IsNullOrEmpty(op));
            return default(bool);
        }
    }
}
