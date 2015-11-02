using Demo02.LayeredArchitecture.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo02.LayeredArchitecture.Business.Test
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
