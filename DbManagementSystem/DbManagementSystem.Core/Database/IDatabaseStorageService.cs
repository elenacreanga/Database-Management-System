using System.Collections.Generic;

namespace DbManagementSystem.Core.Database
{
    public interface IDatabaseStorageService
    {
        bool ExistsTable(string path);
        IEnumerable<string> ReadLines(string path);
        string[] ReadAllLines(string path);
        void WriteAllText(string path, string contents);
        void WriteAllLines(string path, string[] contents);
        void AppendAllText(string path, string contents);
        void DeleteTable(string path);
        bool ExistsDatabase(string path);
        IEnumerable<string> GetDatabases(string serverLocation);
        IEnumerable<string> EnumerateTables(string path);
        void MoveDatabase(string fromPath, string toPath);
        void CreateDatabase(string path);
        void DeleteDatabase(string path);
    }
}
