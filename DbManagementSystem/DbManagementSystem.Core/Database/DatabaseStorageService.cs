using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DbManagementSystem.Core.Database
{
    public class DatabaseStorageService : IDatabaseStorageService
    {
        public void AppendAllText(string path, string contents)
        {
            File.AppendAllText(path, contents);
        }

        public void CreateDatabase(string path)
        {
            Directory.CreateDirectory(path);
        }

        public void DeleteTable(string path)
        {
            File.Delete(path);
        }

        public void DeleteDatabase(string path)
        {
            Directory.Delete(path, true);
        }

        public IEnumerable<string> EnumerateTables(string path)
        {
            return Directory.EnumerateFiles(path);
        }

        public bool ExistsDatabase(string path)
        {
            return Directory.Exists(path);
        }

        public bool ExistsTable(string path)
        {
            return File.Exists(path);
        }

        public IEnumerable<string> GetDatabases(string serverLocation)
        {
            return new DirectoryInfo(serverLocation).EnumerateDirectories().Select(d => d.Name);
        }

        public void MoveDatabase(string fromPath, string toPath)
        {
            Directory.Move(fromPath, toPath);
        }

        public string[] ReadAllLines(string path)
        {
            return File.ReadAllLines(path);
        }

        public IEnumerable<string> ReadLines(string path)
        {
            return File.ReadLines(path);
        }

        public void WriteAllText(string path, string contents)
        {
            File.WriteAllText(path, contents);
        }

        public void WriteAllLines(string path, string[] contents)
        {
            File.WriteAllLines(path, contents);
        }

        public double GetTableSize(string tableLocation)
        {
            return new FileInfo(tableLocation).Length / 1024.0d;
        }
    }
}
