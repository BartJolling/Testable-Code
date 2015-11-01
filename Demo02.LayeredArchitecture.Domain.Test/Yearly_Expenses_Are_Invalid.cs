using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Demo02.LayeredArchitecture.Domain.Test
{
    [TestClass]
    public class Yearly_Expenses_Are_Invalid
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LA01_Expense_Date_Before_1962()
        {
            //Act
            YearlyExpense expense = new YearlyExpense()
            {
                Amount = 1000,
                Category = "Hotel",
                EmployeeId = 1,
                FiscalYear = 1960
            };
        }
    }
}
