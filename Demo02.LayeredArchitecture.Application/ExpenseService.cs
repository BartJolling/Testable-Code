using Demo01.TestDrivenDevelopment;
using Demo02.LayeredArchitecture.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo02.LayeredArchitecture.Application
{
    public class ExpenseService
    {
        private IExpenseRepository ExpenseRepository { get; set; }

        public event Action<YearlyExpense> ExpensePersisted;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            ArgumentNullException.ThrowIfNull(expenseRepository);

            this.ExpenseRepository = expenseRepository;
            this.ExpenseRepository.YearlyExpenseSaved += ExpenseRepository_YearlyExpenseSaved;
        }

        private void ExpenseRepository_YearlyExpenseSaved(YearlyExpense expense)
        {
            if (ExpensePersisted != null)
            {
                //Simulate Processing
                System.Threading.Thread.Sleep(500);

                ExpensePersisted.Invoke(expense);
            }
        }

        public void PersistFile(string fileContent, char separator, int fiscalYear)
        {
            if (string.IsNullOrWhiteSpace(fileContent))
                throw new ArgumentNullException(nameof(fileContent));

            var expenses = ParseFileContent(fileContent, separator, 2015);

            ExpenseRepository.SaveYearExpenses(expenses);
        }

        private static IReadOnlyCollection<YearlyExpense> ParseFileContent(string fileContent, char separator, int fiscalYear)
        {
            if (string.IsNullOrWhiteSpace(fileContent))
                throw new ArgumentNullException(nameof(fileContent));

            var expenses = new List<YearlyExpense>();
            var stringCalculator = new StringCalculator04();

            var lines = fileContent.Split([Environment.NewLine], StringSplitOptions.None).Skip(1);

            foreach( var line in lines)
            {
                if(string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var fields = line.Split(separator);
                expenses.Add(new YearlyExpense()
                {
                    Amount = stringCalculator.Add(line, separator, 2),
                    Category = fields[1],
                    EmployeeId = int.Parse(fields[0], System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture),
                    FiscalYear = fiscalYear
                });
            }

            return expenses;
        }
    }
}