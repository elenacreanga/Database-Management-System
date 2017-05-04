﻿using System.Collections.Generic;
using DbManagementSystem.Core.Database;
using DbManagementSystem.Core.Query;
using DbManagementSystem.Core.Query.Executors;

namespace DbManagementSystem.UI
{
   public class IntegrationTestsRunner
    {
        private static readonly string ServerLocation = @"C:\Users\vasea\Desktop\DatabaseWorkspace";
        private readonly IDatabaseConfiguration databaseConfiguration;
        private readonly IQueryExecutor queryExecutor;

        public IntegrationTestsRunner()
        {
            this.databaseConfiguration = new DatabaseCofiguration(new DatabaseStorageService());
            this.queryExecutor = new QueryExecutor();
        }

        public IQueryResult CreateDatabase(string databaseName)
        {
            IDatabaseConnection databaseConnection = new DatabaseConnection(this.databaseConfiguration, ServerLocation);
            IQuery query = new SqlQuery("CREATE DATABASE :databaseName").SetParameter("databaseName", databaseName);
            return queryExecutor.Execute(databaseConnection, query);
        }

        public IQueryResult DeleteDatabase(string databaseName)
        {
            IDatabaseConnection databaseConnection = new DatabaseConnection(this.databaseConfiguration, ServerLocation);
            IQuery query = new SqlQuery("DELETE DATABASE :databaseName").SetParameter("databaseName", databaseName);
            return queryExecutor.Execute(databaseConnection, query);
        }

        public IQueryResult AlterDatabase(string databaseName, string newDatabaseName)
        {
            IDatabaseConnection databaseConnection = new DatabaseConnection(this.databaseConfiguration, ServerLocation);
            IQuery query = new SqlQuery("ALTER DATABASE :databaseName RENAME :newDatabaseName").SetParameter("databaseName", databaseName).SetParameter("newDatabaseName", newDatabaseName);
            return queryExecutor.Execute(databaseConnection, query);
        }

        public IQueryResult CreateTable(string databaseName, string tableName, string columns)
        {
            IDatabaseConnection databaseConnection = new DatabaseConnection(this.databaseConfiguration, ServerLocation, databaseName);
            IQuery query = new SqlQuery("CREATE TABLE :tableName (:columns)").SetParameter("tableName", tableName).SetParameter("columns", columns);
            return queryExecutor.Execute(databaseConnection, query);
        }

        public IQueryResult DeleteTable(string databaseName, string tableName)
        {
            IDatabaseConnection databaseConnection = new DatabaseConnection(this.databaseConfiguration, ServerLocation, databaseName);
            IQuery query = new SqlQuery("DELETE TABLE :tableName").SetParameter("tableName", tableName);
            return queryExecutor.Execute(databaseConnection, query);
        }

        public IQueryResult AlterTable(string databaseName, string tableName, string action, string columns)
        {
            IDatabaseConnection databaseConnection = new DatabaseConnection(this.databaseConfiguration, ServerLocation, databaseName);
            IQuery query = new SqlQuery("ALTER TABLE :tableName :action COLUMNS (:columns)").SetParameter("tableName", tableName).SetParameter("action", action).SetParameter("columns", columns);
            return queryExecutor.Execute(databaseConnection, query);
        }
        

        public IQueryResult ExecuteQuery(string rawQuery, string databaseName = null, string tableName = null)
        {
            IDatabaseConnection databaseConnection = new DatabaseConnection(this.databaseConfiguration, ServerLocation, databaseName);
            IQuery query = new SqlQuery(rawQuery);
            return queryExecutor.Execute(databaseConnection, query);
        }


        public IEnumerable<Database> GetDatabaseList()
        {
            IDatabaseConnection databaseConnection = new DatabaseConnection(this.databaseConfiguration, ServerLocation);
            DatabaseManager databaseManager = new DatabaseManager(databaseConnection);

            return databaseManager.GetDatabaseList();
        }

        public IEnumerable<Table> GetDatabaseTableList(string databaseName)
        {
            IDatabaseConnection databaseConnection = new DatabaseConnection(this.databaseConfiguration, ServerLocation);
            DatabaseManager databaseManager = new DatabaseManager(databaseConnection);
            return databaseManager.GetDatabaseTableList(databaseName);
        }

        public Table  GetTable(string databaseName, string tableName)
        {
            IDatabaseConnection databaseConnection = new DatabaseConnection(this.databaseConfiguration, ServerLocation);
            DatabaseManager databaseManager = new DatabaseManager(databaseConnection);
            return databaseManager.GetDatabaseTable(databaseName,tableName);
        }

        public IDatabaseConnection GetConn(string databaseName)
        {
            return  new DatabaseConnection(this.databaseConfiguration, ServerLocation, databaseName);
        }

    }
}
