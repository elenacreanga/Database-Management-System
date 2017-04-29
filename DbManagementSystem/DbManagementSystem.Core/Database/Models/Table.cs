using System.Collections.Generic;

namespace DbManagementSystem.Core.Database
{
    public class Table
    {
        public string Name { get; set; }
        public double Size { get; set; }
        public string DatabaseName { get; set; }
        public Dictionary<string, string> Columns { get; set; }
    }
}
