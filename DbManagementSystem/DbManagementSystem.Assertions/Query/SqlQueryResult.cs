using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace DbManagementSystem.Assertions.Query
{
    public class SqlQueryResult : IQueryResult
    {
        private readonly List<Dictionary<string, object>> result;

        private int readIndex;

        public int RowsAffected { get; }

        public bool Success { get; }

        public string Message { get; }

        public SqlQueryResult(int rowsAffected, bool success, string message, List<Dictionary<string, object>> result)
        {

            this.RowsAffected = rowsAffected;
            this.Success = success;
            this.Message = message;
            this.result = result;
            this.readIndex = -1;
        }

        public object GetValue(string name)
        {
            Contract.Requires(!string.IsNullOrEmpty(name));

            if (this.result != null && this.result.Count > 0 && this.readIndex < this.result.Count)
            {
                object value;
                this.result[readIndex].TryGetValue(name, out value);
                return value;
            }

            return null;
        }

        public object GetValue(int index)
        {

            if (this.result != null && this.result.Count > 0 && this.readIndex < this.result.Count)
            {
                return this.result[readIndex].ElementAtOrDefault(index).Value;
            }

            return null;
        }

        public bool Read()
        {
            if (this.result != null && this.result.Count > 0 && this.readIndex < this.result.Count - 1)
            {
                this.readIndex++;
                return true;
            }

            return false;
        }

        public string[] GetColumnNames()
        {
            if (this.result != null && this.result.Any())
            {
                return this.result.FirstOrDefault().Keys.ToArray();
            }

            return new string[] { };
        }
    }
}
