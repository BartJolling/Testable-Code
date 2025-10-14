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

#pragma warning disable CS0067
    public event Action<Domain.YearlyExpense> YearlyExpenseSaved;
#pragma warning restore CS0067

    public IEnumerable<YearlyExpense> ReceivedExpenses()
    {
        return this._receivedExpenses;
    }
}
