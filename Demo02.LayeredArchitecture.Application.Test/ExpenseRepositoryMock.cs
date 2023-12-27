using Demo02.LayeredArchitecture.Domain;
using System;
using System.Collections.Generic;

namespace Demo02.LayeredArchitecture.Application.Test
{
    internal class ExpenseRepositoryMock : IExpenseRepository
    {
        public IEnumerable<Domain.YearlyExpense> ReceivedExpenses = null;

        public void SaveYearExpenses(IEnumerable<Domain.YearlyExpense> expenses)
        {
            this.ReceivedExpenses = expenses;
        }

        public event Action<Domain.YearlyExpense> YearlyExpenseSaved;
    }
}
