using Demo02.LayeredArchitecture.Business;
using Demo02.LayeredArchitecture.Domain;
using Demo02.LayeredArchitecture.Infrastructure.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Demo03.Presentation.MVVM.UI.Test
{
    [TestClass]
    public class Store_Valid_User_Input_To_Database
    {
        [TestMethod]
        public void When_Valid_Filename_Separator_FiscalYear_Are_Provided()
        {
            //Arrange
            var repository = new Mock<IExpenseRepository>();
            repository.Setup(r => r.SaveYearExpenses(It.IsAny<IEnumerable<YearlyExpense>>())).
                Callback<IEnumerable<YearlyExpense>>(expenses =>
                {
                    Assert.IsNotNull(expenses);
                    Assert.AreEqual(5, expenses.Count());
                    Assert.AreEqual(1104, expenses.Single(e => e.EmployeeId == 1 && e.Category == "Travel").Amount);
                    Assert.AreEqual(250, expenses.Single(e => e.EmployeeId == 1 && e.Category == "Parking").Amount);
                    Assert.AreEqual(7000, expenses.Single(e => e.EmployeeId == 2 && e.Category == "Travel").Amount);
                    Assert.AreEqual(760, expenses.Single(e => e.EmployeeId == 2 && e.Category == "Parking").Amount);
                    Assert.AreEqual(2860, expenses.Single(e => e.EmployeeId == 2 && e.Category == "Hotel").Amount);
                });

            ExpenseService expenseService = new ExpenseService(repository.Object);
            MainWindowViewModel viewModel = new MainWindowViewModel(expenseService)
            {
                Filename = "Expenses_2015.txt",
                FiscalYear = 2015,
                Separator = ','
            };

            //Act
            viewModel.PersistFile();

            //Assert
            Assert.AreEqual("File Expenses_2015.txt Submitted", viewModel.ErrorMessage);
        }
    }
}
