using Demo02.LayeredArchitecture.Domain;
using System;
using System.Collections.Generic;

namespace Demo02.LayeredArchitecture.Application.Test;

internal class ExpenseRepositoryMock : IExpenseRepository
{
    private IEnumerable<Domain.YearlyExpense> _receivedExpenses = null;

    public void SaveYearExpenses(IEnumerable<Domain.YearlyExpense> expenses)
    {
        this._receivedExpenses = expenses;
    }

    public event Action<Domain.YearlyExpense> YearlyExpenseSaved;

    public IEnumerable<YearlyExpense> ReceivedExpenses()
    {
        return this._receivedExpenses;
    }
}
