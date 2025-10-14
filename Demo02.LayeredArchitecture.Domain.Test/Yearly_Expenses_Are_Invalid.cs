using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Demo02.LayeredArchitecture.Domain.Test
{
    [TestClass]
    public class Yearly_Expenses_Are_Invalid
    {
        [TestMethod]
        public void LA01_Expense_Date_Before_1962()
        {
            //Act
            void act() => new YearlyExpense()
            {
                Amount = 1000,
                Category = "Hotel",
                EmployeeId = 1,
                FiscalYear = 1960
            };

            //Assert
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(act);
        }
    }
}
