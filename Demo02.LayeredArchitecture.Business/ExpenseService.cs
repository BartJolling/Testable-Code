using Demo01.TestDrivenDevelopment;
using Demo02.LayeredArchitecture.Domain;
using Demo02.LayeredArchitecture.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo02.LayeredArchitecture.Business
{
    public class ExpenseService
    {
        public IExpenseRepository ExpenseRepository { get; set; }

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            if (expenseRepository == null)
                throw new ArgumentNullException("ExpenseRepository");

            this.ExpenseRepository = expenseRepository;
        }

        public void PersistFile(string fileContent, char separator, int fiscalYear)
        {
            if (string.IsNullOrWhiteSpace(fileContent))
                throw new ArgumentNullException("Provide valid file content");

            var expenses = ParseFileContent(fileContent, separator, 2015);

            ExpenseRepository.SaveYearExpenses(expenses);
        }

        private IReadOnlyCollection<YearlyExpense> ParseFileContent(string fileContent, char separator, int fiscalYear)
        {
            if (string.IsNullOrWhiteSpace(fileContent))
                throw new ArgumentNullException("Provide valid file content");

            var expenses = new List<YearlyExpense>();
            var stringCalculator = new StringCalculator04();

            var lines = fileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).Skip(1);

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