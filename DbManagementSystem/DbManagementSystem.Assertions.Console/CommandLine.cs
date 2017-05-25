using DbManagementSystem.Assertions.Database;
using DbManagementSystem.Assertions.Query;
using DbManagementSystem.Assertions.Query.Executors;
using System;
using DbManagementSystem.Assertions;

namespace DbManagementSystem.Assertions.Console
{
    class CommandLine
    {
        private readonly IDatabaseConfiguration databaseConfiguration;
        private readonly IQueryExecutor queryExecutor;
        private IDatabaseConnection databaseConnection;
        private readonly string ServerLocation = Constants.ServerLocation;

        public CommandLine()
        {
            this.databaseConfiguration = new DatabaseCofiguration(new DatabaseStorageService());
            this.queryExecutor = new QueryExecutor();
            this.databaseConnection = new DatabaseConnection(this.databaseConfiguration, ServerLocation);
        }

        public void Run()
        {
            System.Console.WriteLine("---------Database Management System Console--------");
            System.Console.WriteLine("----Type a query or 'exit' to close the console----");
            System.Console.Write(">");
            var query = "";
            while (!(query = System.Console.ReadLine()).Equals("exit", System.StringComparison.OrdinalIgnoreCase))
            {
                if(query.StartsWith("USE ", System.StringComparison.OrdinalIgnoreCase))
                {
                    var databaseName = query.Substring(4).Trim();
                    this.databaseConnection = new DatabaseConnection(this.databaseConfiguration, ServerLocation, databaseName);
                }
                else
                {
                    IQuery sqlQuery = new SqlQuery(query);
                    var result = queryExecutor.Execute(databaseConnection, sqlQuery);
                    PrintResult(result);
                }

                System.Console.Write(">");
            }
        }

        private static void PrintResult(IQueryResult result)
        {
            if (result.Success)
            {
                System.Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine(string.Format("Rows Affected: {0}\nMessage: {1}", result.RowsAffected, result.Message));
                System.Console.ResetColor();
                PrintResultData(result);
                System.Console.WriteLine();
            }
            else
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine(string.Format("Rows Affected: {0}\nMessage: {1}", result.RowsAffected, result.Message));
                System.Console.ResetColor();
                System.Console.WriteLine();
            }
        }

        private static void PrintResultData(IQueryResult result)
        {
            var columns = result.GetColumnNames();
            foreach (var column in columns)
            {
                System.Console.Write(string.Format("|{0}|", column));
            }

            System.Console.WriteLine();
            while (result.Read())
            {
                foreach (var column in columns)
                {
                    System.Console.Write(string.Format("|{0}|", result.GetValue(column)));
                }

                System.Console.WriteLine();
            }
        }
    }
}
