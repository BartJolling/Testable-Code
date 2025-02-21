using Demo02.LayeredArchitecture.Application;
using Demo02.LayeredArchitecture.Domain;
using Demo02.LayeredArchitecture.Infrastructure;
using Microsoft.Data.SqlClient;
using Shared.TestableCode.Database;
using System;
using System.Configuration;

namespace Demo02.LayeredArchitecture.Facade
{
    public static class ExpenseServiceFactory
    {
        public static ExpenseService Create()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ExpensesDatabase"].ConnectionString;
            var connection = new SqlConnection(connectionString);
            var repository = new ExpenseRepository(connection);

            return Create(repository);
        }

        public static ExpenseService Create(IExpenseRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository);

            return new ExpenseService(repository);
        }

        public static ExpenseService Create(ExpensesDbContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext);

            dbContext.Database.EnsureCreated();

            var repository = new ExpenseRepositoryEF(dbContext);

            return new ExpenseService(repository);
        }
    }
}
