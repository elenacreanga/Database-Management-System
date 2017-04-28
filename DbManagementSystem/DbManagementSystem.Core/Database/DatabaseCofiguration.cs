using System.Collections.Generic;

namespace DbManagementSystem.Core.Database
{
    class DatabaseCofiguration : IDatabaseConfiguration
    {
        private static readonly Dictionary<string, HashSet<string>> AllowedDataTypes;
        private static readonly Dictionary<string, object> DefaultDataTypeValues;

        static DatabaseCofiguration()
        {
            AllowedDataTypes = new Dictionary<string, HashSet<string>>();
            AllowedDataTypes["int"] = new HashSet<string>()
            {
                "=", "<", "<=", ">", ">=", "!="
            };

            AllowedDataTypes["string"] = new HashSet<string>()
            {
                "=", "!="
            };

            DefaultDataTypeValues = new Dictionary<string, object>();
            DefaultDataTypeValues["int"] = 0;
            DefaultDataTypeValues["string"] = string.Empty;
        }

        public bool IsDataTypeAllowed(string dataType)
        {
            return AllowedDataTypes.ContainsKey(dataType);
        }

        public bool IsOperatorAllowedForDataType(string dataType, string op)
        {
            return AllowedDataTypes.ContainsKey(dataType) && AllowedDataTypes[dataType].Contains(op);
        }

        public object GetDefaultValueForDataType(string dataType)
        {
            if (DefaultDataTypeValues.TryGetValue(dataType, out object value))
            {
                return value;
            }

            return null;
        }
    }
}
