using System;
using System.Collections.Generic;
using DbManagementSystem.Core.Query;
using Xunit;

namespace DbManagementSystem.Test.Query
{
    public class SQLQueryResultTest
    {
        [Fact]
        public void GetColumnNames_WhenNoColumns_ShouldReturnEmpty()
        {
            var sqlQueryResultTest = GetSUT("Success");
            var result = sqlQueryResultTest.GetColumnNames();
            Assert.Empty(result);
        }

        [Fact]
        public void GetColumnNames_WhenColumnsExist_ShouldReturnTheirNames()
        {
            const string success = "Success";
            var row = new Dictionary<string, object>();
            const string column1 = "column1";
            row.Add(column1, "value1");
            const string column2 = "column2";
            row.Add(column2, "value2");
            var resultList = new List<Dictionary<string, object>>(){ row };
            var sqlQueryResultTest = GetSUT(success, resultList);
            var result = sqlQueryResultTest.GetColumnNames();
            Assert.Equal(new []{column1, column2}, result);
        }

        [Fact]
        public void GetValue_WhenNoReadCalledBeforehand_ShouldThrowException()
        {
            const string success = "Success";
            var row = new Dictionary<string, object>();
            const string column1 = "column1";
            row.Add(column1, "value1");
            const string column2 = "column2";
            row.Add(column2, "value2");
            var resultList = new List<Dictionary<string, object>>(){ row };
            var sqlQueryResultTest = GetSUT(success, resultList);
            Assert.ThrowsAny<ArgumentOutOfRangeException>(() => sqlQueryResultTest.GetValue(column1));
        }

        [Fact]
        public void Read_WhenColumnsExist_ShouldReturnTrue()
        {
            const string success = "Success";
            var row = new Dictionary<string, object>();
            const string column1 = "column1";
            row.Add(column1, "value1");
            const string column2 = "column2";
            row.Add(column2, "value2");
            var resultList = new List<Dictionary<string, object>>(){ row };
            var sqlQueryResultTest = GetSUT(success, resultList);
            var result = sqlQueryResultTest.Read();
            Assert.True(result);
        }

        [Fact]
        public void Read_WhenNoColumnsExist_ShouldReturnFalse()
        {
            const string success = "Success";
            var sqlQueryResultTest = GetSUT(success);
            var result = sqlQueryResultTest.Read();
            Assert.False(result);
        }

        private SqlQueryResult GetSUT(string message, List<Dictionary<string,object>> resultList = null)
        {
            return new SqlQueryResult(1, true, message, resultList);
        }
    }
}
