using System;
using System.Collections.Generic;

namespace Demo02.LayeredArchitecture.Domain
{
    public interface IExpenseRepository
    {
        void SaveYearExpenses(IEnumerable<YearlyExpense> expenses);
        IEnumerable<YearlyExpense> ReceivedExpenses();

        event Action<YearlyExpense> YearlyExpenseSaved;
    }
}
