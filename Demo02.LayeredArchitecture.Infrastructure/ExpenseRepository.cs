using Demo02.LayeredArchitecture.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Demo02.LayeredArchitecture.Infrastructure
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly IDbConnection _connection = null;

        public event Action<Domain.YearlyExpense> YearlyExpenseSaved;

        public ExpenseRepository(IDbConnection connection)
        {
            ArgumentNullException.ThrowIfNull(connection);

            this._connection = connection;
        }

        public void SaveYearExpenses(IEnumerable<Domain.YearlyExpense> expenses)
        {
            ArgumentNullException.ThrowIfNull(expenses);

            if (!expenses.Any()) return; //nothing to do

            try
            {
                this._connection.Open();

                foreach (var expense in expenses)
                {
                    SaveYearExpense(expense);
                }
            }
            finally
            {
                if(this._connection.State != ConnectionState.Closed)
                    this._connection.Close();
            }
        }

        private void SaveYearExpense(Domain.YearlyExpense expense)
        {
            int result;

            using (var command = this._connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO dbo.Expenses (EmployeeId, FiscalYear, Category, Amount) VALUES (@EmployeeId, @FiscalYear, @Category, @Amount)";

                var employeeId = command.CreateParameter();
                employeeId.ParameterName = "@EmployeeId";
                employeeId.DbType = DbType.Int32;                    
                employeeId.Value = expense.EmployeeId;
                command.Parameters.Add(employeeId);

                var fiscalYear = command.CreateParameter();
                fiscalYear.ParameterName = "@FiscalYear";
                fiscalYear.DbType = DbType.Int32;
                fiscalYear.Value = expense.FiscalYear;
                command.Parameters.Add(fiscalYear);

                var category = command.CreateParameter();
                category.ParameterName = "@Category";
                category.DbType = DbType.String;
                category.Value = expense.Category;
                command.Parameters.Add(category);

                var amount = command.CreateParameter();
                amount.ParameterName = "@Amount";
                amount.DbType = DbType.Decimal;
                amount.Value = expense.Amount;
                command.Parameters.Add(amount);

                result = command.ExecuteNonQuery();                
            }

            if (result != 1)
                throw new DataException("Expected to insert 1 expense");

            YearlyExpenseSaved?.Invoke(expense);
        }
    }
}