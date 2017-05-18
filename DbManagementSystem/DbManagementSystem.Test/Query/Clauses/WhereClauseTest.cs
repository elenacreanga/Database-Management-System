using System.Collections.Generic;
using DbManagementSystem.Core.Database;
using DbManagementSystem.Core.Query.Clauses;
using NSubstitute;
using Xunit;

namespace DbManagementSystem.Test.Query.Clauses
{
    public class WhereClauseTest
    {
        [Fact]
        public void Build_WhenValidOperatosForDataTypes_ShouldSucceed()
        {
            var databaseConfiguration = Substitute.For<IDatabaseConfiguration>();
            var whereClause = GetSUT(databaseConfiguration);

            var whereClauseString = "DELETE FROM students WHERE Id=1 OR Id>=8";
            var tableColumns = new Dictionary<string, string>();
            tableColumns.Add("Id", "int");
            tableColumns.Add("Name", "string");

            var allowedDataTypes = new List<string> { "=", ">=" };
            databaseConfiguration.GetAllowedOperators().Returns(allowedDataTypes);

            var result = whereClause.Build("Id=1 OR Id>=8", tableColumns);
            Assert.True(result);
        }

        [Fact]
        public void Build_WhenInvalidOperatosForDataTypes_ShouldFail()
        {
            var databaseConfiguration = Substitute.For<IDatabaseConfiguration>();
            var whereClause = GetSUT(databaseConfiguration);

            var whereClauseString = "DELETE FROM students WHERE Id=1 OR Id>=8";
            var tableColumns = new Dictionary<string, string>();
            tableColumns.Add("Id", "int");
            tableColumns.Add("Name", "string");

            var allowedDataTypes = new List<string> { "!=", "<" };
            databaseConfiguration.GetAllowedOperators().Returns(allowedDataTypes);

            var result = whereClause.Build("Id=1 OR Id>=8", tableColumns);
            Assert.False(result);
        }

        [Fact]
        public void Match_WhenValidOperatosForDataTypes_ShouldSucceed()
        {
            var databaseConfiguration = Substitute.For<IDatabaseConfiguration>();
            var whereClause = GetSUT(databaseConfiguration);

            var tableColumns = new Dictionary<string, string>();
            tableColumns.Add("Id", "int");
            tableColumns.Add("Name", "string");

            var allowedDataTypes = new List<string> { "=" };
            databaseConfiguration.GetAllowedOperators().Returns(allowedDataTypes);
            databaseConfiguration.IsOperatorAllowedForDataType("int", "=").Returns(true);
            databaseConfiguration.ParseDataTypeValue("int", "1").Returns(1);
            databaseConfiguration.PerformOperation(Arg.Any<string>(), Arg.Any<object>(), Arg.Any<object>(), Arg.Any<string>()).Returns(true);

            var isSuccessfulBuild = whereClause.Build("Id=1", tableColumns);
            Assert.True(isSuccessfulBuild);
            var tableRow = new Dictionary<string, object>();
            tableRow.Add("Id", 1);
            tableRow.Add("Name", "Gigel");
            var result = whereClause.MatchesRow(tableRow);
            Assert.True(result);
        }

        private WhereClause GetSUT(IDatabaseConfiguration databaseConfiguration)
        {
            return new WhereClause(databaseConfiguration);
        }
    }
}
