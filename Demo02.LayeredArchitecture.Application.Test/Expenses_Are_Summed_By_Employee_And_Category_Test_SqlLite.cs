using Demo02.LayeredArchitecture.Facade;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.TestableCode.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo02.LayeredArchitecture.Application.Test
{    
    [TestClass]
    public class Expenses_Are_Summed_By_Employee_And_Category_Test_SqlLite
        {
        private static ExpensesDbContext _dbContext;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<ExpensesDbContext>()
                .UseSqlite(connection)
                .Options;

            _dbContext = new ExpensesDbContext(options);
        }

        [ClassCleanup(ClassCleanupBehavior.EndOfClass)]
        public static void ClassCleanup()
        {
            // Dispose of the DbContext
            _dbContext.Dispose();
        }

        [TestMethod]
        public void LA02_When_Receiving_Valid_File()
        {
            //Arrange
            string fileContent = "EmployeeId,Category,January,February,March,April,May,June,July,August,September,October,November,December" + Environment.NewLine +
                                 "1,Travel,92,92,92,92,92,92,92,92,92,92,92,92" + Environment.NewLine +
                                 "1,Parking,20,20,20,20,35,00,20,35,20,20,20,20" + Environment.NewLine +
                                 "2,Travel,1500,0,0,1700,0,0,1400,0,0,2400,0,0" + Environment.NewLine +
                                 "2,Parking,200,0,0,150,0,0,235,0,0,175,0,0" + Environment.NewLine +
                                 "2,Hotel,700,0,0,750,0,0,335,0,0,1075,0,0" + Environment.NewLine;

            var service = ExpenseServiceFactory.Create(_dbContext);

            //Act
            service.PersistFile(fileContent, ',', 2015);

            IEnumerable<Expense> expenses = _dbContext.Expenses;

            //Assert
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
