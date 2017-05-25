using DbManagementSystem.Assertions.Database;
using DbManagementSystem.Assertions.Database.TableImporters;
using DbManagementSystem.Assertions.Query;
using DbManagementSystem.Assertions.Query.QueryResultSerializers;
using System;
using System.Collections.Generic;
using DbManagementSystem.Assertions;

namespace DbManagementSystem.Assertions.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            AddMockData();
          /*  var commandLine = new CommandLine();
            commandLine.Run();*/
        }

        private static void AddMockData()
        {
            var runner = new IntegrationTestsRunner();
            runner.DeleteDatabase("schoolDb");
            runner.CreateDatabase("schoolDb");
            runner.ExecuteQuery("CREATE TABLE students (Id:int,FirstName:string,LastName:string,Grade:int)", "schoolDb");
            runner.ExecuteQuery("CREATE TABLE objects (Id:int,Name:string,TeacherName:string,Language:string)", "schoolDb");
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
                runner.ExecuteQuery(string.Format("INSERT INTO students(Id,FirstName,LastName,Grade) VALUES({0},'{1}','{2}',{3})", i + 1, firstNames[i], lastNames[i], grades[i]), "schoolDb");
            }

            var objectNames = new List<string>()
            {
                "Math","Computer Science","History","Business","Biology","Chemistry","Phisics", "Literatre", "Algorithms", "Sport"
            };

            var teacherName = new List<string>()
            {
                "Ion Neculce","Ion Creanga","Herta Gheorghe","Tudorel Toader","Dorel Lucanu","Vlad Radulescu","Valeriu Baltag", "Apostol Sergiu", "Mironica Maria", "Velea Alex"
            };

            var language = new List<string>()
            {
                "English","Romanian","Russia","French","English","Romanian","German","Spanish","English","Romanian"
            };

            for (var i = 0; i < 10; i++)
            {
                runner.ExecuteQuery(string.Format("INSERT INTO objects(Id,Name,TeacherName,Language) VALUES({0},'{1}','{2}','{3}')", i + 1, objectNames[i], teacherName[i], language[i]), "schoolDb");
            }
        }

        private static void Test()
        {
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

            result = runner.ExecuteQuery("SELECT * FROM students", "schoolDb");
            IQueryResultSerializer serializer = new CsvQueryResultSerializer();
            var csv = serializer.Serialize(result);
            System.Console.WriteLine(csv);

            result = runner.ExecuteQuery("SELECT * FROM students", "schoolDb");
            serializer = new XmlQueryResultSerializer();
            var xml = serializer.Serialize(result);
            System.Console.WriteLine(xml);

            result = runner.ExecuteQuery("DELETE FROM students", "schoolDb");
            PrintResult(result);

            var databaseConnection = new DatabaseConnection(new DatabaseCofiguration(new DatabaseStorageService()), Constants.ServerLocation, "schoolDb");

            ITableImporter importer = new CsvTableImporter();
            var success = importer.Import(databaseConnection, "students", csv);
            System.Console.WriteLine(string.Format("Import from csv: {0}", success));

            importer = new XmlTableImporter();
            success = importer.Import(databaseConnection, "students", xml);
            System.Console.WriteLine(string.Format("Import from xml: {0}", success));

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
