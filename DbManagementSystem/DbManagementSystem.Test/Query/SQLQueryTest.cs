using DbManagementSystem.Core.Query;
using Xunit;

namespace DbManagementSystem.Test.Query
{
    public class SQLQueryTest
    {
        [Fact]
        public void GetQuery_WhenQueryIsNull_ShouldReturnEmpty()
        {
            var sqlQuery = GetSUT();
            var result = sqlQuery.GetQuery();
            Assert.Empty(result);
        }

        [Fact]
        public void GetQuery_WhenQueryIsEmpty_ShouldReturnEmpty()
        {
            var sqlQuery = GetSUT(string.Empty);
            var result = sqlQuery.GetQuery();
            Assert.Empty(result);
        }

        [Fact]
        public void GetQuery_WhenQueryIsValid_ShouldReturnTrimmedQuery()
        {
            const string validquery = "validQuery with whitespaces       ";
            var sqlQuery = GetSUT(validquery);
            var result = sqlQuery.GetQuery();
            Assert.Equal(validquery.Trim(), result);
        }

        [Fact]
        public void SetParameter_WhenParameterDoesNotExist_QueryShouldRemainIdentical()
        {
            const string nume = "nume";
            const string gigel = "Gigel";
            const string validqueryWithWhitespaces = "validQuery with whitespaces ";
            var sqlQuery = GetSUT(validqueryWithWhitespaces);
            var result = sqlQuery.SetParameter(nume, gigel);
            Assert.Equal(validqueryWithWhitespaces.Trim(), result.GetQuery());
        }

        [Fact]
        public void SetParameter_WhenParameterExists_QueryShouldBeUpdated()
        {
            const string nume = "nume";
            const string prenume = "Gigel";
            const string validqueryWithWhitespaces = "validQuery with whitespaces ";
            const string initialQuery = validqueryWithWhitespaces + ":" + nume + "       ";
            const string expectedUpdatedQuery = validqueryWithWhitespaces + prenume;
            var sqlQuery = GetSUT(initialQuery);
            var result = sqlQuery.SetParameter(nume, prenume);
            Assert.Equal(expectedUpdatedQuery, result.GetQuery());
        }

        private SqlQuery GetSUT(string query = null)
        {
            return new SqlQuery(query);
        }
    }
}
