using DbManagementSystem.Assertions.Database;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace DbManagementSystem.Assertions.Query.Clauses
{
    public class WhereClause : IWhereClause
    {
        private readonly IDatabaseConfiguration databaseConfiguration;
        private Dictionary<string, string> tableColumns;
        private List<Tuple<string, string, string, string>> expressions;

        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(expressions != null);
        }

        public WhereClause(IDatabaseConfiguration databaseConfiguration)
        {
            Contract.Ensures(this.databaseConfiguration != null);

            this.databaseConfiguration = databaseConfiguration;
            this.expressions = new List<Tuple<string, string, string, string>>();
        }

        public bool Build(string whereExpression, Dictionary<string, string> tableColumns)
        {
            Contract.Requires(!string.IsNullOrEmpty(whereExpression));
            Contract.Requires(tableColumns != null);

            try
            {
                this.tableColumns = tableColumns;
                var expressions = new List<Tuple<string, string, string, string>>();
                var operators = this.databaseConfiguration.GetAllowedOperators().ToArray();
                var orExpressions = whereExpression.Split(new[] { "OR" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var orExpression in orExpressions)
                {
                    var andExpressions = orExpression.Split(new[] { "AND" }, StringSplitOptions.RemoveEmptyEntries);
                    for (var i = 0; i < andExpressions.Length; i++)
                    {
                        var subExpression = andExpressions[i];
                        var found = false;
                        foreach (var op in operators)
                        {
                            var parts = subExpression.Split(new[] { op }, StringSplitOptions.RemoveEmptyEntries);
                            if (parts.Length == 2)
                            {
                                var nextOperator = (i == andExpressions.Length - 1) ? "OR" : "AND";
                                expressions.Add(new Tuple<string, string, string, string>(parts[0].Trim(), op, parts[1].Trim(), nextOperator));
                                found = true;
                                break;
                            }
                        }

                        if (found == false)
                        {
                            return false;
                        }
                    }
                }

                this.expressions = expressions;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool MatchesRow(Dictionary<string, object> row)
        {
            Contract.Requires(row != null);

            var expressionIndex = 0;
            var matches = false;
            while (expressionIndex < this.expressions.Count)
            {
                matches = matches || MatchesRow(row, ref expressionIndex);
                expressionIndex++;
            }

            return this.expressions.Any() ? matches : true;
        }

        private bool MatchesRow(Dictionary<string, object> row, ref int expressionIndex)
        {
            if (expressionIndex >= this.expressions.Count)
            {
                return true;
            }

            var expression = this.expressions[expressionIndex];
            var matches = Matches(expression.Item1, expression.Item2, expression.Item3, row);
            if (expression.Item4 == "AND")
            {
                expressionIndex++;
                if (matches == false)
                {
                    while (expressionIndex < this.expressions.Count && this.expressions[expressionIndex].Item4 == "AND")
                    {
                        expressionIndex++;
                    }

                    return false;
                }
                else
                {
                    return matches && MatchesRow(row, ref expressionIndex);
                }
            }
            else
            {
                return matches;
            }
        }

        private bool Matches(string columnName, string op, string value, Dictionary<string, object> row)
        {
            if (!row.ContainsKey(columnName))
            {
                throw new Exception(string.Format("Invalid column name '{0}' in WHERE clause", columnName));
            }

            if (value.StartsWith("'"))
            {
                value = value.Substring(1);
            }

            if (value.EndsWith("'"))
            {
                value = value.Substring(0, value.Length - 1);
            }

            var columnDataType = this.tableColumns[columnName];

            Contract.Assert(columnDataType != null);

            if (!this.databaseConfiguration.IsOperatorAllowedForDataType(columnDataType, op))
            {
                throw new Exception(string.Format("Invalid operator '{0}' for column '{1}' in WHERE clause", op, columnName));
            }

            var valueToCompare = this.databaseConfiguration.ParseDataTypeValue(columnDataType, value);

            Contract.Assert(valueToCompare != null);

            var rowValue = row[columnName];

            return this.databaseConfiguration.PerformOperation(columnDataType, rowValue, valueToCompare, op);
        }
    }
}
