using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace DbManagementSystem.Assertions.Database
{
    public class DatabaseCofiguration : IDatabaseConfiguration
    {
        private static readonly Dictionary<string, HashSet<string>> AllowedDataTypes;
        private static readonly Dictionary<string, Dictionary<string, Func<object, object, bool>>> OperationFunctions;
        private static readonly Dictionary<string, object> DefaultDataTypeValues;
        private static readonly Dictionary<string, Func<string, object>> DataTypeParsers;

        private readonly IDatabaseStorageService databaseStorageService;

        public IDatabaseStorageService DatabaseStorageService
        {
            get
            {
                return this.databaseStorageService;
            }
        }

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

            DataTypeParsers = new Dictionary<string, Func<string, object>>();
            DataTypeParsers["int"] = (value) =>
            {
                int result;
                if (int.TryParse(value, out result))
                {
                    return result;
                }
                else
                {
                    throw new Exception(string.Format("Invalid value for int: '{0}'", value));
                }
            };
            DataTypeParsers["string"] = (value) => { return string.IsNullOrEmpty(value) ? string.Empty : value.ToString(); };

            OperationFunctions = new Dictionary<string, Dictionary<string, Func<object, object, bool>>>();
            OperationFunctions["string"] = new Dictionary<string, Func<object, object, bool>>();
            OperationFunctions["string"]["="] = (l, r) => { return l.ToString() == r.ToString(); };
            OperationFunctions["string"]["!="] = (l, r) => { return l.ToString() != r.ToString(); };
            OperationFunctions["int"] = new Dictionary<string, Func<object, object, bool>>();
            OperationFunctions["int"]["="] = (l, r) => { return (int)l == (int)r; };
            OperationFunctions["int"]["!="] = (l, r) => { return (int)l != (int)r; };
            OperationFunctions["int"]["<"] = (l, r) => { return (int)l < (int)r; };
            OperationFunctions["int"]["<="] = (l, r) => { return (int)l <= (int)r; };
            OperationFunctions["int"][">"] = (l, r) => { return (int)l > (int)r; };
            OperationFunctions["int"][">="] = (l, r) => { return (int)l >= (int)r; };
        }

        public DatabaseCofiguration(IDatabaseStorageService databaseStorageService)
        {
            Contract.Ensures(this.databaseStorageService != null);
            this.databaseStorageService = databaseStorageService;
        }

        public bool IsDataTypeAllowed(string dataType)
        {
            return AllowedDataTypes.ContainsKey(dataType.ToLower());
        }

        public bool IsOperatorAllowedForDataType(string dataType, string op)
        {
            return AllowedDataTypes.ContainsKey(dataType.ToLower()) && AllowedDataTypes[dataType.ToLower()].Contains(op);
        }

        public object GetDefaultValueForDataType(string dataType)
        {
            object value;
            if (DefaultDataTypeValues.TryGetValue(dataType.ToLower(), out value))
            {
                return value;
            }

            return null;
        }

        public List<string> GetAllowedOperators()
        {
            var operators = new HashSet<string>();
            foreach (var dataType in AllowedDataTypes.Keys)
            {
                operators.UnionWith(AllowedDataTypes[dataType]);
            }

            return operators.OrderByDescending(o => o.Length).ToList();
        }

        public object ParseDataTypeValue(string dataType, string value)
        {
            return DataTypeParsers[dataType.ToLower()].Invoke(value);
        }

        public bool PerformOperation(string dataType, object leftOperand, object rightOperand, string op)
        {
            return OperationFunctions[dataType][op].Invoke(leftOperand, rightOperand);
        }
    }
}
