using Demo02.LayeredArchitecture.Application;
using Demo02.LayeredArchitecture.Domain;
using Moq;

namespace Demo03.Presentation.MVVM.UI.Test
{
    [TestClass]
    public class Store_Valid_User_Input_To_Database
    {
        [TestMethod]
        public void When_Valid_Filename_Separator_FiscalYear_Are_Provided()
        {
            //Arrange
            var expectedMessages = new[]
            {
                "File Expenses_2015.txt Submitted",
                "File Expenses_2015.txt Uploaded"
            };
            var actualMessages = new List<string>();
            int counter = 0;

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

            var expenseService = new ExpenseService(repository.Object);
            var viewModel = new MainWindowViewModel(expenseService)
            {
                Filename = "Expenses_2015.txt",
                FiscalYear = 2015,
                Separator = ','
            };

            // Subscribe to property changed
            viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(viewModel.StatusMessage))
                {
                    actualMessages.Add(viewModel.StatusMessage);
                    counter++;
                }
            };

            //Act
            viewModel.PersistFile();

            // Wait until both status messages have had a chance to fire
#if DEBUG
            var deadline = DateTime.Now + TimeSpan.FromSeconds(30); // long timeout for debugging
            var delay = 60;
#else
            var deadline = DateTime.Now + TimeSpan.FromSeconds(5);  // normal timeout for CI/tests
            var delay = 10;
#endif

            while (counter < expectedMessages.Length && DateTime.Now < deadline)
            {
                Thread.Sleep(delay);
            }

            // Assert
            Assert.HasCount(expectedMessages.Length, actualMessages, "Did not receive expected number of StatusMessage updates");
            for (int i = 0; i < expectedMessages.Length; i++)
            {
                Assert.AreEqual(expectedMessages[i], actualMessages[i], $"Message at index {i} mismatch");
            }
        }
    }
}
