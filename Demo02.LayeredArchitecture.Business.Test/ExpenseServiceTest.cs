using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Demo02.LayeredArchitecture.Domain;
using Demo02.LayeredArchitecture.Infrastructure.Interfaces;
using Moq;

namespace Demo02.LayeredArchitecture.Business.Test
{
    [TestClass]
    public class ExpenseServiceTest
    {
        [TestMethod]
        public void LA02_WhenReceivingValidFileThenExpensesAreSummedByEmployeeAndCategory()
        {
            //Arrange
            string fileContent = "EmployeeId,Category,January,February,March,April,May,June,July,August,September,October,November,December" + Environment.NewLine +
                                 "1,Travel,92,92,92,92,92,92,92,92,92,92,92,92" + Environment.NewLine +
                                 "1,Parking,20,20,20,20,35,00,20,35,20,20,20,20" + Environment.NewLine +
                                 "2,Travel,1500,0,0,1700,0,0,1400,0,0,2400,0,0" + Environment.NewLine +
                                 "2,Parking,200,0,0,150,0,0,235,0,0,175,0,0" + Environment.NewLine +
                                 "2,Hotel,700,0,0,750,0,0,335,0,0,1075,0,0" + Environment.NewLine;
            
            //Act
            var repository = new ExpenseRepositoryMock();
            ExpenseService service = new ExpenseService(repository);
            service.PersistFile(fileContent, ',', 2015);
            IEnumerable<YearlyExpense> expenses = repository.ReceivedExpenses;

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
        public void LA02_WhenReceivingValidFileThenExpensesAreSummedByEmployeeAndCategory_Moq()
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
                Callback<IEnumerable<YearlyExpense>>(e => expenses = e);

            //Act
            ExpenseService service = new ExpenseService(repository.Object);
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
