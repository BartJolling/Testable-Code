using Demo02.LayeredArchitecture.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Demo02.LayeredArchitecture.Infrastructure
{
    public class ExpenseRepository : IExpenseRepository
    {
        private IDbConnection _connection = null;
        public ExpenseRepository(IDbConnection connection)
        {
            if (connection == null)
                throw new ArgumentNullException("Provide a valid database connection");

            this._connection = connection;
        }

        public void SaveYearExpenses(IEnumerable<Domain.YearlyExpense> expenses)
        {
            if (expenses == null )
                throw new ArgumentNullException("Provide a valid list of yearly expenses");

            if (expenses.Count() == 0)
                return; //nothing to do

            throw new NotImplementedException();
        }
    }
}