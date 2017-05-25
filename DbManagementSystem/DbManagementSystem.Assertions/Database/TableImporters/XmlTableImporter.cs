using DbManagementSystem.Assertions.Query;
using DbManagementSystem.Assertions.Query.Executors;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text.RegularExpressions;

namespace DbManagementSystem.Assertions.Database.TableImporters
{
    public class XmlTableImporter : ITableImporter
    {
        public bool Import(IDatabaseConnection databaseConnection, string tableName, string data)
        {
            Contract.Requires(!string.IsNullOrEmpty(tableName));
            Contract.Requires(!string.IsNullOrEmpty(data));
            Contract.Requires(databaseConnection != null);

            var rows = data.Replace("<Data>", string.Empty)
                .Replace("</Data>", string.Empty)
                .Split(new[] { "<Row>" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(r => r.Replace("</Row>", string.Empty)).ToList();
            if (rows.Count < 1)
            {
                return false;
            }

            for (var i = 0; i < rows.Count; i++)
            {
                var columnNames = new List<string>();
                var columnValues = new List<string>();
                var columns = rows[i].Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var column in columns)
                {
                    var match = Regex.Match(column, @"\<(?<columnName>\w+)\>(?<columnValue>[\s\S]+)\<\/(?<closingColumnName>\w+)\>", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        var columnName = match.Groups["columnName"].Value;
                        var closingColumnName = match.Groups["closingColumnName"].Value;
                        var columnValue = match.Groups["columnValue"].Value;
                        if (columnName.Equals(closingColumnName))
                        {
                            columnNames.Add(columnName);
                            columnValues.Add(columnValue);
                        }
                    }
                }

                if (columnNames.Count > 0)
                {
                    var rawQuery = string.Format("INSERT INTO {0}({1}) VALUES({2})", tableName, string.Join(",", columnNames), string.Join(",", columnValues));
                    IQuery query = new SqlQuery(rawQuery);
                    IQueryExecutor queryExecutor = new QueryExecutor();
                    queryExecutor.Execute(databaseConnection, query);
                }
            }

            return true;
        }
    }
}
