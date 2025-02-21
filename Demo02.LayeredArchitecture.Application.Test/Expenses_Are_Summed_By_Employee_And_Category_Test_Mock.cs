using Demo02.LayeredArchitecture.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo02.LayeredArchitecture.Application.Test
{
    [TestClass]
    public class Expenses_Are_Summed_By_Employee_And_Category_Test_Mock
    {
        [TestMethod]
        public void LA02_When_Receiving_Valid_File_Mock()
        {
            //Arrange
            string fileContent = "EmployeeId,Category,January,February,March,April,May,June,July,August,September,October,November,December" + Environment.NewLine +
                                 "1,Travel,92,92,92,92,92,92,92,92,92,92,92,92" + Environment.NewLine +
                                 "1,Parking,20,20,20,20,35,00,20,35,20,20,20,20" + Environment.NewLine +
                                 "2,Travel,1500,0,0,1700,0,0,1400,0,0,2400,0,0" + Environment.NewLine +
                                 "2,Parking,200,0,0,150,0,0,235,0,0,175,0,0" + Environment.NewLine +
                                 "2,Hotel,700,0,0,750,0,0,335,0,0,1075,0,0" + Environment.NewLine;
            
            
            var repository = new ExpenseRepositoryMock();
            var service = new ExpenseService(repository);

            //Act
            service.PersistFile(fileContent, ',', 2015);

            IEnumerable<YearlyExpense> expenses = repository.ReceivedExpenses();

            //Assert
            Assert.IsNotNull(expenses);
            Assert.AreEqual(5, expenses.Count());
            Assert.AreEqual(1104, expenses.Single(e => e.EmployeeId == 1 && e.Category == "Travel").Amount);
            Assert.AreEqual(250, expenses.Single(e => e.EmployeeId == 1 && e.Category == "Parking").Amount);
            Assert.AreEqual(7000, expenses.Single(e => e.EmployeeId == 2 && e.Category == "Travel").Amount);
            Assert.AreEqual(760, expenses.Single(e => e.EmployeeId == 2 && e.Category == "Parking").Amount);
            Assert.AreEqual(2860, expenses.Single(e => e.EmployeeId == 2 && e.Category == "Hotel").Amount);
        }

        [TestMethod]
        public void LA02_When_Receiving_Valid_File_Moq()
        {
            //Arrange
            string fileContent = "EmployeeId,Category,January,February,March,April,May,June,July,August,September,October,November,December" + Environment.NewLine +
                                 "1,Travel,92,92,92,92,92,92,92,92,92,92,92,92" + Environment.NewLine +
                                 "1,Parking,20,20,20,20,35,00,20,35,20,20,20,20" + Environment.NewLine +
                                 "2,Travel,1500,0,0,1700,0,0,1400,0,0,2400,0,0" + Environment.NewLine +
                                 "2,Parking,200,0,0,150,0,0,235,0,0,175,0,0" + Environment.NewLine +
                                 "2,Hotel,700,0,0,750,0,0,335,0,0,1075,0,0" + Environment.NewLine;

            IEnumerable<YearlyExpense> expenses = null;

            var repository = new Mock<IExpenseRepository>();
            repository.Setup(r => r.SaveYearExpenses(It.IsAny<IEnumerable<YearlyExpense>>())).
                Callback<IEnumerable<YearlyExpense>>(e =>
                    {
                        expenses = e;
                    });

            //Act
            var service = new ExpenseService(repository.Object);
            service.PersistFile(fileContent, ',', 2015);

            //Assert
            repository.VerifyAll();
            Assert.IsNotNull(expenses);
            Assert.AreEqual(5, expenses.Count());
            Assert.AreEqual(1104, expenses.Single(e => e.EmployeeId == 1 && e.Category == "Travel").Amount);
            Assert.AreEqual(250, expenses.Single(e => e.EmployeeId == 1 && e.Category == "Parking").Amount);
            Assert.AreEqual(7000, expenses.Single(e => e.EmployeeId == 2 && e.Category == "Travel").Amount);
            Assert.AreEqual(760, expenses.Single(e => e.EmployeeId == 2 && e.Category == "Parking").Amount);
            Assert.AreEqual(2860, expenses.Single(e => e.EmployeeId == 2 && e.Category == "Hotel").Amount);
        }
    }
}
