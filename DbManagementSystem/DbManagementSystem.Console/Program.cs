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

            //result = runner.CreateTable("schoolDb", "students", "firstName:string,grade:int,height:int");
            //PrintResult(result);

            //result = runner.AlterTable("schoolDb", "students", "ADD", "lastName:string");
            //PrintResult(result);

            //result = runner.AlterTable("schoolDb", "students", "REMOVE", "height");
            //PrintResult(result);

            //result = runner.DeleteTable("schoolDb", "students");
            //PrintResult(result);

            result = runner.ExecuteQuery("CREATE TABLE students (Id:int,FirstName:string,LastName:string,Grade:int)", "schoolDb");
            PrintResult(result);

            result = runner.ExecuteQuery("CREATE TABLE objects (Name:string,Points:int)", "schoolDb");
            PrintResult(result);

            result = runner.ExecuteQuery("SELECT * FROM students WHERE Id>0 AND Id<2 OR Grade>9 AND Id>=2 AND FirstName!='Vasile' ", "schoolDb");
            PrintResult(result);

            System.Console.ReadKey();
        }

        private static void PrintResult(IQueryResult result)
        {
            if (result.Success)
            {
                System.Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine(string.Format("Rows Affected: {0}\nMessage: {1}", result.RowsAffected, result.Message));
                System.Console.ResetColor();
                PrintResultData(result);
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
                System.Console.Write(string.Format("{0}\t\t|", column));
            }

            System.Console.WriteLine();
            while (result.Read())
            {
                foreach (var column in columns)
                {
                    System.Console.Write(string.Format("{0}\t\t|", result.GetValue(column)));
                }

                System.Console.WriteLine();
            }
        }
    }
}
