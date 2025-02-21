using Demo02.LayeredArchitecture.Domain;
using Shared.TestableCode.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Demo02.LayeredArchitecture.Infrastructure;

public class ExpenseRepositoryEF(ExpensesDbContext dbContext) : IExpenseRepository
{
    private readonly ExpensesDbContext _expensesDbContent = dbContext;

    public event Action<YearlyExpense> YearlyExpenseSaved;

    public IEnumerable<YearlyExpense> ReceivedExpenses()
    {
        var expenses = _expensesDbContent.Expenses
            .Select(e => new YearlyExpense
            {
                EmployeeId = e.EmployeeId,
                FiscalYear = e.FiscalYear,
                Amount = e.Amount,
                Category = e.Category,
            })
            .ToList();

        return expenses;
    }

    public void SaveYearExpenses(IEnumerable<YearlyExpense> expenses)
    {
        ArgumentNullException.ThrowIfNull(expenses);

        if (!expenses.Any()) return; //nothing to do

        var newExpenses = expenses.Select(expense => new Expense
        {
            EmployeeId = expense.EmployeeId,
            FiscalYear = expense.FiscalYear,
            Category = expense.Category,
            Amount = expense.Amount
        }).ToList();

        _expensesDbContent.Expenses.AddRange(newExpenses);
        int result = _expensesDbContent.SaveChanges();

        if (result != newExpenses.Count)
            throw new DataException($"Expected to insert {newExpenses.Count} expenses, but inserted {result}.");

        foreach (var expense in expenses)
        {
            YearlyExpenseSaved?.Invoke(expense);
        }
    }
}
