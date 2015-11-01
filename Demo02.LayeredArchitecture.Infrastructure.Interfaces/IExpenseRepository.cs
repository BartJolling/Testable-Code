using Demo02.LayeredArchitecture.Domain;
using System;
using System.Collections.Generic;

namespace Demo02.LayeredArchitecture.Infrastructure.Interfaces
{
    public interface IExpenseRepository
    {
        void SaveYearExpenses(IEnumerable<YearlyExpense> expenses);

        event Action<YearlyExpense> YearlyExpenseSaved;
    }
}
