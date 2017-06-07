using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace DbManagementSystem.Assertions.Database
{

    [ContractClass(typeof(DatabaseStorageServiceContract))]
    public interface IDatabaseStorageService
    {
        bool ExistsTable(string path);
        IEnumerable<string> ReadLines(string path);
        string[] ReadAllLines(string path);
        void WriteAllText(string path, string contents);
        void WriteAllLines(string path, string[] contents);
        void AppendAllText(string path, string contents);
        void DeleteTable(string path);
        double GetTableSize(string tableLocation);
        bool ExistsDatabase(string path);
        IEnumerable<string> GetDatabases(string serverLocation);
        IEnumerable<string> EnumerateTables(string path);
        void MoveDatabase(string fromPath, string toPath);
        void CreateDatabase(string path);
        void DeleteDatabase(string path);
    }

    [ContractClassFor(typeof(IDatabaseStorageService))]
    public class DatabaseStorageServiceContract: IDatabaseStorageService
    {
        public bool ExistsTable(string path)
        {
            Contract.Requires(!string.IsNullOrEmpty(path));
            return default(bool);
        }

        public IEnumerable<string> ReadLines(string path)
        {
            Contract.Requires(!string.IsNullOrEmpty(path));
            return default(IEnumerable<string>);
        }

        public string[] ReadAllLines(string path)
        {
            Contract.Requires(!string.IsNullOrEmpty(path));
            return default(string[]);
        }

        public void WriteAllText(string path, string contents)
        {
            Contract.Requires(!string.IsNullOrEmpty(path));
            Contract.Requires(!string.IsNullOrEmpty(contents));
        }

        public void WriteAllLines(string path, string[] contents)
        {
            Contract.Requires(!string.IsNullOrEmpty(path));
            Contract.Requires(Contract.ForAll(contents, x => !string.IsNullOrEmpty(x)));
        }

        public void AppendAllText(string path, string contents)
        {
            Contract.Requires(!string.IsNullOrEmpty(path));
            Contract.Requires(!string.IsNullOrEmpty(contents));
        }

        public void DeleteTable(string path)
        {
            Contract.Requires(!string.IsNullOrEmpty(path));
        }

        public double GetTableSize(string tableLocation)
        {
            Contract.Requires(!string.IsNullOrEmpty(tableLocation));
            return default(double);
        }

        public bool ExistsDatabase(string path)
        {
            Contract.Requires(!string.IsNullOrEmpty(path));
            return default(bool);
        }

        public IEnumerable<string> GetDatabases(string serverLocation)
        {
            Contract.Requires(!string.IsNullOrEmpty(serverLocation));
            return default(IEnumerable<string>);
        }

        public IEnumerable<string> EnumerateTables(string path)
        {
            Contract.Requires(!string.IsNullOrEmpty(path));
            return default(IEnumerable<string>);
        }

        public void MoveDatabase(string fromPath, string toPath)
        {
            Contract.Requires(!string.IsNullOrEmpty(fromPath));
            Contract.Requires(!string.IsNullOrEmpty(toPath));
        }

        public void CreateDatabase(string path)
        {
            Contract.Requires(!string.IsNullOrEmpty(path));
        }

        public void DeleteDatabase(string path)
        {
            Contract.Requires(!string.IsNullOrEmpty(path));
        }
    }
}
