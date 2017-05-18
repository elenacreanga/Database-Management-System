using System;
using DbManagementSystem.Core.Database;
using NSubstitute;
using Xunit;

namespace DbManagementSystem.Test.Database
{
    public class DatabaseConfigurationTest
    {
        [Fact]
        public void GetDefaultValueForDataType_GivenDataTypeString_ShouldReturnDefaultValueEmpty()
        {
            var databaseConfiguration = GetSUT();
            const string dataType = "string";
            var result = databaseConfiguration.GetDefaultValueForDataType(dataType);
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void GetDefaultValueForDataType_GivenDataTypeInt_ShouldReturnDefaultValue0()
        {
            var databaseConfiguration = GetSUT();
            const string dataType = "int";
            var result = databaseConfiguration.GetDefaultValueForDataType(dataType);
            Assert.Equal(0, result);
        }

        [Fact]
        public void GetDefaultValueForDataType_GivenDataTypeUnknown_ShouldReturnNull()
        {
            var databaseConfiguration = GetSUT();
            const string dataType = "unknown";
            var result = databaseConfiguration.GetDefaultValueForDataType(dataType);
            Assert.Equal(null, result);
        }

        [Fact]
        public void IsDataTypeAllowed_GivenDataTypeString_ShouldReturnTrue()
        {
            var databaseConfiguration = GetSUT();
            const string dataType = "string";
            var result = databaseConfiguration.IsDataTypeAllowed(dataType);
            Assert.True(result);
        }

        [Fact]
        public void IsDataTypeAllowed_GivenDataTypeInt_ShouldReturnTrue()
        {
            var databaseConfiguration = GetSUT();
            const string dataType = "int";
            var result = databaseConfiguration.IsDataTypeAllowed(dataType);
            Assert.True(result);
        }

        [Fact]
        public void IsDataTypeAllowed_GivenDataTypeUnknown_ShouldReturnFalse()
        {
            var databaseConfiguration = GetSUT();
            const string dataType = "unknown";
            var result = databaseConfiguration.IsDataTypeAllowed(dataType);
            Assert.False(result);
        }

        [Fact]
        public void IsOperatorAllowedForDataType_GivenValidOperatorForString_ShouldReturnTrue()
        {
            var databaseConfiguration = GetSUT();
            const string dataType = "string";
            const string operation = "!=";
            var result = databaseConfiguration.IsOperatorAllowedForDataType(dataType, operation);
            Assert.True(result);
        }

        [Fact]
        public void IsOperatorAllowedForDataType_GivenInvalidOperatorForString_ShouldReturnFalse()
        {
            var databaseConfiguration = GetSUT();
            const string dataType = "string";
            const string operation = "<>";
            var result = databaseConfiguration.IsOperatorAllowedForDataType(dataType, operation);
            Assert.False(result);
        }

        [Fact]
        public void IsOperatorAllowedForDataType_GivenValidOperatorForInt_ShouldReturnTrue()
        {
            var databaseConfiguration = GetSUT();
            const string dataType = "int";
            const string operation = "<";
            var result = databaseConfiguration.IsOperatorAllowedForDataType(dataType, operation);
            Assert.True(result);
        }

        [Fact]
        public void IsOperatorAllowedForDataType_GivenInvalidOperatorForInt_ShouldReturnFalse()
        {
            var databaseConfiguration = GetSUT();
            const string dataType = "int";
            const string operation = "<>";
            var result = databaseConfiguration.IsOperatorAllowedForDataType(dataType, operation);
            Assert.False(result);
        }

        [Fact]
        public void ParseDataTypeValue_GivenStringDataAndStringDataType_ShouldReturnParsed()
        {
            var databaseConfiguration = GetSUT();
            const string dataType = "string";
            const string value = "123aAs";
            var result = databaseConfiguration.ParseDataTypeValue(dataType, value);
            Assert.Equal(typeof(string), result.GetType());
        }

        [Fact]
        public void ParseDataTypeValue_GivenIntDataAndIntDataType_ShouldReturnParsed()
        {
            var databaseConfiguration = GetSUT();
            const string dataType = "int";
            const string value = "123";
            var result = databaseConfiguration.ParseDataTypeValue(dataType, value);
            Assert.Equal(typeof(int), result.GetType());
        }

        [Fact]
        public void ParseDataTypeValue_GivenStringDataAndIntDataType_ShouldThrowException()
        {
            var databaseConfiguration = GetSUT();
            const string dataType = "int";
            const string value = "123aafs";
            Assert.ThrowsAny<Exception>(() => databaseConfiguration.ParseDataTypeValue(dataType, value));
        }

        [Fact]
        public void ParseDataTypeValue_GivenStringDataAndUnknownDataType_ShouldThrowException()
        {
            var databaseConfiguration = GetSUT();
            const string dataType = "int";
            const string value = "123aafs";
            Assert.ThrowsAny<Exception>(() => databaseConfiguration.ParseDataTypeValue(dataType, value));
        }

        [Fact]
        public void PerformOperation_GivenValidOperationAndParameters_ShouldSucceed()
        {
            var databaseConfiguration = GetSUT();
            const string dataType = "string";
            const string leftOperand = "5";
            const string rightOperand = "a";
            const string operation = "!=";
            var result = databaseConfiguration.PerformOperation(dataType, leftOperand, rightOperand, operation);
            Assert.True(result);
        }

        [Fact]
        public void PerformOperation_GivenInvalidOperatorAndValidParameters_ShouldThrowException()
        {
            var databaseConfiguration = GetSUT();
            const string dataType = "string";
            const string leftOperand = "5";
            const string rightOperand = "a";
            const string operation = "<";
            Assert.ThrowsAny<Exception>(
                () => databaseConfiguration.PerformOperation(dataType, leftOperand, rightOperand, operation));
        }

        [Fact]
        public void PerformOperation_GivenValidOperatorAndInvalidParameters_ShouldThrowException()
        {
            var databaseConfiguration = GetSUT();
            const string dataType = "int";
            const string leftOperand = "5";
            const int rightOperand = 1;
            const string operation = "<";
            Assert.ThrowsAny<Exception>(
                () => databaseConfiguration.PerformOperation(dataType, leftOperand, rightOperand, operation));
        }

        [Fact]
        public void PerformOperation_GivenValidOperatorAndParametersAndInvalidDataType_ShouldThrowException()
        {
            var databaseConfiguration = GetSUT();
            const string dataType = "unknown";
            const string leftOperand = "5";
            const string rightOperand = "a";
            const string operation = "=";
            Assert.ThrowsAny<Exception>(
                () => databaseConfiguration.PerformOperation(dataType, leftOperand, rightOperand, operation));
        }

        private DatabaseCofiguration GetSUT()
        {
            var databaseStorageService = Substitute.For<IDatabaseStorageService>();
            return new DatabaseCofiguration(databaseStorageService);
        }
    }
}
