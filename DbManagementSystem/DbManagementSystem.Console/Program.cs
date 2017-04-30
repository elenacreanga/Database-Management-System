using DbManagementSystem.Core.Query;
using System;
using System.Collections.Generic;

namespace DbManagementSystem.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("----Database Management System Console----");

            var runner = new IntegrationTestsRunner();

            var result = runner.CreateDatabase("schoolDb");
            PrintResult(result);

            result = runner.ExecuteQuery("CREATE TABLE students (Id:int,FirstName:string,LastName:string,Grade:int)", "schoolDb");
            PrintResult(result);

            result = runner.ExecuteQuery("CREATE TABLE objects (Name:string,Points:int)", "schoolDb");
            PrintResult(result);

            var firstNames = new List<string>()
            {
                "Vasile","Ion","Gheorghe","Nicolai","Elena","Stefan","Alexandra", "Mihai", "Maria", "Ana"
            };

            var lastNames = new List<string>()
            {
                "Pojoga","Munteanu","Herta","Ailenei","Creanga","Stan","Budescu", "Apostol", "Mironica", "Velea"
            };

            var grades = new List<int>()
            {
                9,4,8,7,10,9,3,2,8,10
            };

            for (var i = 0; i < 10; i++)
            {
                result = runner.ExecuteQuery(string.Format("INSERT INTO students(Id,FirstName,LastName,Grade) VALUES({0},'{1}','{2}',{3})", i + 1, firstNames[i], lastNames[i], grades[i]), "schoolDb");
                PrintResult(result);
            }

            result = runner.ExecuteQuery("SELECT * FROM students", "schoolDb");
            PrintResult(result);

            result = runner.ExecuteQuery("SELECT * FROM students WHERE Id>0 AND Id<2 OR Grade>9 AND Id>=2 AND FirstName!='Vasile'", "schoolDb");
            PrintResult(result);

            result = runner.ExecuteQuery("DELETE FROM students WHERE Id=1 OR Id>=8", "schoolDb");
            PrintResult(result);

            result = runner.ExecuteQuery("SELECT * FROM students", "schoolDb");
            PrintResult(result);

            result = runner.ExecuteQuery("UPDATE students SET Grade=5 WHERE Grade<5", "schoolDb");
            PrintResult(result);

            result = runner.ExecuteQuery("SELECT * FROM students", "schoolDb");
            PrintResult(result);

            result = runner.ExecuteQuery("UPDATE students SET Grade=5,LastName='Failed' WHERE Grade<5 OR FirstName='Vasile'", "schoolDb");
            PrintResult(result);

            result = runner.ExecuteQuery("SELECT * FROM students", "schoolDb");
            PrintResult(result);

            result = runner.ExecuteQuery("DELETE FROM students", "schoolDb");
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
