using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Demo02.LayeredArchitecture.Domain.Test
{
    [TestClass]
    public class YearlyExpenseTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LA01_ExpensesFromBefore1962AreInvalid()
        {
            //Arrange
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
