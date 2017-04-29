using DbManagementSystem.Core.Query;
using System;

namespace DbManagementSystem.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("----Database Management System Console----");

            var runner = new IntegrationTestsRunner();

            var result = runner.CreateDatabase("school");
            PrintResult(result);

            result = runner.CreateDatabase("university");
            PrintResult(result);

            result = runner.DeleteDatabase("university");
            PrintResult(result);

            result = runner.AlterDatabase("school", "schoolDb");
            PrintResult(result);

            result = runner.CreateTable("schoolDb", "students", "firstName:string,grade:int,height:int");
            PrintResult(result);

            result = runner.AlterTable("schoolDb", "students", "ADD", "lastName:string");
            PrintResult(result);

            result = runner.AlterTable("schoolDb", "students", "REMOVE", "height");
            PrintResult(result);

            result = runner.DeleteTable("schoolDb", "students");
            PrintResult(result);

            result = runner.ExecuteQuery("CREATE TABLE teachers (firstName:string,lastName:string,height:int)", "schoolDb");
            PrintResult(result);

            result = runner.ExecuteQuery("CREATE TABLE objects (name:string,points:double)", "schoolDb");
            PrintResult(result);

            System.Console.ReadKey();
        }

        private static void PrintResult(IQueryResult result)
        {
            if(result.Success)
            {
                System.Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine(string.Format("Rows Affected: {0}\nMessage: {1}", result.RowsAffected, result.Message));
                System.Console.ResetColor();
            }
            else
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine(string.Format("Rows Affected: {0}\nMessage: {1}", result.RowsAffected, result.Message));
                System.Console.ResetColor();
            }
        }
    }
}
